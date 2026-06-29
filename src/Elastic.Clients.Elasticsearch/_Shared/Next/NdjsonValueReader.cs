// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

#if NET10_0_OR_GREATER
using System.IO.Pipelines;
#endif

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Receives one complete top-level JSON value at a time, as a slice into the reader's buffer. The slice is only valid
/// for the duration of the call; the next refill may compact or overwrite it, so a visitor must materialize what it
/// needs (deserialize into a managed object) before returning.
/// </summary>
internal delegate void NdjsonValueVisitor(ReadOnlySequence<byte> value, int index);

/// <summary>
/// Streams the top-level JSON values of an NDJSON body (or a tolerated single JSON array of those values) without
/// buffering the whole body: bytes are read into a small pooled buffer that is grown only as far as the largest single
/// value requires, each complete value is handed to a visitor, then its bytes are dropped. Modeled on the streaming
/// reader prototyped for the dictionary case; NDJSON is the simpler shape (repeated top-level values, no enclosing
/// object), so the parser is a single value loop.
/// </summary>
internal static class NdjsonValueReader
{
	private const int DefaultBufferSize = 64 * 1024;

	/// <summary>
	/// The body is either genuine NDJSON (multiple top-level values, what the client emits) or a single JSON array of
	/// those values (as recorded by tooling). The shape is decided from the first token and then drives iteration.
	/// </summary>
	private enum NdjsonShape
	{
		Unknown,
		Array,
		MultiValue
	}

	private struct NdjsonReaderState
	{
		public JsonReaderState JsonState;
		public NdjsonShape Shape;
	}

	/// <summary>
	/// The outer reader needs <see cref="JsonReaderOptions.AllowMultipleValues"/> so it can walk successive top-level
	/// values; <see cref="JsonReaderOptions.MaxDepth"/> mirrors the serializer (default 64 would reject deeply nested
	/// mappings the serializer accepts at 512).
	/// </summary>
	public static JsonReaderOptions BuildReaderOptions(JsonSerializerOptions options) => new()
	{
		AllowMultipleValues = true,
		MaxDepth = options.MaxDepth,
		AllowTrailingCommas = options.AllowTrailingCommas,
		CommentHandling = options.ReadCommentHandling
	};

	/// <summary>
	/// Deserializes one complete top-level value (as handed to a <see cref="NdjsonValueVisitor"/>) into
	/// <typeparamref name="T"/>, copying a multi-segment slice to a contiguous buffer first. The inner reader's
	/// <see cref="JsonReaderOptions.MaxDepth"/> mirrors the serializer, matching the outer drive loop.
	/// </summary>
	public static T? DeserializeValue<T>(in ReadOnlySequence<byte> value, JsonSerializerOptions options)
	{
		var readerOptions = new JsonReaderOptions { MaxDepth = options.MaxDepth };

		if (value.IsSingleSegment)
			return DeserializeValue<T>(value.First.Span, options, readerOptions);

		var length = (int)value.Length;
		var rented = ArrayPool<byte>.Shared.Rent(length);
		try
		{
			value.CopyTo(rented);
			return DeserializeValue<T>(rented.AsSpan(0, length), options, readerOptions);
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(rented);
		}
	}

	private static T? DeserializeValue<T>(ReadOnlySpan<byte> span, JsonSerializerOptions options, JsonReaderOptions readerOptions)
	{
		var reader = new Utf8JsonReader(span, readerOptions);
		reader.Read();
		return reader.ReadValue<T>(options);
	}

	public static void DriveStream(Stream stream, JsonReaderOptions readerOptions, NdjsonValueVisitor visit)
	{
		var buffer = new StreamBuffer(stream, DefaultBufferSize);
		try
		{
			Drive(new SyncStreamBufferCursor(buffer), readerOptions, visit, default);
		}
		finally
		{
			buffer.Dispose();
		}
	}

	public static async ValueTask DriveStreamAsync(Stream stream, JsonReaderOptions readerOptions, NdjsonValueVisitor visit, CancellationToken cancellationToken)
	{
#if NET10_0_OR_GREATER
		// leaveOpen: the caller owns the response stream; completing the pipe must not close it.
		var pipeReader = PipeReader.Create(stream, new StreamPipeReaderOptions(leaveOpen: true));
		try
		{
			await DriveAsync(new PipeReaderCursor(pipeReader), readerOptions, visit, cancellationToken).ConfigureAwait(false);
		}
		finally
		{
			await pipeReader.CompleteAsync().ConfigureAwait(false);
		}
#else
		var buffer = new StreamBuffer(stream, DefaultBufferSize);
		try
		{
			await DriveAsync(new AsyncStreamCursor(buffer), readerOptions, visit, cancellationToken).ConfigureAwait(false);
		}
		finally
		{
			buffer.Dispose();
		}
#endif
	}

