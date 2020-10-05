// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
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

		public static PostData StreamHandler<T>(T state, Action<T, Stream> syncWriter, Func<T, Stream, CancellationToken, Task> asyncWriter) =>
			new StreamableData<T>(state, syncWriter, asyncWriter);

		protected void BufferIfNeeded(IConnectionConfigurationValues settings, ref MemoryStream buffer, ref Stream stream)
		{
			var disableDirectStreaming = DisableDirectStreaming ?? settings.DisableDirectStreaming;
			if (!disableDirectStreaming) return;

			buffer = settings.MemoryStreamFactory.Create();
			stream = buffer;
		}

		protected void FinishStream(Stream writableStream, MemoryStream buffer, IConnectionConfigurationValues settings)
		{
			var disableDirectStreaming = DisableDirectStreaming ?? settings.DisableDirectStreaming;
			if (buffer == null || !disableDirectStreaming) return;

			buffer.Position = 0;
			buffer.CopyTo(writableStream, BufferSize);
			WrittenBytes ??= buffer.ToArray();
		}

		protected async
#if NETSTANDARD2_1
			ValueTask
			#else
			Task
#endif
			FinishStreamAsync(Stream writableStream, MemoryStream buffer, IConnectionConfigurationValues settings, CancellationToken ctx)
		{
			var disableDirectStreaming = DisableDirectStreaming ?? settings.DisableDirectStreaming;
			if (buffer == null || !disableDirectStreaming) return;

			buffer.Position = 0;
			await buffer.CopyToAsync(writableStream, BufferSize, ctx).ConfigureAwait(false);
			WrittenBytes ??= buffer.ToArray();
		}
	}

	public class PostData<T> : PostData, IPostData<T>
	{
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
				{
					if (_enumerableOfStrings == null)
						return;

					using var enumerator = _enumerableOfStrings.GetEnumerator();
					if (!enumerator.MoveNext())
						return;

					BufferIfNeeded(settings, ref buffer, ref stream);
					do
					{
						var bytes = enumerator.Current.Utf8Bytes();
						stream.Write(bytes, 0, bytes.Length);
						stream.Write(NewLineByteArray, 0, 1);
					} while (enumerator.MoveNext());
					break;
				}
				case PostType.EnumerableOfObject:
				{
					if (_enumerableOfObject == null)
						return;

					using var enumerator = _enumerableOfObject.GetEnumerator();
					if (!enumerator.MoveNext())
						return;

					BufferIfNeeded(settings, ref buffer, ref stream);
					do
					{
						var o = enumerator.Current;
						settings.RequestResponseSerializer.Serialize(o, stream, SerializationFormatting.None);
						stream.Write(NewLineByteArray, 0, 1);
					} while (enumerator.MoveNext());
					break;
				}
				case PostType.StreamHandler:
					var streamHandlerException = $"{nameof(PostData)} cannot handle {nameof(PostType.StreamHandler)} data. "
						+ $"Use {typeof(StreamableData<>).FullName} through {nameof(PostData)}.{nameof(StreamHandler)}<T>() for streamable data";
					throw new Exception(streamHandlerException);
				case PostType.Serializable:
					var serializableException = $"{nameof(PostData)} cannot handle {nameof(PostType.Serializable)} data. "
						+ $"Use {typeof(SerializableData<>).FullName} through {nameof(PostData)}.{nameof(Serializable)}<T>() for serializable data";
					throw new Exception(serializableException);

				default:
					throw new ArgumentOutOfRangeException();
			}

			FinishStream(writableStream, buffer, settings);

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
				{
					if (_enumerableOfStrings == null)
						return;

					using var enumerator = _enumerableOfStrings.GetEnumerator();
					if (!enumerator.MoveNext())
						return;

					BufferIfNeeded(settings, ref buffer, ref stream);
					do
					{
						var bytes = enumerator.Current.Utf8Bytes();
						await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
						await stream.WriteAsync(NewLineByteArray, 0, 1, cancellationToken).ConfigureAwait(false);
					} while (enumerator.MoveNext());
					break;
				}
				case PostType.EnumerableOfObject:
				{
					if (_enumerableOfObject == null)
						return;

					using var enumerator = _enumerableOfObject.GetEnumerator();
					if (!enumerator.MoveNext())
						return;

					BufferIfNeeded(settings, ref buffer, ref stream);
					do
					{
						var o = enumerator.Current;
						await settings.RequestResponseSerializer.SerializeAsync(o, stream, SerializationFormatting.None, cancellationToken)
							.ConfigureAwait(false);
						await stream.WriteAsync(NewLineByteArray, 0, 1, cancellationToken).ConfigureAwait(false);
					} while (enumerator.MoveNext());
					break;
				}
				case PostType.StreamHandler:
					throw new Exception("PostData is not expected/capable to handle streamable data, use StreamableData instead");

				case PostType.Serializable:
					throw new Exception("PostData is not expected/capable to handle contain serializable, use SerializableData instead");

				default:
					throw new ArgumentOutOfRangeException();
			}

			await FinishStreamAsync(writableStream, buffer, settings, cancellationToken).ConfigureAwait(false);
		}

	}
}
