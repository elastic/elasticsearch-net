#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;

namespace Elasticsearch.Net.Utf8Json
{
    /// <summary>
    /// High-Level API of Utf8Json.
    /// </summary>
	internal static partial class JsonSerializer
    {
        private static IJsonFormatterResolver _defaultResolver;

        /// <summary>
        /// FormatterResolver that used resolver less overloads. If does not set it, used StandardResolver.Default.
        /// </summary>
        public static IJsonFormatterResolver DefaultResolver => _defaultResolver ??= StandardResolver.Default;

		/// <summary>
        /// Set default resolver of Utf8Json APIs.
        /// </summary>
        /// <param name="resolver"></param>
        public static void SetDefaultResolver(IJsonFormatterResolver resolver) => _defaultResolver = resolver;

		/// <summary>
        /// Serialize to binary with default resolver.
        /// </summary>
        public static byte[] Serialize<T>(T obj) => Serialize(obj, _defaultResolver);

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

        public static void Serialize<T>(ref JsonWriter writer, T value) => Serialize<T>(ref writer, value, _defaultResolver);

		public static void Serialize<T>(ref JsonWriter writer, T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var formatter = resolver.GetFormatterWithVerify<T>();
            formatter.Serialize(ref writer, value, resolver);
        }

        /// <summary>
        /// Serialize to stream.
        /// </summary>
        public static void Serialize<T>(Stream stream, T value) => Serialize(stream, value, _defaultResolver);

		/// <summary>
        /// Serialize to stream with specified resolver.
        /// </summary>
        public static void Serialize<T>(Stream stream, T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var buffer = SerializeUnsafe(value, resolver);
            stream.Write(buffer.Array, buffer.Offset, buffer.Count);
        }

        /// <summary>
        /// Serialize to stream(write async).
        /// </summary>
        public static Task SerializeAsync<T>(Stream stream, T value) => SerializeAsync<T>(stream, value, _defaultResolver);

		/// <summary>
        /// Serialize to stream(write async) with specified resolver.
        /// </summary>
        public static async Task SerializeAsync<T>(Stream stream, T value, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var buf = MemoryPool.Rent();
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
                MemoryPool.Return(buf);
            }
        }

        /// <summary>
        /// Serialize to binary. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
        /// </summary>
        public static ArraySegment<byte> SerializeUnsafe<T>(T obj) => SerializeUnsafe(obj, _defaultResolver);

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
        public static string ToJsonString<T>(T value) => ToJsonString(value, _defaultResolver);

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

        public static T Deserialize<T>(string json) => Deserialize<T>(json, _defaultResolver);

		public static T Deserialize<T>(string json, IJsonFormatterResolver resolver) => Deserialize<T>(StringEncoding.UTF8.GetBytes(json), resolver);

		public static T Deserialize<T>(byte[] bytes) => Deserialize<T>(bytes, _defaultResolver);

		public static T Deserialize<T>(byte[] bytes, IJsonFormatterResolver resolver) => Deserialize<T>(bytes, 0, resolver);

		public static T Deserialize<T>(byte[] bytes, int offset) => Deserialize<T>(bytes, offset, _defaultResolver);

		public static T Deserialize<T>(byte[] bytes, int offset, IJsonFormatterResolver resolver)
        {
			if (bytes == null || bytes.Length == 0)
				return default;

            if (resolver == null)
				resolver = DefaultResolver;

            var reader = new JsonReader(bytes, offset);
            var formatter = resolver.GetFormatterWithVerify<T>();
            return formatter.Deserialize(ref reader, resolver);
        }

        public static T Deserialize<T>(ref JsonReader reader) => Deserialize<T>(ref reader, _defaultResolver);

		public static T Deserialize<T>(ref JsonReader reader, IJsonFormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;

            var formatter = resolver.GetFormatterWithVerify<T>();
            return formatter.Deserialize(ref reader, resolver);
        }

        public static T Deserialize<T>(Stream stream) => Deserialize<T>(stream, _defaultResolver);

		public static T Deserialize<T>(Stream stream, IJsonFormatterResolver resolver)
        {
			if (stream == null || stream.CanSeek && stream.Length == 0)
				return default;

            if (resolver == null)
				resolver = DefaultResolver;

			if (stream is MemoryStream ms)
            {
				if (ms.TryGetBuffer(out var buf2))
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
            var buf = MemoryPool.Rent();
			var poolBuf = buf;
			try
			{
				var length = FillFromStream(stream, ref buf);

				if (length == 0)
					return default;

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
				MemoryPool.Return(poolBuf);
			}
        }

        public static Task<T> DeserializeAsync<T>(Stream stream) => DeserializeAsync<T>(stream, _defaultResolver);

		public static async Task<T> DeserializeAsync<T>(Stream stream, IJsonFormatterResolver resolver)
        {
			if (stream == null || stream.CanSeek && stream.Length == 0)
				return default;

            if (resolver == null)
				resolver = DefaultResolver;

			if (stream is MemoryStream ms)
            {
				if (ms.TryGetBuffer(out var buf2))
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

            var buffer = MemoryPool.Rent();
            var buf = buffer;
            try
            {
                var length = 0;
                int read;
                while ((read = await stream.ReadAsync(buf, length, buf.Length - length).ConfigureAwait(false)) > 0)
                {
                    length += read;
                    if (length == buf.Length)
						BinaryUtil.FastResize(ref buf, length * 2);
                }

				if (length == 0)
					return default;

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
                MemoryPool.Return(buffer);
            }
        }

		private static int FillFromStream(Stream input, ref byte[] buffer)
        {
            var length = 0;
            int read;
            while ((read = input.Read(buffer, length, buffer.Length - length)) > 0)
            {
                length += read;
                if (length == buffer.Length)
					BinaryUtil.FastResize(ref buffer, length * 2);
			}

            return length;
        }

		internal static class MemoryPool
		{
			public static byte[] Rent(int minLength = 65535) => System.Buffers.ArrayPool<byte>.Shared.Rent(minLength);

			public static void Return(byte[] bytes) => System.Buffers.ArrayPool<byte>.Shared.Return(bytes);
		}
    }
}
