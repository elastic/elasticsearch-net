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
		byte[] ToByteArray(IConnectionConfigurationValues settings);
	}
	public interface IPostData<out T> : IPostData { }

	public class PostData<T> : IPostData<T>
	{
		protected readonly byte[] ByteArray;
		protected readonly string LiteralString;
		protected readonly IEnumerable<string> EnumurabeOfStrings;
		protected readonly IEnumerable<T> EnumerableOfObject;
		protected readonly T Serializable;

		readonly int _tag;

		public PostData(byte[] item) { ByteArray = item; _tag = 0; }
		public PostData(string item) { LiteralString = item; _tag = 1; }
		public PostData(IEnumerable<string> item) { EnumurabeOfStrings = item; _tag = 2; }
		public PostData(IEnumerable<T> item) { EnumerableOfObject = item; _tag = 3; }
		public PostData(T item) { Serializable = item; _tag = 4; }
		
		//TODO memoize from write
		public byte[] ToByteArray(IConnectionConfigurationValues settings)
		{
			var indent = settings.UsesPrettyRequests ? SerializationFormatting.Indented : SerializationFormatting.None;
			switch (_tag)
			{
				case 0: return ByteArray;
				case 1: return LiteralString?.Utf8Bytes();
				case 2: return EnumurabeOfStrings?.HasAny() ?? false ? (string.Join("\n", EnumurabeOfStrings) + "\n").Utf8Bytes() : null;
				case 3: return EnumerableOfObject?.HasAny() ?? false
						? (string.Join("\n", EnumerableOfObject.Select(soo => settings.Serializer.SerializeToBytes(soo, SerializationFormatting.None).Utf8String())) + "\n").Utf8Bytes()
						: null;
				case 4: return settings.Serializer.SerializeToBytes(Serializable, indent);
			}
			return null;
		}
		//TODO listen to keeprawresponse and optionally do NOT write directly to stream
		//TODO also rename keeprawresponse to DoNotDirectlyReadAndWriteToStream or something terser
		public void Write(Stream writableStream, IConnectionConfigurationValues settings)
		{
			var indent = settings.UsesPrettyRequests ? SerializationFormatting.Indented : SerializationFormatting.None;
			MemoryStream ms = null;
			switch (_tag)
			{
				case 0:
					ms = new MemoryStream(ByteArray);
					break;
				case 1:
					ms = new MemoryStream(LiteralString?.Utf8Bytes());
					break;
				case 2:
					ms = EnumurabeOfStrings?.HasAny() ?? false ? new MemoryStream((string.Join("\n", EnumurabeOfStrings) + "\n").Utf8Bytes()) : null;
					break;
				case 3:
					if (EnumerableOfObject?.HasAny() ?? false)
						return;

					foreach (var o in EnumerableOfObject)
						settings.Serializer.Serialize(o, writableStream, indent);
					return;
				case 4: 
					settings.Serializer.Serialize(this.Serializable, writableStream, indent);
					return;
			}
			if (ms != null)
				ms.CopyTo(writableStream, 8096);
		}

		public async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			var indent = settings.UsesPrettyRequests ? SerializationFormatting.Indented : SerializationFormatting.None;
			MemoryStream ms = null;
			switch (_tag)
			{
				case 0:
					ms = new MemoryStream(ByteArray);
					break;
				case 1:
					ms = new MemoryStream(LiteralString?.Utf8Bytes());
					break;
				case 2:
					ms = EnumurabeOfStrings?.HasAny() ?? false ? new MemoryStream((string.Join("\n", EnumurabeOfStrings) + "\n").Utf8Bytes()) : null;
					break;
				case 3:
					if (EnumerableOfObject?.HasAny() ?? false)
						return;

					foreach (var o in EnumerableOfObject)
						settings.Serializer.Serialize(o, writableStream, indent);
					return;
				case 4: 
					settings.Serializer.Serialize(this.Serializable, writableStream, indent);
					return;
			}
			if (ms != null)
				await ms.CopyToAsync(writableStream, 8096, cancellationToken);
		}


		public static implicit operator PostData<T>(byte[] byteArray) => new PostData<T>(byteArray);
		public static implicit operator PostData<T>(string literalString) => new PostData<T>(literalString);
		public static implicit operator PostData<T>(List<string> listOfStrings) => new PostData<T>(listOfStrings);
		public static implicit operator PostData<T>(List<T> listOfObjects) => new PostData<T>(listOfObjects);
		public static implicit operator PostData<T>(T @object) => new PostData<T>(@object);
	}

}
