using System;
using System.IO;
using System.Linq;
using System.Text;
using Utf8Json.Internal;
using Utf8Json.Resolvers;

namespace Utf8Json
{
    /// <summary>
    /// High-Level API of Utf8Json.
    /// </summary>
    public static partial class JsonSerializer
    {
        static IJsonFormatterResolver defaultResolver;

        /// <summary>
        /// FormatterResolver that used resolver less overloads. If does not set it, used StandardResolver.Default.
        /// </summary>
        public static IJsonFormatterResolver DefaultResolver
        {
            get
            {
                if (defaultResolver == null)
                {
                    defaultResolver = StandardResolver.Default;
                }

                return defaultResolver;
            }
        }

        /// <summary>
        /// Is resolver decided?
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                return defaultResolver != null;
            }
        }

        /// <summary>
        /// Set default resolver of Utf8Json APIs.
        /// </summary>
        /// <param name="resolver"></param>
        public static void SetDefaultResolver(IJsonFormatterResolver resolver)
        {
            defaultResolver = resolver;
        }

        /// <summary>
        /// Serialize to binary with default resolver.
        /// </summary>
        public static byte[] Serialize<T>(T obj)
        {
            return Serialize(obj, defaultResolver);
        }

        /// <summary>
        /// Serialize to binary with specified resolver.
        /// </summary>
        public static byte[] Serialize<T>(T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;
			var buffer = MemoryPool.Rent();
			try
			{
				var writer = new JsonWriter(buffer);
				var formatter = resolver.GetFormatterWithVerify<T>();
				formatter.Serialize(ref writer, value, resolver);
				return writer.ToUtf8ByteArray();
			}
			finally
			{
				MemoryPool.Return(buffer);
			}
        }

        public static void Serialize<T>(ref JsonWriter writer, T value)
        {
            Serialize<T>(ref writer, value, defaultResolver);
        }

        public static void Serialize<T>(ref JsonWriter writer, T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var formatter = resolver.GetFormatterWithVerify<T>();
            formatter.Serialize(ref writer, value, resolver);
        }

        /// <summary>
        /// Serialize to stream.
        /// </summary>
        public static void Serialize<T>(Stream stream, T value)
        {
            Serialize(stream, value, defaultResolver);
        }

        /// <summary>
        /// Serialize to stream with specified resolver.
        /// </summary>
        public static void Serialize<T>(Stream stream, T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var buffer = SerializeUnsafe(value, resolver);
            stream.Write(buffer.Array, buffer.Offset, buffer.Count);
        }

#if NETSTANDARD

        /// <summary>
        /// Serialize to stream(write async).
        /// </summary>
        public static System.Threading.Tasks.Task SerializeAsync<T>(Stream stream, T value)
        {
            return SerializeAsync<T>(stream, value, defaultResolver);
        }

        /// <summary>
        /// Serialize to stream(write async) with specified resolver.
        /// </summary>
        public static async System.Threading.Tasks.Task SerializeAsync<T>(Stream stream, T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var buf = BufferPool.Default.Rent();
            try
            {
                var writer = new JsonWriter(buf);
                var formatter = resolver.GetFormatterWithVerify<T>();
                formatter.Serialize(ref writer, value, resolver);
                var buffer = writer.GetBuffer();
                await stream.WriteAsync(buffer.Array, buffer.Offset, buffer.Count).ConfigureAwait(false);
            }
            finally
            {
                BufferPool.Default.Return(buf);
            }
        }

