using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Serialization;
using Nest.Resolvers;
using Newtonsoft.Json;
using System.Threading;

namespace Nest
{
	public class NestSerializer : IElasticsearchSerializer
	{
		private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);

		private readonly IConnectionSettingsValues _settings;
		private readonly Dictionary<SerializationFormatting, JsonSerializer> _defaultSerializers;
		private readonly JsonSerializer _defaultSerializer;

		public NestSerializer(IConnectionSettingsValues settings) : this(settings, null) { }

		/// <summary>
		/// this constructor is only here for stateful (de)serialization 
		/// </summary>
		public NestSerializer(IConnectionSettingsValues settings, JsonConverter stateFullConverter)
		{
			this._settings = settings;

			this._defaultSerializer = JsonSerializer.Create(this.CreateSettings(SerializationFormatting.None, stateFullConverter));
			this._defaultSerializer.Formatting = Formatting.None; 
			var indentedSerializer = JsonSerializer.Create(this.CreateSettings(SerializationFormatting.Indented, stateFullConverter));
			indentedSerializer.Formatting = Formatting.Indented; 
			this._defaultSerializers = new Dictionary<SerializationFormatting, JsonSerializer>
			{
				{ SerializationFormatting.None, this._defaultSerializer },
				{ SerializationFormatting.Indented, indentedSerializer }
			};
		}

		public void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serializer = _defaultSerializers[formatting];
			using (var writer = new StreamWriter(writableStream, ExpectedEncoding, 8096, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				serializer.Serialize(jsonWriter, data);
				writer.Flush();
				jsonWriter.Flush();
			}
		}

		public virtual T Deserialize<T>(Stream stream)
		{
			if (stream == null) return default(T);
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var t = this._defaultSerializer.Deserialize(jsonTextReader, typeof(T));
				return (T)t;
			}
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			//Json.NET does not support reading a stream asynchronously :(
			var result = this.Deserialize<T>(stream);
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

			_settings.ModifyJsonSerializerSettings?.Invoke(settings);
			settings.ContractResolver = new SettingsContractResolver(settings.ContractResolver, this._settings) { PiggyBackState = piggyBackState };

			return settings;
		}
	}
}