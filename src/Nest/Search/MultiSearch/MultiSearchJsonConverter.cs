using System;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	internal class MultiSearchJsonConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = (IMultiSearchRequest)value;
			if (request == null) return;

			var settings = serializer.GetConnectionSettings();
			var elasticsearchSerializer = settings.RequestResponseSerializer;
			if (elasticsearchSerializer == null) return;

			if (request.Operations == null) return;

			foreach (var operation in request.Operations.Values)
			{
				var p = operation.RequestParameters;

				string GetString(string key)
				{
					return p.GetResolvedQueryStringValue(key, settings);
				}

				IUrlParameter indices = request.Index == null || !request.Index.Equals(operation.Index)
					? operation.Index
					: null;

				var searchType = GetString("search_type");
				if (searchType == "query_then_fetch")
					searchType = null;

				var header = new
				{
					index = indices?.GetString(settings),
					search_type = searchType,
					preference = GetString("preference"),
					routing = GetString("routing"),
					ignore_unavailable = GetString("ignore_unavailable")
				};

				var headerString = elasticsearchSerializer.SerializeToString(header, SerializationFormatting.None);
				writer.WriteRaw($"{headerString}\n");
				var bodyString = elasticsearchSerializer.SerializeToString(operation, SerializationFormatting.None);
				writer.WriteRaw($"{bodyString}\n");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}