#endif

        /// <summary>
        /// Serialize to binary. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
        /// </summary>
        public static ArraySegment<byte> SerializeUnsafe<T>(T obj)
        {
            return SerializeUnsafe(obj, defaultResolver);
        }

        /// <summary>
        /// Serialize to binary with specified resolver. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
        /// </summary>
        public static ArraySegment<byte> SerializeUnsafe<T>(T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

			var buffer = MemoryPool.Rent();
			try
			{
				var writer = new JsonWriter(buffer);
				var formatter = resolver.GetFormatterWithVerify<T>();
				formatter.Serialize(ref writer, value, resolver);
				var arraySegment = writer.GetBuffer();
				return new ArraySegment<byte>(BinaryUtil.ToArray(ref arraySegment));
			}
			finally
			{
				MemoryPool.Return(buffer);
			}
        }

        /// <summary>
        /// Serialize to JsonString.
        /// </summary>
        public static string ToJsonString<T>(T value)
        {
            return ToJsonString(value, defaultResolver);
        }

        /// <summary>
        /// Serialize to JsonString with specified resolver.
        /// </summary>
        public static string ToJsonString<T>(T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

			var buffer = MemoryPool.Rent();
			try
			{
				var writer = new JsonWriter(buffer);
				var formatter = resolver.GetFormatterWithVerify<T>();
				formatter.Serialize(ref writer, value, resolver);
				return writer.ToString();
			}
			finally
			{
				MemoryPool.Return(buffer);
			}
        }

        public static T Deserialize<T>(string json)
        {
            return Deserialize<T>(json, defaultResolver);
        }

        public static T Deserialize<T>(string json, IJsonFormatterResolver resolver)
        {
            return Deserialize<T>(StringEncoding.UTF8.GetBytes(json), resolver);
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            return Deserialize<T>(bytes, defaultResolver);
        }

        public static T Deserialize<T>(byte[] bytes, IJsonFormatterResolver resolver)
        {
            return Deserialize<T>(bytes, 0, resolver);
        }

        public static T Deserialize<T>(byte[] bytes, int offset)
        {
            return Deserialize<T>(bytes, offset, defaultResolver);
        }

        public static T Deserialize<T>(byte[] bytes, int offset, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var reader = new JsonReader(bytes, offset);
            var formatter = resolver.GetFormatterWithVerify<T>();
            return formatter.Deserialize(ref reader, resolver);
        }

        public static T Deserialize<T>(ref JsonReader reader)
        {
            return Deserialize<T>(ref reader, defaultResolver);
        }

        public static T Deserialize<T>(ref JsonReader reader, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var formatter = resolver.GetFormatterWithVerify<T>();
            return formatter.Deserialize(ref reader, resolver);
        }

        public static T Deserialize<T>(Stream stream)
        {
            return Deserialize<T>(stream, defaultResolver);
        }

        public static T Deserialize<T>(Stream stream, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

#if NETSTANDARD && !NET45
            var ms = stream as MemoryStream;
            if (ms != null)
            {
                ArraySegment<byte> buf2;
                if (ms.TryGetBuffer(out buf2))
                {
                    // when token is number, can not use from pool(can not find end line).
                    var token = new JsonReader(buf2.Array, buf2.Offset).GetCurrentJsonToken();
                    if (token == JsonToken.Number)
                    {
                        var buf3 = new byte[buf2.Count];
                        Buffer.BlockCopy(buf2.Array, buf2.Offset, buf3, 0, buf3.Length);
                        return Deserialize<T>(buf3, 0, resolver);
                    }

                    return Deserialize<T>(buf2.Array, buf2.Offset, resolver);
                }
            }
#endif
            {
				var buf = MemoryPool.Rent();
				var poolBuf = buf;
				try
				{
					var len = FillFromStream(stream, ref buf);

					// when token is number, can not use from pool(can not find end line).
					var token = new JsonReader(buf).GetCurrentJsonToken();
					if (token == JsonToken.Number)
					{
						buf = BinaryUtil.FastCloneWithResize(buf, len);
					}

					return Deserialize<T>(buf, resolver);
				}
				finally
				{
					MemoryPool.Return(poolBuf);
				}
            }
        }

#if NETSTANDARD

        public static System.Threading.Tasks.Task<T> DeserializeAsync<T>(Stream stream)
        {
            return DeserializeAsync<T>(stream, defaultResolver);
        }

        public static async System.Threading.Tasks.Task<T> DeserializeAsync<T>(Stream stream, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var buffer = BufferPool.Default.Rent();
            var buf = buffer;
            try
            {
                int length = 0;
                int read;
                while ((read = await stream.ReadAsync(buf, length, buf.Length - length).ConfigureAwait(false)) > 0)
                {
                    length += read;
                    if (length == buf.Length)
                    {
                        BinaryUtil.FastResize(ref buf, length * 2);
                    }
                }

                // when token is number, can not use from pool(can not find end line).
                var token = new JsonReader(buf).GetCurrentJsonToken();
                if (token == JsonToken.Number)
                {
                    buf = BinaryUtil.FastCloneWithResize(buf, length);
                }

                return Deserialize<T>(buf, resolver);
            }
            finally
            {
                BufferPool.Default.Return(buffer);
            }
        }

