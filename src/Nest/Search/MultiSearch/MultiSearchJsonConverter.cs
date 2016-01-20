using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class MultiSearchJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => false;
		public override bool CanWrite => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = value as IMultiSearchRequest;
			if (request == null) return;
			var settings = serializer.GetConnectionSettings();
			var elasticsearchSerializer = settings.Serializer;
			if (elasticsearchSerializer == null) return;

			if (request.Operations == null) return;

			foreach (var operation in request.Operations.Values)
			{
				var indices = request.Index == null || !request.Index.Equals(operation.Index)
					? operation.Index
					: null;

				var types = request.Type == null || !request.Type.Equals(operation.Type)
					? operation.Type
					: null;

				var searchType = operation.RequestParameters.GetQueryStringValue<SearchType>("search_type").GetStringValue();
				if (searchType == "query_then_fetch")
					searchType = null;

				var header = new
				{
					index = (indices as IUrlParameter)?.GetString(settings),
					type = (types as IUrlParameter)?.GetString(settings),
					search_type = searchType,
					preference = operation.Preference,
					routing = operation.Routing,
					ignore_unavailable = operation.IgnoreUnavalable
				};

				var headerBytes = elasticsearchSerializer.SerializeToBytes(header, SerializationFormatting.None);
				writer.WriteRaw(headerBytes.Utf8String() + "\n");
				var bodyBytes = elasticsearchSerializer.SerializeToBytes(operation, SerializationFormatting.None);
				writer.WriteRaw(bodyBytes.Utf8String() + "\n");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
