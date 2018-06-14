using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>The built in internal serializer that the high level client NEST uses.</summary>
	internal class InternalSerializer : IElasticsearchSerializer
	{
		// Default buffer size of StreamWriter, which is private :(
		internal const int DefaultBufferSize = 1024;

		internal static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);

		private static readonly Task CompletedTask = Task.CompletedTask;

		private readonly JsonSerializer _indentedSerializer;
		internal JsonSerializer Serializer { get; }

		protected IConnectionSettingsValues Settings { get; }

		/// <summary> Resolves JsonContracts for types </summary>
		private ElasticContractResolver ContractResolver { get; }

		/// <summary>
		/// The size of the buffer to use when writing the serialized request
		/// to the request stream
		/// </summary>
		// Performance tests as part of https://github.com/elastic/elasticsearch-net/issues/1899 indicate this
		// to be a good compromise buffer size for performance throughput and bytes allocated.
		protected virtual int BufferSize => DefaultBufferSize;

		public InternalSerializer(IConnectionSettingsValues settings) : this(settings, null) { }

		/// <summary>
		/// this constructor is only here for stateful (de)serialization
		/// </summary>
		protected internal InternalSerializer(IConnectionSettingsValues settings, JsonConverter statefulConverter)
		{
			this.Settings = settings;
			var piggyBackState = statefulConverter == null ? null : new JsonConverterPiggyBackState { ActualJsonConverter = statefulConverter };
			this.ContractResolver = new ElasticContractResolver(this.Settings) { PiggyBackState = piggyBackState };

			var collapsed = this.CreateSettings(SerializationFormatting.None);
			var indented = this.CreateSettings(SerializationFormatting.Indented);

			this.Serializer = JsonSerializer.Create(collapsed);
			this._indentedSerializer = JsonSerializer.Create(indented);
		}

		public virtual void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serializer = formatting == SerializationFormatting.Indented
				? _indentedSerializer
				: Serializer;

			//this leaveOpen is most likely here because in PostData when we serialize IEnumerable<object> as multi json
			//we call this multiple times, it would be better to have a dedicated Serialize(IEnumerable<object>) on the
			//IElasticsearchSerializer interface more explicitly
			using (var writer = new StreamWriter(writableStream, ExpectedEncoding, BufferSize, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				serializer.Serialize(jsonWriter, data);
				writer.Flush();
				jsonWriter.Flush();
			}
		}

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			//This makes no sense now but we need the async method on the interface in 6.x so we can start swapping this out
			//for an implementation that does make sense without having to wait for 7.x
			this.Serialize(data, stream, formatting);
			return CompletedTask;
		}

		public T Deserialize<T>(JsonReader reader) => this.Serializer.Deserialize<T>(reader);

		public object Deserialize(JsonReader reader, Type objectType) => this.Serializer.Deserialize(reader, objectType);

		public T Deserialize<T>(Stream stream)
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return default(T);
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
				return this.Serializer.Deserialize<T>(jsonTextReader);
		}

		public object Deserialize(Type type, Stream stream)
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return type.DefaultValue();
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
				return this.Serializer.Deserialize(jsonTextReader, type);
		}

#pragma warning disable 1998 // we know this is not an async operation
		public async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
#pragma warning restore 1998
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return default(T);
            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
				return this.Serializer.Deserialize<T>(jtr);
		}

#pragma warning disable 1998 // we know this is not an async operation
		public async Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
#pragma warning restore 1998
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return type.DefaultValue();
            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
				return this.Serializer.Deserialize(jtr, type);
		}

		private JsonSerializerSettings CreateSettings(SerializationFormatting formatting)
		{
			var settings = new JsonSerializerSettings
			{
				Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None,
				ContractResolver = this.ContractResolver,
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore
			};

			if (!(settings.ContractResolver is ElasticContractResolver))
				throw new Exception($"NEST needs an instance of {nameof(ElasticContractResolver)} registered on Json.NET's JsonSerializerSettings");

			return settings;
		}
	}
}