#endif

        public static string PrettyPrint(byte[] json)
        {
            return PrettyPrint(json, 0);
        }

        public static string PrettyPrint(byte[] json, int offset)
        {
            var reader = new JsonReader(json, offset);
			var buffer = MemoryPool.Rent();
			try
			{
				var writer = new JsonWriter(buffer);
				WritePrittyPrint(ref reader, ref writer, 0);
				return writer.ToString();
			}
			finally
			{
				MemoryPool.Return(buffer);
			}
        }

        public static string PrettyPrint(string json)
        {
            var reader = new JsonReader(Encoding.UTF8.GetBytes(json));
			var buffer = MemoryPool.Rent();

			try
			{
				var writer = new JsonWriter(buffer);
				WritePrittyPrint(ref reader, ref writer, 0);
				return writer.ToString();
			}
			finally
			{
				MemoryPool.Return(buffer);
			}
        }

        public static byte[] PrettyPrintByteArray(byte[] json)
        {
            return PrettyPrintByteArray(json, 0);
        }

        public static byte[] PrettyPrintByteArray(byte[] json, int offset)
        {
            var reader = new JsonReader(json, offset);
			var buffer = MemoryPool.Rent();

			try
			{
				var writer = new JsonWriter(buffer);
				WritePrittyPrint(ref reader, ref writer, 0);
				return writer.ToUtf8ByteArray();
			}
			finally
			{
				MemoryPool.Return(buffer);
			}
        }

        public static byte[] PrettyPrintByteArray(string json)
        {
            var reader = new JsonReader(Encoding.UTF8.GetBytes(json));
			var buffer = MemoryPool.Rent();
			try
			{
				var writer = new JsonWriter(buffer);
				WritePrittyPrint(ref reader, ref writer, 0);
				return writer.ToUtf8ByteArray();
			}
			finally
			{
				MemoryPool.Return(buffer);
			}
        }

        static readonly byte[][] indent = Enumerable.Range(0, 100).Select(x => Encoding.UTF8.GetBytes(new string(' ', x * 2))).ToArray();
        static readonly byte[] newLine = Encoding.UTF8.GetBytes(Environment.NewLine);

        static void WritePrittyPrint(ref JsonReader reader, ref JsonWriter writer, int depth)
        {
            var token = reader.GetCurrentJsonToken();
            switch (token)
            {
                case JsonToken.BeginObject:
                    {
                        writer.WriteBeginObject();
                        writer.WriteRaw(newLine);
                        var c = 0;
                        while (reader.ReadIsInObject(ref c))
                        {
                            if (c != 1)
                            {
                                writer.WriteRaw((byte)',');
                                writer.WriteRaw(newLine);
                            }
                            writer.WriteRaw(indent[depth + 1]);
                            writer.WritePropertyName(reader.ReadPropertyName());
                            writer.WriteRaw((byte)' ');
                            WritePrittyPrint(ref reader, ref writer, depth + 1);
                        }
                        writer.WriteRaw(newLine);
                        writer.WriteRaw(indent[depth]);
                        writer.WriteEndObject();
                    }
                    break;
                case JsonToken.BeginArray:
                    {
                        writer.WriteBeginArray();
                        writer.WriteRaw(newLine);
                        var c = 0;
                        while (reader.ReadIsInArray(ref c))
                        {
                            if (c != 1)
                            {
                                writer.WriteRaw((byte)',');
                                writer.WriteRaw(newLine);
                            }
                            writer.WriteRaw(indent[depth + 1]);
                            WritePrittyPrint(ref reader, ref writer, depth + 1);
                        }
                        writer.WriteRaw(newLine);
                        writer.WriteRaw(indent[depth]);
                        writer.WriteEndArray();
                    }
                    break;
                case JsonToken.Number:
                    {
                        var v = reader.ReadDouble();
                        writer.WriteDouble(v);
                    }
                    break;
                case JsonToken.String:
                    {
                        var v = reader.ReadString();
                        writer.WriteString(v);
                    }
                    break;
                case JsonToken.True:
                case JsonToken.False:
                    {
                        var v = reader.ReadBoolean();
                        writer.WriteBoolean(v);
                    }
                    break;
                case JsonToken.Null:
                    {
                        reader.ReadIsNull();
                        writer.WriteNull();
                    }
                    break;
                default:
                    break;
            }
        }

        static int FillFromStream(Stream input, ref byte[] buffer)
        {
            int length = 0;
            int read;
            while ((read = input.Read(buffer, length, buffer.Length - length)) > 0)
            {
                length += read;
                if (length == buffer.Length)
                {
                    BinaryUtil.FastResize(ref buffer, length * 2);
                }
            }

            return length;
        }

		internal static class MemoryPool
		{
			public static byte[] Rent(int minLength = 65535)
			{
				return System.Buffers.ArrayPool<byte>.Shared.Rent(minLength);
			}

			public static void Return(byte[] bytes)
			{
				System.Buffers.ArrayPool<byte>.Shared.Return(bytes);
			}
		}
    }
}
