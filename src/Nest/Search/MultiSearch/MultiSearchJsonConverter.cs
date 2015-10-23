using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Elasticsearch.Net.Serialization;
using Nest.Resolvers;
using Elasticsearch.Net;

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
			var contract = serializer.ContractResolver as SettingsContractResolver;
			var elasticsearchSerializer = contract?.ConnectionSettings.Serializer;
			if (elasticsearchSerializer == null) return;

			foreach (var operation in request.Operations.Values)
			{
				var index = (request.Index != null && !request.Index.Equals(operation.Index))
					? operation.Index
					: null;

				var type = (request.Type != null && !request.Type.Equals(operation.Type))
					? operation.Type
					: null;

				var searchType = operation.RequestParameters.GetQueryStringValue<SearchType>("search_type").GetStringValue();
				if (searchType == "query_then_fetch")
					searchType = null;

				var header = new
				{
					index = index,
					type = type,
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
