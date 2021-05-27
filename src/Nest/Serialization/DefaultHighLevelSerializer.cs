using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	public class ProxyRequestConverterFactory : JsonConverterFactory
	{
		private readonly IConnectionSettingsValues _settings;

		public ProxyRequestConverterFactory(IConnectionSettingsValues settings) => _settings = settings;

		public override bool CanConvert(Type typeToConvert) => typeToConvert.IsGenericType
															   && typeToConvert.GetInterfaces().Any(x => x.UnderlyingSystemType == typeof(IProxyRequest)); // Check proxy request

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var elementType = typeToConvert.GetGenericArguments()[0];

			var att = typeToConvert.GetCustomAttribute<ConvertAsAttribute>();

			var converter = (JsonConverter)Activator.CreateInstance(
				typeof(ProxyRequestConverter<>).MakeGenericType(att?.ConvertType.MakeGenericType(elementType) ?? elementType),
				BindingFlags.Instance | BindingFlags.Public,
				args: new object[] { _settings },
				binder: null,
				culture: null)!;

			return converter;
		}
	}

	[AttributeUsage(AttributeTargets.Interface)]
	internal class ConvertAsAttribute : Attribute
	{
		public ConvertAsAttribute(Type convertType) => ConvertType = convertType;

		public Type ConvertType { get; }
	}

	public class ProxyRequestConverter<TRequest> : JsonConverter<TRequest>
	{
		private readonly IConnectionSettingsValues _settings;

		public ProxyRequestConverter(IConnectionSettingsValues settings) => _settings = settings;

		public override TRequest? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

		public override void Write(Utf8JsonWriter writer, TRequest value, JsonSerializerOptions options)
		{
			if (value is IProxyRequest proxyRequest) proxyRequest.WriteJson(writer, _settings.SourceSerializer);
		}
	}
	
	public class UnionConverter<TConcrete> : JsonConverter<TConcrete> where TConcrete : class
	{
		public override TConcrete Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var token = reader.TokenType;

			switch (token)
			{
				case JsonTokenType.String:
					{
						var value = reader.GetString();
						var result = (TConcrete)Activator.CreateInstance(typeof(TConcrete), value);
						return result;
					}
				case JsonTokenType.Number:
					{
						var value = reader.GetInt32();
						var result = (TConcrete)Activator.CreateInstance(typeof(TConcrete), value);
						return result;
					}
			}

			throw new SerializationException();
		}

		public override void Write(Utf8JsonWriter writer, TConcrete value, JsonSerializerOptions options) =>
			throw new NotImplementedException();
	}

	public class PercentageConverter : JsonConverter<Percentage>
	{
		public override Percentage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var token = reader.TokenType;

			switch (token)
			{
				case JsonTokenType.String:
					{
						var value = reader.GetString();
						var result = (Percentage)Activator.CreateInstance(typeof(Percentage), value);
						return result;
					}
				case JsonTokenType.Number:
					{
						var value = reader.GetSingle();
						var result = (Percentage)Activator.CreateInstance(typeof(Percentage), value);
						return result;
					}
			}

			throw new SerializationException();
		}

		public override void Write(Utf8JsonWriter writer, Percentage value, JsonSerializerOptions options) =>
			throw new NotImplementedException();
	}

	/// <summary>The built in internal serializer that the high level client NEST uses.</summary>
	internal class DefaultHighLevelSerializer : ITransportSerializer
	{
		// ctor added so we can pass down settings. TODO: review this design, perhaps have a method AddConverter which can be called instead?
		public DefaultHighLevelSerializer(IConnectionSettingsValues settings) =>
			Options = new JsonSerializerOptions
			{
				IgnoreNullValues = true,
				Converters = {new JsonStringEnumConverter(), new ProxyRequestConverterFactory(settings)}
			};

		private JsonSerializerOptions Options { get; }

		private static readonly UTF8Encoding Encoding = new(false);

		// TODO - This is not ideal as we allocate a large string - No stream based sync overload - We should use a pooled byte array in the future
		public T Deserialize<T>(Stream stream)
		{
			if (stream.Length == 0)
				return default;
			using var reader = new StreamReader(stream);
			return JsonSerializer.Deserialize<T>(reader.ReadToEnd(), Options);
		}

		public object Deserialize(Type type, Stream stream) =>
			throw new NotImplementedException();

		// TODO - Return ValueTask?
		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			long length = 0;

			try
			{
				length = stream.Length;
			}
			catch (NotSupportedException)
			{
				// ignored
			}

			return JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken).AsTask();

			//return length > 0
			//	? JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken).AsTask()
			//	: Task.FromResult(default(T));
		}

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync(stream, type, Options, cancellationToken).AsTask();

		// TODO - This is not ideal as we allocate a large string - No stream based sync overload
		public virtual void Serialize<T>(T data, Stream writableStream,
			SerializationFormatting formatting = SerializationFormatting.None)
		{
			var json = JsonSerializer.Serialize(data, Options);
			using var writer = new StreamWriter(writableStream, Encoding, 4096, true);
			writer.Write(json);
		}

		public Task SerializeAsync<T>(T data, Stream stream,
			SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		) => JsonSerializer.SerializeAsync(stream, data, Options, cancellationToken);
	}

	//public class SystemTextJsonSerializer : IElasticsearchSerializer
	//{
	//	public static readonly SystemTextJsonSerializer Instance = new SystemTextJsonSerializer();

	//	private readonly Lazy<JsonSerializerOptions> _indented;
	//	private readonly Lazy<JsonSerializerOptions> _none;

	//	public IReadOnlyCollection<JsonConverter> AdditionalConverters { get; }
	//	private IList<JsonConverter> BakedInConverters { get; } = new List<JsonConverter>
	//	{
	//		{ new ExceptionConverter() },
	//		{ new DynamicDictionaryConverter() }
	//	};

	//	public SystemTextJsonSerializer() : this(null) { }

	//	public SystemTextJsonSerializer(IEnumerable<JsonConverter> converters)
	//	{
	//		AdditionalConverters = converters != null
	//			? new ReadOnlyCollection<JsonConverter>(converters.ToList())
	//			: EmptyReadOnly<JsonConverter>.Collection;
	//		_indented = new Lazy<JsonSerializerOptions>(() => CreateSerializerOptions(Indented));
	//		_none = new Lazy<JsonSerializerOptions>(() => CreateSerializerOptions(None));
	//	}

	//	/// <summary>
	//	/// Creates <see cref="JsonSerializerOptions"/> used for serialization.
	//	/// Override on a derived serializer to change serialization.
	//	/// </summary>
	//	protected virtual JsonSerializerOptions CreateSerializerOptions(SerializationFormatting formatting)
	//	{
	//		var options = new JsonSerializerOptions
	//		{
	//			IgnoreNullValues = true,
	//			WriteIndented = formatting == Indented,
	//		};
	//		foreach (var converter in BakedInConverters)
	//			options.Converters.Add(converter);
	//		foreach (var converter in AdditionalConverters)
	//			options.Converters.Add(converter);

	//		return options;

	//	}

	//	private static bool TryReturnDefault<T>(Stream stream, out T deserialize)
	//	{
	//		deserialize = default;
	//		return stream == null || stream == Stream.Null || (stream.CanSeek && stream.Length == 0);
	//	}

	//	private static MemoryStream ToMemoryStream(Stream stream)
	//	{
	//		if (stream is MemoryStream m)
	//			return m;
	//		var length = stream.CanSeek ? stream.Length : (long?)null;
	//		var wrapped = length.HasValue ? new MemoryStream(new byte[length.Value]) : new MemoryStream();
	//		stream.CopyTo(wrapped);
	//		return wrapped;
	//	}

	//	private static ReadOnlySpan<byte> ToReadOnlySpan(Stream stream)
	//	{
	//		using var m = ToMemoryStream(stream);

	//		if (m.TryGetBuffer(out var segment))
	//			return segment;

	//		var a = m.ToArray();
	//		return new ReadOnlySpan<byte>(a).Slice(0, a.Length);
	//	}

	//	private JsonSerializerOptions GetFormatting(SerializationFormatting formatting) => formatting == None ? _none.Value : _indented.Value;

	//	public object Deserialize(Type type, Stream stream)
	//	{
	//		if (TryReturnDefault(stream, out object deserialize))
	//			return deserialize;

	//		var buffered = ToReadOnlySpan(stream);
	//		return JsonSerializer.Deserialize(buffered, type, _none.Value);
	//	}

	//	public T Deserialize<T>(Stream stream)
	//	{
	//		if (TryReturnDefault(stream, out T deserialize))
	//			return deserialize;

	//		var buffered = ToReadOnlySpan(stream);
	//		return JsonSerializer.Deserialize<T>(buffered, _none.Value);
	//	}

	//	public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = None)
	//	{
	//		using var writer = new Utf8JsonWriter(stream);
	//		if (data == null)
	//			JsonSerializer.Serialize(writer, null, typeof(object), GetFormatting(formatting));
	//		//TODO validate if we can avoid boxing by checking if data is typeof(object)
	//		else
	//			JsonSerializer.Serialize(writer, data, data.GetType(), GetFormatting(formatting));
	//	}

	//	public async Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = None,
	//		CancellationToken cancellationToken = default
	//	)
	//	{
	//		if (data == null)
	//			await JsonSerializer.SerializeAsync(stream, null, typeof(object), GetFormatting(formatting), cancellationToken).ConfigureAwait(false);
	//		else
	//			await JsonSerializer.SerializeAsync(stream, data, data.GetType(), GetFormatting(formatting), cancellationToken).ConfigureAwait(false);
	//	}

	//	//TODO return ValueTask, breaking change? probably 8.0
	//	public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default)
	//	{
	//		if (TryReturnDefault(stream, out object deserialize))
	//			return Task.FromResult(deserialize);

	//		return JsonSerializer.DeserializeAsync(stream, type, _none.Value, cancellationToken).AsTask();
	//	}

	//	public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
	//	{
	//		if (TryReturnDefault(stream, out T deserialize))
	//			return Task.FromResult(deserialize);

	//		return JsonSerializer.DeserializeAsync<T>(stream, _none.Value, cancellationToken).AsTask();
	//	}
	//}
}
