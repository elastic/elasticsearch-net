﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	// ReSharper disable once UnusedTypeParameter
	public interface IPostData<out T>
	{
		void Write(Stream writableStream, IConnectionConfigurationValues settings);

		Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken token);
	}

	public enum PostType
	{
		ByteArray,
#if NETSTANDARD2_1
		ReadOnlyMemory,
#endif
		LiteralString,
		EnumerableOfString,
		EnumerableOfObject,
		StreamHandler,
		Serializable
	}

	public abstract class PostData
	{
		protected const int BufferSize = 81920;
		protected const string NewLineString = "\n";
		protected static readonly byte[] NewLineByteArray = { (byte)'\n' };

		//TODO internal set?;
		public bool? DisableDirectStreaming { get; set; }
		public PostType Type { get; protected set; }
		public byte[] WrittenBytes { get; protected set; }

		public static PostData Empty => new PostData<object>(string.Empty);

		public abstract void Write(Stream writableStream, IConnectionConfigurationValues settings);

		public abstract Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken);

		public static implicit operator PostData(byte[] byteArray) => Bytes(byteArray);

		public static implicit operator PostData(string literalString) => String(literalString);

		public static SerializableData<T> Serializable<T>(T o) => new SerializableData<T>(o);

		public static PostData MultiJson(IEnumerable<string> listOfString) => new PostData<object>(listOfString);

		public static PostData MultiJson(IEnumerable<object> listOfObjects) => new PostData<object>(listOfObjects);

		public static PostData Bytes(byte[] bytes) => new PostData<object>(bytes);

#if NETSTANDARD2_1
		public static PostData ReadOnlyMemory(ReadOnlyMemory<byte> bytes) => new PostData<object>(bytes);
