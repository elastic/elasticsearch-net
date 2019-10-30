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
		LiteralString,
		EnumerableOfString,
		EnumerableOfObject,
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

		public static PostData String(string serializedString) => new PostData<object>(serializedString);
	}

	public class PostData<T> : PostData, IPostData<T>
	{
		private readonly IEnumerable<object> _enumerableOfObject;
		private readonly IEnumerable<string> _enumerableOfStrings;
		private readonly string _literalString;

		protected internal PostData(byte[] item)
		{
			WrittenBytes = item;
			Type = PostType.ByteArray;
		}

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
			MemoryStream ms = null;
			switch (Type)
			{
				case PostType.ByteArray:
					ms = settings.MemoryStreamFactory.Create(WrittenBytes);
					break;
				case PostType.LiteralString:
					ms = !string.IsNullOrEmpty(_literalString) ? settings.MemoryStreamFactory.Create(_literalString?.Utf8Bytes()) : null;
					break;
				case PostType.EnumerableOfString:
					ms = _enumerableOfStrings.HasAny()
						? settings.MemoryStreamFactory.Create((string.Join(NewLineString, _enumerableOfStrings) + NewLineString).Utf8Bytes())
						: null;
					break;
				case PostType.EnumerableOfObject:
					if (!_enumerableOfObject.HasAny()) return;

					Stream stream;
					if (DisableDirectStreaming ?? settings.DisableDirectStreaming)
					{
						ms = settings.MemoryStreamFactory.Create();
						stream = ms;
					}
					else stream = writableStream;
					foreach (var o in _enumerableOfObject)
					{
						settings.RequestResponseSerializer.Serialize(o, stream, SerializationFormatting.None);
						stream.Write(NewLineByteArray, 0, 1);
					}
					break;
				case PostType.Serializable:
					throw new Exception("PostData is not expected/capable to handle contain serializable, use SerializableData instead");
			}
			if (ms != null)
			{
				ms.Position = 0;
				ms.CopyTo(writableStream, BufferSize);
			}
			if (Type != 0)
				WrittenBytes = ms?.ToArray();
		}

		public override async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken)
		{
			MemoryStream ms = null;
			switch (Type)
			{
				case PostType.ByteArray:
					ms = settings.MemoryStreamFactory.Create(WrittenBytes);
					break;
				case PostType.LiteralString:
					ms = !string.IsNullOrEmpty(_literalString) ? settings.MemoryStreamFactory.Create(_literalString.Utf8Bytes()) : null;
					break;
				case PostType.EnumerableOfString:
					ms = _enumerableOfStrings.HasAny()
						? settings.MemoryStreamFactory.Create((string.Join(NewLineString, _enumerableOfStrings) + NewLineString).Utf8Bytes())
						: null;
					break;
				case PostType.EnumerableOfObject:
					if (!_enumerableOfObject.HasAny()) return;

					Stream stream;
					if (DisableDirectStreaming ?? settings.DisableDirectStreaming)
					{
						ms = settings.MemoryStreamFactory.Create();
						stream = ms;
					}
					else stream = writableStream;
					foreach (var o in _enumerableOfObject)
					{
						await settings.RequestResponseSerializer.SerializeAsync(o, stream, SerializationFormatting.None, cancellationToken)
							.ConfigureAwait(false);
						await stream.WriteAsync(NewLineByteArray, 0, 1, cancellationToken).ConfigureAwait(false);
					}
					break;
				case PostType.Serializable:
					throw new Exception("PostData is not expected/capable to handle contain serializable, use SerializableData instead");
			}
			if (ms != null)
			{
				ms.Position = 0;
				await ms.CopyToAsync(writableStream, BufferSize, cancellationToken).ConfigureAwait(false);
			}
			if (Type != 0)
				WrittenBytes = ms?.ToArray();
		}
	}
}
