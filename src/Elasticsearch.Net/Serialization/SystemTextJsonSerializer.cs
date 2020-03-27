using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
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

		private static bool TryReturnDefault<T>(Stream stream, out T deserialize)
		{
			deserialize = default;
			return stream == null || stream == Stream.Null || (stream.CanSeek && stream.Length == 0);
		}

		private static MemoryStream ToMemoryStream(Stream stream)
		{
			if (stream is MemoryStream m) return m;
			var length = stream.CanSeek ? stream.Length : (long?)null;
			var wrapped = length.HasValue ? new MemoryStream(new byte[length.Value]) : new MemoryStream();
			//var wrapped = new MemoryStream();
			stream.CopyTo(wrapped);
			return wrapped;
		}

		private static ReadOnlySpan<byte> ToReadOnlySpan(Stream stream)
		{
			using var m = ToMemoryStream(stream);

			if (m.TryGetBuffer(out var segment))
				return segment;

			var a = m.ToArray();
			return new ReadOnlySpan<byte>(a).Slice(0, a.Length);
		}

		private JsonSerializerOptions GetFormatting(SerializationFormatting formatting) => formatting == None ? _none : _indented;

		public object Deserialize(Type type, Stream stream)
		{
			if (TryReturnDefault(stream, out object deserialize)) return deserialize;

			var buffered = ToReadOnlySpan(stream);
			return JsonSerializer.Deserialize(buffered, type, _none);
		}

		public T Deserialize<T>(Stream stream)
		{
			if (TryReturnDefault(stream, out T deserialize)) return deserialize;

			var buffered = ToReadOnlySpan(stream);
			return JsonSerializer.Deserialize<T>(buffered, _none);
		}

		public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = None)
		{
			using var writer = new Utf8JsonWriter(stream);
			if (data == null)
				JsonSerializer.Serialize(writer, null, typeof(object), GetFormatting(formatting));
			//TODO validate if we can avoid boxing by checking if data is typeof(object)
			else
				JsonSerializer.Serialize(writer, data, data.GetType(), GetFormatting(formatting));
		}

		public async Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = None,
			CancellationToken cancellationToken = default
		)
		{
			if (data == null)
				await JsonSerializer.SerializeAsync(stream, null, typeof(object), GetFormatting(formatting)).ConfigureAwait(false);
			else
				await JsonSerializer.SerializeAsync(stream, data, data.GetType(), GetFormatting(formatting), cancellationToken).ConfigureAwait(false);
		}


		//TODO ValueTask, breaking change? probably 8.0
		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default)
		{
			if (TryReturnDefault(stream, out object deserialize)) return Task.FromResult(deserialize);

			return JsonSerializer.DeserializeAsync(stream, type, _none, cancellationToken).AsTask();
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			if (TryReturnDefault(stream, out T deserialize)) return Task.FromResult(deserialize);

			return JsonSerializer.DeserializeAsync<T>(stream, _none, cancellationToken).AsTask();
		}
	}
}
