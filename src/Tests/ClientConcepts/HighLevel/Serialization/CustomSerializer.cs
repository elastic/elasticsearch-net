using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	public class CustomSerializer : IElasticsearchSerializer
	{
		private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);
		protected virtual int BufferSize => 1024;

		private readonly JsonSerializer _serializer;
		private readonly JsonSerializer _collapsedSerializer;

		public CustomSerializer(Func<JsonSerializerSettings> settings)
		{
			var contract = new DefaultContractResolver();
			_serializer = CreateSerializer(settings, contract, SerializationFormatting.Indented);
			_collapsedSerializer = CreateSerializer(settings, contract, SerializationFormatting.None);
		}

		private static JsonSerializer CreateSerializer(
			Func<JsonSerializerSettings> settings, IContractResolver contract, SerializationFormatting formatting)
		{
			var s = settings();
			s.Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None;
			s.ContractResolver = contract;
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
