using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Serialization;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Connection.Configuration;
using PurifyNet;
using System.IO;
using System.Collections.Specialized;
using System.Threading;

namespace Elasticsearch.Net.Connection
{
	public interface IPostData
	{
		void Write(Stream writableStream, IConnectionConfigurationValues settings);
	}
	public interface IPostData<out T> : IPostData { }

	public class PostData<T> : IPostData<T>
	{
		protected readonly string LiteralString;
		protected readonly IEnumerable<string> EnumurabeOfStrings;
		protected readonly IEnumerable<object> EnumerableOfObject;
		protected readonly T Serializable;

		public byte[] Bytes { get; private set; }

		readonly int _tag;

		public PostData(byte[] item) { Bytes = item; _tag = 0; }
		public PostData(string item) { LiteralString = item; _tag = 1; }
		public PostData(IEnumerable<string> item) { EnumurabeOfStrings = item; _tag = 2; }
		public PostData(IEnumerable<object> item) { EnumerableOfObject = item; _tag = 3; }
		public PostData(T item) { Serializable = item; _tag = 4; }

		public void Write(Stream writableStream, IConnectionConfigurationValues settings)
		{
			var indent = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			MemoryStream ms = null; Stream stream = null;
			switch (_tag)
			{
				case 0:
					ms = new MemoryStream(Bytes);
					break;
				case 1:
					ms = new MemoryStream(LiteralString?.Utf8Bytes());
					break;
				case 2:
					ms = EnumurabeOfStrings.HasAny() ? new MemoryStream((string.Join("\n", EnumurabeOfStrings) + "\n").Utf8Bytes()) : null;
					break;
				case 3:
					if (!EnumerableOfObject.HasAny()) return;

					if (settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					else stream = writableStream;
					foreach (var o in EnumerableOfObject)
					{
						settings.Serializer.Serialize(o, stream, indent);
						stream.Write(new byte[] { (byte)'\n' }, 0, 1);
					}
					break;
				case 4:
					stream = writableStream;
					if (settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					settings.Serializer.Serialize(this.Serializable, writableStream, indent);
					break;
			}
			if (ms != null)
			{
				ms.Position = 0;
				ms.CopyTo(writableStream, 8096);
			}
			if (this._tag != 0)
				this.Bytes = ms?.ToArray();
		}

		public async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			var indent = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			MemoryStream ms = null; Stream stream = null;
			switch (_tag)
			{
				case 0:
					ms = new MemoryStream(Bytes);
					break;
				case 1:
					ms = new MemoryStream(LiteralString?.Utf8Bytes());
					break;
				case 2:
					ms = EnumurabeOfStrings.HasAny() ? new MemoryStream((string.Join("\n", EnumurabeOfStrings) + "\n").Utf8Bytes()) : null;
					break;
				case 3:
					if (!EnumerableOfObject.HasAny()) return;
					if (settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					else stream = writableStream;
					foreach (var o in EnumerableOfObject)
					{
						settings.Serializer.Serialize(o, stream, indent);
						stream.Write(new byte[] { (byte)'\n' }, 0, 1);
					}
					break;
				case 4:
					stream = writableStream;
					if (settings.DisableDirectStreaming)
					{
						ms = new MemoryStream();
						stream = ms;
					}
					settings.Serializer.Serialize(this.Serializable, writableStream, indent);
					break;
			}
			if (ms != null)
			{
				ms.Position = 0;
				await ms.CopyToAsync(writableStream, 8096);
			}
			if (this._tag != 0)
				this.Bytes = ms?.ToArray();
		}

		public static implicit operator PostData<T>(byte[] byteArray) => new PostData<T>(byteArray);
		public static implicit operator PostData<T>(string literalString) => new PostData<T>(literalString);
		public static implicit operator PostData<T>(List<string> listOfStrings) => new PostData<T>(listOfStrings);
		public static implicit operator PostData<T>(List<object> listOfObjects) => new PostData<T>(listOfObjects);
		public static implicit operator PostData<T>(T @object) => new PostData<T>(@object);
	}

}