	private static void Drive(ISyncBufferCursor cursor, JsonReaderOptions readerOptions, NdjsonValueVisitor visit, CancellationToken cancellationToken)
	{
		var rs = new NdjsonReaderState { JsonState = new JsonReaderState(readerOptions), Shape = NdjsonShape.Unknown };
		var index = 0;
		var done = false;
		while (!done)
		{
			if (!cursor.Read())
				break;

			var buffer = cursor.Buffer;
			var isFinal = cursor.IsCompleted;
			var reachedEnd = false;
			while (TryReadNextValue(ref buffer, isFinal, ref rs, out var value, out reachedEnd))
			{
				if (reachedEnd)
				{
					done = true;
					break;
				}

				cancellationToken.ThrowIfCancellationRequested();
				visit(value, index++);
			}

			if (reachedEnd)
				done = true;

			cursor.AdvanceTo(buffer.Start, buffer.End);
			if (cursor.IsCompleted)
				break;
		}
	}

	private static async ValueTask DriveAsync(IAsyncBufferCursor cursor, JsonReaderOptions readerOptions, NdjsonValueVisitor visit, CancellationToken cancellationToken)
	{
		var rs = new NdjsonReaderState { JsonState = new JsonReaderState(readerOptions), Shape = NdjsonShape.Unknown };
		var index = 0;
		var done = false;
		while (!done)
		{
			if (!await cursor.ReadAsync(cancellationToken).ConfigureAwait(false))
				break;

			var buffer = cursor.Buffer;
			var isFinal = cursor.IsCompleted;
			var reachedEnd = false;
			while (TryReadNextValue(ref buffer, isFinal, ref rs, out var value, out reachedEnd))
			{
				if (reachedEnd)
				{
					done = true;
					break;
				}

				cancellationToken.ThrowIfCancellationRequested();
				visit(value, index++);
			}

			if (reachedEnd)
				done = true;

			cursor.AdvanceTo(buffer.Start, buffer.End);
			if (cursor.IsCompleted)
				break;
		}
	}

	/// <summary>
	/// Returns the next complete top-level value (<paramref name="reachedEnd"/> false) or signals the end of the body
	/// (<paramref name="reachedEnd"/> true). Returns false when more bytes are required; the caller refills and re-enters
	/// with <paramref name="rs"/> preserved so a value spanning a refill resumes from the same point.
	/// </summary>
	private static bool TryReadNextValue(ref ReadOnlySequence<byte> buffer, bool isFinalBlock, ref NdjsonReaderState rs, out ReadOnlySequence<byte> value, out bool reachedEnd)
	{
		value = default;
		reachedEnd = false;

		while (true)
		{
			if (rs.Shape == NdjsonShape.Unknown)
			{
				var probe = new Utf8JsonReader(buffer, isFinalBlock, rs.JsonState);
				if (!probe.Read())
					return false;

				if (probe.TokenType == JsonTokenType.StartArray)
				{
					rs.Shape = NdjsonShape.Array;
					rs.JsonState = probe.CurrentState;
					buffer = buffer.Slice(probe.Position);
				}
				else
				{
					// Multi-value NDJSON: the first value starts at the current position, so do not advance.
					rs.Shape = NdjsonShape.MultiValue;
				}

				continue;
			}

			var stateBeforeValue = rs.JsonState;
			var bufferBeforeValue = buffer;
			var reader = new Utf8JsonReader(buffer, isFinalBlock, rs.JsonState);

			if (!reader.Read())
			{
				// No token in the remaining bytes: end of stream on the final block, otherwise we need more.
				// (For the array form an unterminated array also ends here, matching the buffered converter's
				// `while (TokenType is not EndArray) { if (!Read()) break; }`.)
				if (isFinalBlock)
				{
					reachedEnd = true;
					return true;
				}

				rs.JsonState = stateBeforeValue;
				buffer = bufferBeforeValue;
				return false;
			}

			if (rs.Shape == NdjsonShape.Array && reader.TokenType == JsonTokenType.EndArray)
			{
				rs.JsonState = reader.CurrentState;
				buffer = buffer.Slice(reader.Position);
				reachedEnd = true;
				return true;
			}

			// Positioned on the value's first token. Probe completeness on a clone so the value is only sliced once
			// the whole scope is present; the original reader stays put for the restore-and-refill path.
			var valueStart = buffer.GetPosition(reader.TokenStartIndex);
			var clone = reader;
			if (!clone.TrySkip())
			{
				rs.JsonState = stateBeforeValue;
				buffer = bufferBeforeValue;
				return false;
			}

			var valueEnd = clone.Position;
			value = buffer.Slice(valueStart, valueEnd);
			rs.JsonState = clone.CurrentState;
			buffer = buffer.Slice(valueEnd);
			return true;
		}
	}

	private interface IBufferCursor
	{
		ReadOnlySequence<byte> Buffer { get; }
		bool IsCompleted { get; }
		void AdvanceTo(SequencePosition consumed, SequencePosition examined);
	}

