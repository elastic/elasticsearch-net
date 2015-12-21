using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface IPostData
	{
		void Write(Stream writableStream, IConnectionConfigurationValues settings);
	}
	public interface IPostData<out T> : IPostData { }

	public enum PostType
	{
		ByteArray,
		LiteralString,
		EnumerableOfString,
		EnumerableOfObject,
		Serializable
	}

	public class PostData<T> : IPostData<T>
	{
		private readonly string _literalString;
		private readonly IEnumerable<string> _enumurabeOfStrings;
		private readonly IEnumerable<object> _enumerableOfObject;
		private readonly T _serializable;

		public byte[] WrittenBytes { get; private set; }
		public PostType Type { get; }

		public PostData(byte[] item) { WrittenBytes = item; Type = PostType.ByteArray; }
		public PostData(string item) { _literalString = item; Type = PostType.LiteralString; }
		public PostData(IEnumerable<string> item) { _enumurabeOfStrings = item; Type = PostType.EnumerableOfString; }
		public PostData(IEnumerable<object> item) { _enumerableOfObject = item; Type = PostType.EnumerableOfObject; }
		public PostData(T item) { _serializable = item; Type = PostType.Serializable; }

		public void Write(Stream writableStream, IConnectionConfigurationValues settings)
		{
			var indent = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			MemoryStream ms = null; Stream stream = null;
			switch (Type)
			{
				case PostType.ByteArray: 
					ms = new MemoryStream(WrittenBytes);
					break;
				case PostType.LiteralString: 
					ms = new MemoryStream(_literalString?.Utf8Bytes());
					break;
				case PostType.EnumerableOfString: 
					ms = _enumurabeOfStrings.HasAny() ? new MemoryStream((string.Join("\n", _enumurabeOfStrings) + "\n").Utf8Bytes()) : null;
					break;
				case PostType.EnumerableOfObject:
					if (!_enumerableOfObject.HasAny()) return;

					if (settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					else stream = writableStream;
					foreach (var o in _enumerableOfObject)
					{
						settings.Serializer.Serialize(o, stream, indent);
						stream.Write(new byte[] { (byte)'\n' }, 0, 1);
					}
					break;
				case PostType.Serializable: 
					stream = writableStream;
					if (settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					settings.Serializer.Serialize(this._serializable, stream, indent);
					break;
			}
			if (ms != null)
			{
				ms.Position = 0;
				ms.CopyTo(writableStream, 8096);
			}
			if (this.Type != 0)
				this.WrittenBytes = ms?.ToArray();
		}

		public async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			var indent = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			MemoryStream ms = null; Stream stream = null;
			switch (Type)
			{
				case PostType.ByteArray: 
					ms = new MemoryStream(WrittenBytes);
					break;
				case PostType.LiteralString: 
					ms = new MemoryStream(_literalString?.Utf8Bytes());
					break;
				case PostType.EnumerableOfString: 
					ms = _enumurabeOfStrings.HasAny() ? new MemoryStream((string.Join("\n", _enumurabeOfStrings) + "\n").Utf8Bytes()) : null;
					break;
				case PostType.EnumerableOfObject:
					if (!_enumerableOfObject.HasAny()) return;
					if (settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					else stream = writableStream;
					foreach (var o in _enumerableOfObject)
					{
						settings.Serializer.Serialize(o, stream, indent);
						await stream.WriteAsync(new byte[] { (byte)'\n' }, 0, 1, cancellationToken);
					}
					break;
				case PostType.Serializable: 
					stream = writableStream;
					if (settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					settings.Serializer.Serialize(this._serializable, stream, indent);
					break;
			}
			if (ms != null)
			{
				ms.Position = 0;
				await ms.CopyToAsync(writableStream, 8096, cancellationToken);
			}
			if (this.Type != 0)
				this.WrittenBytes = ms?.ToArray();
		}

		public static implicit operator PostData<T>(byte[] byteArray) => new PostData<T>(byteArray);
		public static implicit operator PostData<T>(string literalString) => new PostData<T>(literalString);
		public static implicit operator PostData<T>(List<string> listOfStrings) => new PostData<T>(listOfStrings);
		public static implicit operator PostData<T>(List<object> listOfObjects) => new PostData<T>(listOfObjects);
		public static implicit operator PostData<T>(T @object) => new PostData<T>(@object);
	}

}
