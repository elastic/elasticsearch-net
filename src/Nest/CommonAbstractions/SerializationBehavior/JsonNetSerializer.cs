using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	/// <summary> A JSON serializer that uses Json.NET for serialization </summary>
	internal class JsonNetSerializer : IElasticsearchSerializer
	{
		private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);
		private readonly JsonSerializer _defaultSerializer;
		private readonly JsonSerializer _indentedSerializer;
		//TODO this internal smells
		internal JsonSerializer Serializer => _defaultSerializer;

		protected IConnectionSettingsValues Settings { get; }

		/// <summary>
		/// Resolves JsonContracts for types
		/// </summary>
		private ElasticContractResolver ContractResolver { get; }


		/// <summary>
		/// The size of the buffer to use when writing the serialized request
		/// to the request stream
		/// </summary>
		// Performance tests as part of https://github.com/elastic/elasticsearch-net/issues/1899 indicate this
		// to be a good compromise buffer size for performance throughput and bytes allocated.
		protected virtual int BufferSize => 1024;

		public JsonNetSerializer(IConnectionSettingsValues settings) : this(settings, null) { }

		/// <summary>
		/// this constructor is only here for stateful (de)serialization
		/// </summary>
		protected internal JsonNetSerializer(IConnectionSettingsValues settings, JsonConverter statefulConverter)
		{
			this.Settings = settings;
			var piggyBackState = statefulConverter == null ? null : new JsonConverterPiggyBackState { ActualJsonConverter = statefulConverter };
			this.ContractResolver = new ElasticContractResolver(this.Settings) { PiggyBackState = piggyBackState };

			var collapsed = this.CreateSettings(SerializationFormatting.None);
			var indented = this.CreateSettings(SerializationFormatting.Indented);

			this._defaultSerializer = JsonSerializer.Create(collapsed);
			this._indentedSerializer = JsonSerializer.Create(indented);
		}

		public virtual void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serializer = formatting == SerializationFormatting.Indented
				? _indentedSerializer
				: _defaultSerializer;

			using (var writer = new StreamWriter(writableStream, ExpectedEncoding, BufferSize, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				serializer.Serialize(jsonWriter, data);
				writer.Flush();
				jsonWriter.Flush();
			}
		}

		public object Default(Type type) => type.IsValueType() ? type.CreateInstance() : null;

		public virtual T Deserialize<T>(Stream stream) => (T) this.Deserialize(typeof(T), stream);

		public virtual object Deserialize(Type type, Stream stream)
		{
			if (stream == null) return Default(type);
			//TODO why does Serialize specify leaveOpen but this does not?
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var t = this._defaultSerializer.Deserialize(jsonTextReader, type);
				return t;
			}

		}

		public virtual Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			//TODO result Json.NET 10.0.1 has async
			var result = this.Deserialize<T>(stream);
			return Task.FromResult(result);
		}

		public virtual Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			//TODO result Json.NET 10.0.1 has async
			var result = this.Deserialize(type, stream);
			return Task.FromResult(result);
		}

		private JsonSerializerSettings CreateSettings(SerializationFormatting formatting)
		{
			var settings = new JsonSerializerSettings()
			{
				Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None,
				ContractResolver = this.ContractResolver,
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore
			};

			var contract = settings.ContractResolver as ElasticContractResolver;
			if (contract == null)
				throw new Exception($"NEST needs an instance of {nameof(ElasticContractResolver)} registered on Json.NET's JsonSerializerSettings");

			return settings;
		}
	}
}