	private interface ISyncBufferCursor : IBufferCursor
	{
		bool Read();
	}

	private interface IAsyncBufferCursor : IBufferCursor
	{
		ValueTask<bool> ReadAsync(CancellationToken cancellationToken);
	}

	private sealed class SyncStreamBufferCursor(StreamBuffer buffer) : ISyncBufferCursor
	{
		public ReadOnlySequence<byte> Buffer => buffer.Buffer;
		public bool IsCompleted => buffer.IsCompleted;
		public bool Read() => buffer.Read();
		public void AdvanceTo(SequencePosition consumed, SequencePosition examined) => buffer.AdvanceTo(consumed, examined);
	}

#if NET10_0_OR_GREATER
	private sealed class PipeReaderCursor(PipeReader pipeReader) : IAsyncBufferCursor
	{
		private ReadResult _result;

		public ReadOnlySequence<byte> Buffer => _result.Buffer;
		public bool IsCompleted => _result.IsCompleted;

		public async ValueTask<bool> ReadAsync(CancellationToken cancellationToken)
		{
			_result = await pipeReader.ReadAsync(cancellationToken).ConfigureAwait(false);
			return !_result.Buffer.IsEmpty || !_result.IsCompleted;
		}

		public void AdvanceTo(SequencePosition consumed, SequencePosition examined) => pipeReader.AdvanceTo(consumed, examined);
	}
#else
	private sealed class AsyncStreamCursor(StreamBuffer buffer) : IAsyncBufferCursor
	{
		public ReadOnlySequence<byte> Buffer => buffer.Buffer;
		public bool IsCompleted => buffer.IsCompleted;
		public ValueTask<bool> ReadAsync(CancellationToken cancellationToken) => buffer.ReadAsync(cancellationToken);
		public void AdvanceTo(SequencePosition consumed, SequencePosition examined) => buffer.AdvanceTo(consumed, examined);
	}
#endif

	/// <summary>
	/// A growable pooled buffer over a <see cref="Stream"/>. Compacts consumed bytes to offset 0 on every
	/// <see cref="AdvanceTo"/> so the live region stays single-segment (the converters read <c>ValueSpan</c>, which
	/// requires a contiguous span) and so <c>consumed.GetInteger()</c> is a plain array offset.
	/// </summary>
	private sealed class StreamBuffer : IDisposable
	{
		private const int MinimumReadSize = 4096;

		private readonly Stream _stream;
		private byte[] _buffer;
		private int _filled;
		private bool _streamCompleted;
		private bool _disposed;

		public StreamBuffer(Stream stream, int bufferSize)
		{
			_stream = stream;
			_buffer = ArrayPool<byte>.Shared.Rent(bufferSize <= 0 ? DefaultBufferSize : bufferSize);
		}

		public ReadOnlySequence<byte> Buffer => new(_buffer, 0, _filled);

		public bool IsCompleted => _streamCompleted;

		public bool Read()
		{
			if (_streamCompleted)
				return _filled > 0;

			if (_buffer.Length - _filled < MinimumReadSize)
				Grow();

			var n = _stream.Read(_buffer, _filled, _buffer.Length - _filled);
			if (n == 0)
			{
				_streamCompleted = true;
				return _filled > 0;
			}

			_filled += n;
			return true;
		}

		public async ValueTask<bool> ReadAsync(CancellationToken cancellationToken)
		{
			if (_streamCompleted)
				return _filled > 0;

			if (_buffer.Length - _filled < MinimumReadSize)
				Grow();

#if NETSTANDARD2_1_OR_GREATER || NET8_0_OR_GREATER
			var n = await _stream.ReadAsync(_buffer.AsMemory(_filled, _buffer.Length - _filled), cancellationToken).ConfigureAwait(false);
#else
			var n = await _stream.ReadAsync(_buffer, _filled, _buffer.Length - _filled, cancellationToken).ConfigureAwait(false);
#endif
			if (n == 0)
			{
				_streamCompleted = true;
				return _filled > 0;
			}

			_filled += n;
			return true;
		}

		public void AdvanceTo(SequencePosition consumed, SequencePosition examined)
		{
			var c = consumed.GetInteger();
			if (c <= 0)
				return;

			System.Buffer.BlockCopy(_buffer, c, _buffer, 0, _filled - c);
			_filled -= c;
		}

		private void Grow()
		{
			var next = ArrayPool<byte>.Shared.Rent(_buffer.Length * 2);
			System.Buffer.BlockCopy(_buffer, 0, next, 0, _filled);
			ArrayPool<byte>.Shared.Return(_buffer);
			_buffer = next;
		}

		public void Dispose()
		{
			if (_disposed)
				return;

			_disposed = true;
			ArrayPool<byte>.Shared.Return(_buffer);
		}
	}
}
