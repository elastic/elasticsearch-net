using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Tests.ClientConcepts.HighLevel.Caching;
using Tests.ClientConcepts.HighLevel.Serialization;
using Tests.Framework.MockData;

namespace Tests.Framework.ManagedElasticsearch.SourceSerializers
{
	public class CustomProjectJsonConverter : JsonConverter
	{
		private readonly JsonSerializer _serializer;

		public CustomProjectJsonConverter()
		{
			var contract = new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()};
			var settings = new JsonSerializerSettings {ContractResolver = contract};
			settings.NullValueHandling = NullValueHandling.Ignore;
			settings.DefaultValueHandling = DefaultValueHandling.Include;
			settings.Converters = new List<JsonConverter>
			{
				new TestJoinFieldJsonConverter()
			};
			this._serializer = JsonSerializer.Create(settings);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var p = value as Project;
			var o = JObject.FromObject(p, this._serializer);
			o.Add("notWrittenByDefaultSerializer", "written");
			writer.WriteToken(o.CreateReader(), true);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.ReadFrom(reader);
			var p = o.ToObject<Project>(this._serializer);
			p.NotWrittenByDefaultSerializer = "written";
			p.NotReadByDefaultSerializer = "read";
			return p;
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(Project);
	}

	public class CustomSourceSerializer : IElasticsearchSerializer
	{
		public static CustomSourceSerializer Default { get; } = new CustomSourceSerializer(
			() => new JsonSerializerSettings(),
			new List<JsonConverter>()
			{
				new CustomProjectJsonConverter(),
				new TestJoinFieldJsonConverter()
			});

		private readonly IList<JsonConverter> _converters;
		private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);
		protected virtual int BufferSize => 1024;

		private readonly JsonSerializer _serializer;
		private readonly JsonSerializer _collapsedSerializer;

		public CustomSourceSerializer(Func<JsonSerializerSettings> settings, IList<JsonConverter> converters = null)
		{
			_converters = converters;
			var contract = new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()};
			_serializer = CreateSerializer(settings, contract, SerializationFormatting.Indented);
			_collapsedSerializer = CreateSerializer(settings, contract, SerializationFormatting.None);
		}

		private JsonSerializer CreateSerializer(
			Func<JsonSerializerSettings> settings, IContractResolver contract, SerializationFormatting formatting)
		{
			var s = settings();
			s.Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None;
			s.ContractResolver = contract;
			s.Converters = _converters;
			return JsonSerializer.Create(s);
		}

		public T Deserialize<T>(Stream stream)
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
				return _serializer.Deserialize<T>(jsonTextReader);
		}

		public object Deserialize(Type type, Stream stream)
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
				return _serializer.Deserialize(jsonTextReader, type);
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = new CancellationToken())
		{
			var o = this.Deserialize<T>(stream);
			return Task.FromResult(o);
		}

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = new CancellationToken())
		{
			var o = this.Deserialize(type, stream);
			return Task.FromResult(o);
		}

		public void Serialize(object data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			using (var writer = new StreamWriter(stream, ExpectedEncoding, BufferSize, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
				(formatting == SerializationFormatting.Indented ? _serializer : _collapsedSerializer)
					.Serialize(jsonWriter, data);
		}
	}
}
