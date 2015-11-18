using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;
using Nest.Resolvers;
using Newtonsoft.Json;
using System.Threading;

namespace Nest
{
	public class NestSerializer : IElasticsearchSerializer
	{
		private readonly IConnectionSettingsValues _settings;
		private readonly JsonSerializerSettings _serializationSettings;
		private readonly ElasticInferrer _infer;
		private readonly Encoding _encoding = new UTF8Encoding(false);
		private readonly JsonSerializer _defaultSerializer;

		public NestSerializer(IConnectionSettingsValues settings) : this(settings, null) { }
		
		/// <summary>
		/// this constructor is only here for stateful (de)serialization 
		/// </summary>
		public NestSerializer(IConnectionSettingsValues settings, JsonConverter stateFullConverter)
		{
			this._settings = settings;
			var formatting = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			this._serializationSettings = this.CreateSettings(formatting, stateFullConverter);
			this._infer = new ElasticInferrer(this._settings);
			this._defaultSerializer = JsonSerializer.Create(_serializationSettings);
		}

		public void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			using (var writer = new StreamWriter(writableStream, _encoding, 8096, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				var serializer = JsonSerializer.Create(this.CreateSettings(formatting));
				serializer.Serialize(jsonWriter, data);
				writer.Flush();
				jsonWriter.Flush();
			}
		}

		public virtual T Deserialize<T>(Stream stream)
		{
			if (stream == null) return default(T);
			var serializer = JsonSerializer.Create(this._serializationSettings);
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var t = serializer.Deserialize(jsonTextReader, typeof(T));
				return (T)t;
			}
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			//Json.NET does not support reading a stream asynchronously :(
			var result = this.Deserialize<T>(stream);
			return Task.FromResult<T>(result);
		}


		public virtual T Deserialize<T>(Stream stream, JsonConverter converter)
		{
			if (stream == null) return default(T);
			var serializer = JsonSerializer.Create(this._serializationSettings);
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var t = converter.ReadJson(jsonTextReader, typeof(T), null, serializer);
				return (T)t;
			}
		}

		public Task<T> DeserializeAsync<T>(Stream stream, JsonConverter converter, CancellationToken cancellationToken = default(CancellationToken))
		{
			//Json.NET does not support reading a stream asynchronously :(
			var result = this.Deserialize<T>(stream, converter);
			return Task.FromResult<T>(result);
		}

		internal JsonSerializerSettings CreateSettings(SerializationFormatting formatting, JsonConverter piggyBackJsonConverter = null)
		{
			var piggyBackState = new JsonConverterPiggyBackState { ActualJsonConverter = piggyBackJsonConverter };
			var settings = new JsonSerializerSettings()
			{
				Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None,
				ContractResolver = new ElasticContractResolver(this._settings),
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore
			};

			if (_settings.ModifyJsonSerializerSettings != null)
				_settings.ModifyJsonSerializerSettings(settings);

			settings.ContractResolver = new SettingsContractResolver(settings.ContractResolver, this._settings) { PiggyBackState = piggyBackState };

			return settings;
		}
	}
}