#endif

		public static PostData String(string serializedString) => new PostData<object>(serializedString);

		public static PostData StreamHandler(Action<Stream> syncWriter, Func<Stream, CancellationToken, Task> asyncWriter) =>
			new PostData<object>(syncWriter, asyncWriter);
	}

	public class PostData<T> : PostData, IPostData<T>
	{
		private readonly Action<Stream> _syncWriter;
		private readonly Func<Stream, CancellationToken, Task> _asyncWriter;
		private readonly IEnumerable<object> _enumerableOfObject;
		private readonly IEnumerable<string> _enumerableOfStrings;
		private readonly string _literalString;
#if NETSTANDARD2_1
		private readonly ReadOnlyMemory<byte> _memoryOfBytes;
#endif

		protected internal PostData(byte[] item)
		{
			WrittenBytes = item;
			Type = PostType.ByteArray;
		}

#if NETSTANDARD2_1
		protected internal PostData(ReadOnlyMemory<byte> item)
		{
			_memoryOfBytes = item;
			Type = PostType.ReadOnlyMemory;
		}
#endif

		protected internal PostData(string item)
		{
			_literalString = item;
			Type = PostType.LiteralString;
		}

		protected internal PostData(IEnumerable<string> item)
		{
			_enumerableOfStrings = item;
			Type = PostType.EnumerableOfString;
		}

		protected internal PostData(IEnumerable<object> item)
		{
			_enumerableOfObject = item;
			Type = PostType.EnumerableOfObject;
		}

		protected internal PostData(Action<Stream> syncWriter, Func<Stream, CancellationToken, Task> asyncWriter)
		{
			const string message = "PostData.StreamHandler needs to handle both synchronous and async paths";
			_syncWriter = syncWriter ?? throw new ArgumentNullException(nameof(syncWriter), message);
			_asyncWriter = asyncWriter ?? throw new ArgumentNullException(nameof(asyncWriter), message);
			if (_syncWriter == null || _asyncWriter == null)
				throw new ArgumentNullException();
		}

		private static void BufferIfNeeded(IConnectionConfigurationValues settings, bool disableDirectStreaming, ref MemoryStream buffer,
			ref Stream stream
		)
		{
			if (!disableDirectStreaming) return;

			buffer = settings.MemoryStreamFactory.Create();
			stream = buffer;
		}

		public override void Write(Stream writableStream, IConnectionConfigurationValues settings)
		{
			MemoryStream buffer = null;
			var stream = writableStream;
			var disableDirectStreaming = DisableDirectStreaming ?? settings.DisableDirectStreaming;

			switch (Type)
			{
				case PostType.ByteArray:
					if (WrittenBytes == null) return;

					if (!disableDirectStreaming)
						stream.Write(WrittenBytes, 0, WrittenBytes.Length);
					else
						buffer = settings.MemoryStreamFactory.Create(WrittenBytes);
					break;

#if NETSTANDARD2_1
				case PostType.ReadOnlyMemory:
					if (_memoryOfBytes.IsEmpty) return;

					if (!disableDirectStreaming)
						stream.Write(_memoryOfBytes.Span);
					else
					{
						WrittenBytes ??= _memoryOfBytes.Span.ToArray();
						buffer = settings.MemoryStreamFactory.Create(WrittenBytes);
					}
					break;
#endif

				case PostType.LiteralString:
					if (string.IsNullOrEmpty(_literalString)) return;

					var stringBytes = WrittenBytes ?? _literalString.Utf8Bytes();
					WrittenBytes ??= stringBytes;
					if (!disableDirectStreaming)
						stream.Write(stringBytes, 0, stringBytes.Length);
					else
						buffer = settings.MemoryStreamFactory.Create(stringBytes);
					break;

				case PostType.EnumerableOfString:
					if (!_enumerableOfStrings.HasAny()) return;

					BufferIfNeeded(settings, disableDirectStreaming, ref buffer, ref stream);
					foreach (var s in _enumerableOfStrings)
					{
						var bytes = s.Utf8Bytes();
						stream.Write(bytes, 0, bytes.Length);
						stream.Write(NewLineByteArray, 0, 1);
					}
					break;

				case PostType.EnumerableOfObject:
					if (!_enumerableOfObject.HasAny()) return;

					BufferIfNeeded(settings, disableDirectStreaming, ref buffer, ref stream);
					foreach (var o in _enumerableOfObject)
					{
						settings.RequestResponseSerializer.Serialize(o, stream, SerializationFormatting.None);
						stream.Write(NewLineByteArray, 0, 1);
					}
					break;

				case PostType.StreamHandler:
					BufferIfNeeded(settings, disableDirectStreaming, ref buffer, ref stream);
					_syncWriter(stream);
					break;

				case PostType.Serializable:
					throw new Exception("PostData is not expected/capable to handle contain serializable, use SerializableData instead");

				default:
					throw new ArgumentOutOfRangeException();
			}
			if (buffer == null || !disableDirectStreaming) return;

			buffer.Position = 0;
			buffer.CopyTo(writableStream, BufferSize);
			WrittenBytes ??= buffer.ToArray();
		}

		public override async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken)
		{
			MemoryStream buffer = null;
			var stream = writableStream;
			var disableDirectStreaming = DisableDirectStreaming ?? settings.DisableDirectStreaming;

			switch (Type)
			{
				case PostType.ByteArray:
					if (!disableDirectStreaming)
						await stream.WriteAsync(WrittenBytes, 0, WrittenBytes.Length, cancellationToken).ConfigureAwait(false);
					else
						buffer = settings.MemoryStreamFactory.Create(WrittenBytes);
					break;

#if NETSTANDARD2_1
				case PostType.ReadOnlyMemory:
					if (_memoryOfBytes.IsEmpty) return;

					if (!disableDirectStreaming)
						stream.Write(_memoryOfBytes.Span);
					else
					{
						WrittenBytes ??= _memoryOfBytes.Span.ToArray();
						buffer = settings.MemoryStreamFactory.Create(WrittenBytes);
					}
					break;
#endif

				case PostType.LiteralString:
					if (string.IsNullOrEmpty(_literalString)) return;

					var stringBytes = WrittenBytes ?? _literalString.Utf8Bytes();
					WrittenBytes ??= stringBytes;
					if (!disableDirectStreaming)
						await stream.WriteAsync(stringBytes, 0, stringBytes.Length, cancellationToken).ConfigureAwait(false);
					else
						buffer = settings.MemoryStreamFactory.Create(stringBytes);
					break;

				case PostType.EnumerableOfString:
					if (!_enumerableOfStrings.HasAny()) return;

					BufferIfNeeded(settings, disableDirectStreaming, ref buffer, ref stream);
					foreach (var s in _enumerableOfStrings)
					{
						var bytes = s.Utf8Bytes();
						await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
						await stream.WriteAsync(NewLineByteArray, 0, 1, cancellationToken).ConfigureAwait(false);
					}
					break;

				case PostType.EnumerableOfObject:
					if (!_enumerableOfObject.HasAny()) return;

					BufferIfNeeded(settings, disableDirectStreaming, ref buffer, ref stream);
					foreach (var o in _enumerableOfObject)
					{
						await settings.RequestResponseSerializer.SerializeAsync(o, stream, SerializationFormatting.None, cancellationToken)
							.ConfigureAwait(false);
						await stream.WriteAsync(NewLineByteArray, 0, 1, cancellationToken).ConfigureAwait(false);
					}
					break;

				case PostType.StreamHandler:
					BufferIfNeeded(settings, disableDirectStreaming, ref buffer, ref stream);
					await _asyncWriter(stream, cancellationToken).ConfigureAwait(false);
					break;

				case PostType.Serializable:
					throw new Exception("PostData is not expected/capable to handle contain serializable, use SerializableData instead");

				default:
					throw new ArgumentOutOfRangeException();
			}

			if (buffer == null || !disableDirectStreaming) return;

			buffer.Position = 0;
			await buffer.CopyToAsync(writableStream, BufferSize, cancellationToken).ConfigureAwait(false);
			WrittenBytes ??= buffer.ToArray();
		}
	}
}
