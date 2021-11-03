using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class CustomJsonWriterConverter<TDocument> : JsonConverter<TDocument>
	{
		private readonly IElasticsearchClientSettings _settings;

		public CustomJsonWriterConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override TDocument? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			throw new NotImplementedException();

		public override void Write(Utf8JsonWriter writer, TDocument value, JsonSerializerOptions options)
		{
			if (value is ICustomJsonWriter proxyRequest) proxyRequest.WriteJson(writer, _settings.SourceSerializer);
		}
	}
}
