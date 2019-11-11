#nullable enable
using System;
using System.Buffers;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static Elasticsearch.Net.SerializationFormatting;

namespace Elasticsearch.Net
{
	public class SystemTextJsonSerializer : IElasticsearchSerializer
	{
		private readonly JsonSerializerOptions _indented = new JsonSerializerOptions { WriteIndented = true };
		private readonly JsonSerializerOptions _none = new JsonSerializerOptions { WriteIndented = false };

		public object Deserialize(Type type, Stream stream)
		{
			using var reader = new Utf8JsonStreamReader(stream, 32 * 1024);
			return reader.Deserialize(type, _none);
		}

		public T Deserialize<T>(Stream stream)
		{
			using var reader = new Utf8JsonStreamReader(stream, 32 * 1024);
			return reader.Deserialize<T>(_none);
		}

		public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = None)
		{
			using var writer = new Utf8JsonWriter(stream);
			JsonSerializer.Serialize<T>(writer, data, GetFormatting(formatting));
		}

		private JsonSerializerOptions GetFormatting(SerializationFormatting formatting) => formatting == None ? _none : _indented;

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync(stream, type, _none, cancellationToken).AsTask();

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync<T>(stream, _none, cancellationToken).AsTask();

		public Task SerializeAsync<T>(
			T data, Stream stream, SerializationFormatting formatting = None, CancellationToken cancellationToken = default
		) =>
			JsonSerializer.SerializeAsync<T>(stream, data, GetFormatting(formatting), cancellationToken);
	}


	// https://stackoverflow.com/a/55429664/47020
	// https://github.com/dotnet/corefx/issues/39655

	// Slightly modified to include non generic serialize

	// I suspect due to the impure nature a lot of defensive copies will be allocated
	// This can be totally fine though

	internal ref struct Utf8JsonStreamReader
	{
		private readonly Stream _stream;
		private readonly int _bufferSize;

		private SequenceSegment? _firstSegment;
		private int _firstSegmentStartIndex;
		private SequenceSegment? _lastSegment;
		private int _lastSegmentEndIndex;

		private Utf8JsonReader _jsonReader;
		private bool _keepBuffers;
		private bool _isFinalBlock;

		public Utf8JsonStreamReader(Stream stream, int bufferSize)
		{
			_stream = stream;
			_bufferSize = bufferSize;

			_firstSegment = null;
			_firstSegmentStartIndex = 0;
			_lastSegment = null;
			_lastSegmentEndIndex = -1;

			_jsonReader = default;
			_keepBuffers = false;
			_isFinalBlock = false;
		}

		public bool Read()
		{
			// read could be unsuccessful due to insufficient bufer size, retrying in loop with additional buffer segments
			while (!_jsonReader.Read())
			{
				if (_isFinalBlock)
					return false;

				MoveNext();
			}

			return true;
		}

		private void MoveNext()
		{
			var firstSegment = _firstSegment;
			_firstSegmentStartIndex += (int)_jsonReader.BytesConsumed;

			// release previous segments if possible
			if (!_keepBuffers)
			{
				while (firstSegment?.Memory.Length <= _firstSegmentStartIndex)
				{
					_firstSegmentStartIndex -= firstSegment.Memory.Length;
					firstSegment.Dispose();
					firstSegment = (SequenceSegment?)firstSegment.Next;
				}
			}

			// create new segment
			var newSegment = new SequenceSegment(_bufferSize, _lastSegment);

			if (firstSegment != null)
			{
				_firstSegment = firstSegment;
				newSegment.Previous = _lastSegment;
				_lastSegment?.SetNext(newSegment);
				_lastSegment = newSegment;
			}
			else
			{
				_firstSegment = _lastSegment = newSegment;
				_firstSegmentStartIndex = 0;
			}

			// read data from stream
#if NETSTANDARD2_1
			_lastSegmentEndIndex = _stream.Read(newSegment.Buffer.Memory.Span);
#else
			_lastSegmentEndIndex = Read(newSegment.Buffer.Memory.Span);
#endif
			_isFinalBlock = _lastSegmentEndIndex < newSegment.Buffer.Memory.Length;
			_jsonReader = new Utf8JsonReader(new ReadOnlySequence<byte>(_firstSegment, _firstSegmentStartIndex, _lastSegment, _lastSegmentEndIndex),
				_isFinalBlock, _jsonReader.CurrentState);
		}

        public int Read(Span<byte> buffer)
        {
            var sharedBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length);
            try
            {
                var numRead = _stream.Read(sharedBuffer, 0, buffer.Length);
                if ((uint)numRead > (uint)buffer.Length)
                    throw new IOException("Read more then the buffer allocated for");
                new Span<byte>(sharedBuffer, 0, numRead).CopyTo(buffer);
                return numRead;
            }
            finally { ArrayPool<byte>.Shared.Return(sharedBuffer); }
        }

		public T Deserialize<T>(JsonSerializerOptions? options = null)
		{
			var firstSegment = ToJsonReader(out var newJsonReader);

			// deserialize value
			var result = JsonSerializer.Deserialize<T>(ref newJsonReader, options);

			ReleaseMemoryIfPossible(firstSegment);
			return result;
		}
		public object Deserialize(Type type, JsonSerializerOptions? options = null)
		{
			var firstSegment = ToJsonReader(out var newJsonReader);

			// deserialize value
			var result = JsonSerializer.Deserialize(ref newJsonReader, type, options);

			ReleaseMemoryIfPossible(firstSegment);
			return result;
		}

		private void ReleaseMemoryIfPossible(SequenceSegment? firstSegment)
		{
			// release memory if possible
			var firstSegmentStartIndex = _firstSegmentStartIndex + (int)_jsonReader.BytesConsumed;

			while (firstSegment?.Memory.Length < firstSegmentStartIndex)
			{
				firstSegmentStartIndex -= firstSegment.Memory.Length;
				firstSegment.Dispose();
				firstSegment = (SequenceSegment?)firstSegment.Next;
			}

			if (firstSegment == _firstSegment) return;

			_firstSegment = firstSegment;
			_firstSegmentStartIndex = firstSegmentStartIndex;
			_jsonReader = new Utf8JsonReader(
				new ReadOnlySequence<byte>(_firstSegment!, _firstSegmentStartIndex, _lastSegment!, _lastSegmentEndIndex), _isFinalBlock,
				_jsonReader.CurrentState);
		}

		private SequenceSegment? ToJsonReader(out Utf8JsonReader newJsonReader)
		{
			// JsonSerializer.Deserialize can read only a single object. We have to extract
			// object to be deserialized into separate Utf8JsonReader. This incures one additional
			// pass through data (but data is only passed, not parsed).
			var tokenStartIndex = _jsonReader.TokenStartIndex;
			var firstSegment = _firstSegment;
			var firstSegmentStartIndex = _firstSegmentStartIndex;

			// loop through data until end of object is found
			_keepBuffers = true;
			var depth = 0;

			if (TokenType == JsonTokenType.StartObject || TokenType == JsonTokenType.StartArray)
				depth++;

			while (depth > 0 && Read())
			{
				if (TokenType == JsonTokenType.StartObject || TokenType == JsonTokenType.StartArray)
					depth++;
				else if (TokenType == JsonTokenType.EndObject || TokenType == JsonTokenType.EndArray)
					depth--;
			}

			_keepBuffers = false;

			// end of object found, extract json reader for deserializer
			newJsonReader = new Utf8JsonReader(
				new ReadOnlySequence<byte>(firstSegment!, firstSegmentStartIndex, _lastSegment!, _lastSegmentEndIndex).Slice(tokenStartIndex,
					_jsonReader.Position), true, default);
			return firstSegment;
		}

		public void Dispose() => _lastSegment?.Dispose();

		public int CurrentDepth => _jsonReader.CurrentDepth;
		public bool HasValueSequence => _jsonReader.HasValueSequence;
		public long TokenStartIndex => _jsonReader.TokenStartIndex;
		public JsonTokenType TokenType => _jsonReader.TokenType;
		public ReadOnlySequence<byte> ValueSequence => _jsonReader.ValueSequence;
		public ReadOnlySpan<byte> ValueSpan => _jsonReader.ValueSpan;

		public bool GetBoolean() => _jsonReader.GetBoolean();

		public byte GetByte() => _jsonReader.GetByte();

		public byte[] GetBytesFromBase64() => _jsonReader.GetBytesFromBase64();

		public string GetComment() => _jsonReader.GetComment();

		public DateTime GetDateTime() => _jsonReader.GetDateTime();

		public DateTimeOffset GetDateTimeOffset() => _jsonReader.GetDateTimeOffset();

		public decimal GetDecimal() => _jsonReader.GetDecimal();

		public double GetDouble() => _jsonReader.GetDouble();

		public Guid GetGuid() => _jsonReader.GetGuid();

		public short GetInt16() => _jsonReader.GetInt16();

		public int GetInt32() => _jsonReader.GetInt32();

		public long GetInt64() => _jsonReader.GetInt64();

		public sbyte GetSByte() => _jsonReader.GetSByte();

		public float GetSingle() => _jsonReader.GetSingle();

		public string GetString() => _jsonReader.GetString();

		public uint GetUInt32() => _jsonReader.GetUInt32();

		public ulong GetUInt64() => _jsonReader.GetUInt64();

		public bool TryGetDecimal(out byte value) => _jsonReader.TryGetByte(out value);

		public bool TryGetBytesFromBase64(out byte[] value) => _jsonReader.TryGetBytesFromBase64(out value);

		public bool TryGetDateTime(out DateTime value) => _jsonReader.TryGetDateTime(out value);

		public bool TryGetDateTimeOffset(out DateTimeOffset value) => _jsonReader.TryGetDateTimeOffset(out value);

		public bool TryGetDecimal(out decimal value) => _jsonReader.TryGetDecimal(out value);

		public bool TryGetDouble(out double value) => _jsonReader.TryGetDouble(out value);

		public bool TryGetGuid(out Guid value) => _jsonReader.TryGetGuid(out value);

		public bool TryGetInt16(out short value) => _jsonReader.TryGetInt16(out value);

		public bool TryGetInt32(out int value) => _jsonReader.TryGetInt32(out value);

		public bool TryGetInt64(out long value) => _jsonReader.TryGetInt64(out value);

		public bool TryGetSByte(out sbyte value) => _jsonReader.TryGetSByte(out value);

		public bool TryGetSingle(out float value) => _jsonReader.TryGetSingle(out value);

		public bool TryGetUInt16(out ushort value) => _jsonReader.TryGetUInt16(out value);

		public bool TryGetUInt32(out uint value) => _jsonReader.TryGetUInt32(out value);

		public bool TryGetUInt64(out ulong value) => _jsonReader.TryGetUInt64(out value);

		private sealed class SequenceSegment : ReadOnlySequenceSegment<byte>, IDisposable
		{
			internal IMemoryOwner<byte> Buffer { get; }
			internal SequenceSegment? Previous { get; set; }
			private bool _disposed;

			public SequenceSegment(int size, SequenceSegment? previous)
			{
				Buffer = MemoryPool<byte>.Shared.Rent(size);
				Previous = previous;

				Memory = Buffer.Memory;
				RunningIndex = previous?.RunningIndex + previous?.Memory.Length ?? 0;
			}

			public void SetNext(SequenceSegment next) => Next = next;

			public void Dispose()
			{
				if (!_disposed)
				{
					_disposed = true;
					Buffer.Dispose();
					Previous?.Dispose();
				}
			}
		}
	}
}
