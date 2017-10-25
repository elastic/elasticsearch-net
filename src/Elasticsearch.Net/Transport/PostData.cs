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
		private const int BufferSize = 81920;
		private const string NewLineString = "\n";
		private static readonly byte[] NewLineByteArray = { (byte)'\n' };
		private readonly string _literalString;
		private readonly IEnumerable<string> _enumurabeOfStrings;
		private readonly IEnumerable<object> _enumerableOfObject;
		private readonly T _serializable;

		public bool? DisableDirectStreaming { get; set; }
		public byte[] WrittenBytes { get; private set; }
		public PostType Type { get; }

		public PostData(byte[] item) { WrittenBytes = item; Type = PostType.ByteArray; }
		public PostData(string item) { _literalString = item; Type = PostType.LiteralString; }
		public PostData(IEnumerable<string> item) { _enumurabeOfStrings = item; Type = PostType.EnumerableOfString; }
		public PostData(IEnumerable<object> item) { _enumerableOfObject = item; Type = PostType.EnumerableOfObject; }
		public PostData(T item)
		{
			var boxedType = item.GetType();
			if (typeof(byte[]).AssignableFrom(boxedType))
			{
				WrittenBytes = item as byte[]; Type = PostType.ByteArray;
			}
			else if (typeof(string).AssignableFrom(boxedType))
			{
				_literalString = item as string; Type = PostType.LiteralString;
			}
			else if (typeof(IEnumerable<string>).AssignableFrom(boxedType))
			{
				_enumurabeOfStrings = (IEnumerable<string>)item; Type = PostType.EnumerableOfString;
			}
			else if (typeof(IEnumerable<object>).AssignableFrom(boxedType))
			{
				_enumerableOfObject = (IEnumerable<object>)item; Type = PostType.EnumerableOfObject;
			}
			else
			{
				_serializable = item; Type = PostType.Serializable;
			}
		}

		public void Write(Stream writableStream, IConnectionConfigurationValues settings)
		{
			var indent = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			MemoryStream ms = null;
			Stream stream = null;
			switch (Type)
			{
				case PostType.ByteArray: 
					ms = new MemoryStream(WrittenBytes);
					break;
				case PostType.LiteralString:
					ms = !string.IsNullOrEmpty(_literalString) ? new MemoryStream(_literalString?.Utf8Bytes()) : null;
					break;
				case PostType.EnumerableOfString: 
					ms = _enumurabeOfStrings.HasAny() ? new MemoryStream((string.Join(NewLineString, _enumurabeOfStrings) + NewLineString).Utf8Bytes()) : null;
					break;
				case PostType.EnumerableOfObject:
					if (!_enumerableOfObject.HasAny()) return;

					if (this.DisableDirectStreaming ?? settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
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
					stream = writableStream;
					if (this.DisableDirectStreaming ?? settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					settings.RequestResponseSerializer.Serialize(this._serializable, stream, indent);
					break;
			}
			if (ms != null)
			{
				ms.Position = 0;
				ms.CopyTo(writableStream, BufferSize);
			}
			if (this.Type != 0)
				this.WrittenBytes = ms?.ToArray();
		}

		public async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken)
		{
			var indent = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			MemoryStream ms = null; Stream stream = null;
			switch (Type)
			{
				case PostType.ByteArray: 
					ms = new MemoryStream(WrittenBytes);
					break;
				case PostType.LiteralString: 
					ms = !string.IsNullOrEmpty(_literalString) ? new MemoryStream(_literalString.Utf8Bytes()) : null;
					break;
				case PostType.EnumerableOfString: 
					ms = _enumurabeOfStrings.HasAny() ? new MemoryStream((string.Join(NewLineString, _enumurabeOfStrings) + NewLineString).Utf8Bytes()) : null;
					break;
				case PostType.EnumerableOfObject:
					if (!_enumerableOfObject.HasAny()) return;
					if (this.DisableDirectStreaming ?? settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					else stream = writableStream;
					foreach (var o in _enumerableOfObject)
					{
						settings.RequestResponseSerializer.Serialize(o, stream, SerializationFormatting.None);
						await stream.WriteAsync(NewLineByteArray, 0, 1, cancellationToken).ConfigureAwait(false);
					}
					break;
				case PostType.Serializable: 
					stream = writableStream;
					if (this.DisableDirectStreaming ?? settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					settings.RequestResponseSerializer.Serialize(this._serializable, stream, indent);
					break;
			}
			if (ms != null)
			{
				ms.Position = 0;
				await ms.CopyToAsync(writableStream, BufferSize, cancellationToken).ConfigureAwait(false);
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
