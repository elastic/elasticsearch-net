using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class MultiPercolateJsonConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = value as IMultiPercolateRequest;
			if (request == null) return;
			if (request.Percolations == null) return;

			var settings = serializer.GetConnectionSettings();
			var elasticsearchSerializer = settings.Serializer;
			if (elasticsearchSerializer == null) return;
			foreach (var percolation in request.Percolations)
			{
				var requestParameters = percolation.GetRequestParameters() ?? new PercolateRequestParameters();
				var header = new PercolateHeader
				{
					percolate_index = requestParameters.GetQueryStringValue<string>("percolate_index"),
					percolate_type = requestParameters.GetQueryStringValue<string>("percolate_type"),
					routing = requestParameters.GetQueryStringValue<string[]>("routing"),
					preference = requestParameters.GetQueryStringValue<string>("preference"),
					percolate_routing = requestParameters.GetQueryStringValue<string>("percolate_routing"),
					percolate_preference = requestParameters.GetQueryStringValue<string>("percolate_preference"),
					version = requestParameters.GetQueryStringValue<long?>("version")
				};
				var routeValues = (percolation as IRequest)?.RouteValues;
				if (routeValues != null)
				{
					header.id = routeValues.Get<Id>("id");
					header.index = routeValues.Get<IndexName>("index");
					header.type = routeValues.Get<TypeName>("type");
					if (request.Index != null && request.Index.EqualsMarker(header.index)) header.index = null;
					if (request.Type != null && request.Type.EqualsMarker(header.type)) header.type = null;
				}
				var headerBytes = elasticsearchSerializer.SerializeToBytes(header, SerializationFormatting.None);
				writer.WriteRaw($"{{\"{percolation.MultiPercolateName}\":" + headerBytes.Utf8String() + "}\n");
				if (percolation == null)
				{
					writer.WriteRaw("{}\n");
				}
				else
				{
					var bodyBytes = elasticsearchSerializer.SerializeToBytes(percolation, SerializationFormatting.None);
					writer.WriteRaw(bodyBytes.Utf8String() + "\n");
				}
			}
		}

		private class PercolateHeader
		{
			public IndexName index { get; set; }
			public TypeName type { get; set; }
			public Id id { get; set; }
			public string percolate_index { get; set; }
			public string percolate_type { get; set; }
			public string[] routing { get; set; }
			public string preference { get; set; }
			public string percolate_routing { get; set; }
			public string percolate_preference { get; set; }
			public long? version { get; set; }
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
