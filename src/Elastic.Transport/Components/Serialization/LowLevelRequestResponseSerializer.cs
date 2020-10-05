using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport.Extensions;
using static Elastic.Transport.SerializationFormatting;

namespace Elastic.Transport
{
	public class LowLevelRequestResponseSerializer : ITransportSerializer
	{
		public static readonly LowLevelRequestResponseSerializer Instance = new LowLevelRequestResponseSerializer();

		private readonly Lazy<JsonSerializerOptions> _indented;
		private readonly Lazy<JsonSerializerOptions> _none;

		public IReadOnlyCollection<JsonConverter> AdditionalConverters { get; }
		private IList<JsonConverter> BakedInConverters { get; } = new List<JsonConverter>
		{
			{ new ExceptionConverter() },
			{ new ErrorCauseConverter() },
			{ new ErrorConverter() },
			{ new DynamicDictionaryConverter() }
		};

		public LowLevelRequestResponseSerializer() : this(null) { }

		public LowLevelRequestResponseSerializer(IEnumerable<JsonConverter> converters)
		{
			AdditionalConverters = converters != null
				? new ReadOnlyCollection<JsonConverter>(converters.ToList())
				: EmptyReadOnly<JsonConverter>.Collection;
			_indented = new Lazy<JsonSerializerOptions>(() => CreateSerializerOptions(Indented));
			_none = new Lazy<JsonSerializerOptions>(() => CreateSerializerOptions(None));
		}

		/// <summary>
		/// Creates <see cref="JsonSerializerOptions"/> used for serialization.
		/// Override on a derived serializer to change serialization.
		/// </summary>
		protected virtual JsonSerializerOptions CreateSerializerOptions(SerializationFormatting formatting)
		{
			var options = new JsonSerializerOptions
			{
				IgnoreNullValues = true,
				WriteIndented = formatting == Indented,
			};
			foreach (var converter in BakedInConverters)
				options.Converters.Add(converter);
			foreach (var converter in AdditionalConverters)
				options.Converters.Add(converter);

			return options;

		}

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

		private JsonSerializerOptions GetFormatting(SerializationFormatting formatting) => formatting == None ? _none.Value : _indented.Value;

		public object Deserialize(Type type, Stream stream)
		{
			if (TryReturnDefault(stream, out object deserialize)) return deserialize;

			var buffered = ToReadOnlySpan(stream);
			return JsonSerializer.Deserialize(buffered, type, _none.Value);
		}

		public T Deserialize<T>(Stream stream)
		{
			if (TryReturnDefault(stream, out T deserialize)) return deserialize;

			var buffered = ToReadOnlySpan(stream);
			return JsonSerializer.Deserialize<T>(buffered, _none.Value);
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
				await JsonSerializer.SerializeAsync(stream, null, typeof(object), GetFormatting(formatting), cancellationToken).ConfigureAwait(false);
			else
				await JsonSerializer.SerializeAsync(stream, data, data.GetType(), GetFormatting(formatting), cancellationToken).ConfigureAwait(false);
		}

		//TODO return ValueTask, breaking change? probably 8.0
		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default)
		{
			if (TryReturnDefault(stream, out object deserialize)) return Task.FromResult(deserialize);

			return JsonSerializer.DeserializeAsync(stream, type, _none.Value, cancellationToken).AsTask();
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			if (TryReturnDefault(stream, out T deserialize)) return Task.FromResult(deserialize);

			return JsonSerializer.DeserializeAsync<T>(stream, _none.Value, cancellationToken).AsTask();
		}
	}
}
