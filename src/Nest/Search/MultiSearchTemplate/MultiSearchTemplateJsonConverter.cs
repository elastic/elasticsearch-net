using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	internal class MultiSearchTemplateJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => false;
		public override bool CanWrite => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = (IMultiSearchTemplateRequest)value;
			if (request == null) return;
			var settings = serializer.GetConnectionSettings();
			var elasticsearchSerializer = settings.Serializer;
			if (elasticsearchSerializer == null) return;

			if (request.Operations == null) return;

			foreach (var operation in request.Operations.Values)
			{
				IUrlParameter indices = request.Index == null || !request.Index.Equals(operation.Index)
					? operation.Index
					: null;

				IUrlParameter types = request.Type == null || !request.Type.Equals(operation.Type)
					? operation.Type
					: null;

				var searchType = operation.RequestParameters.GetQueryStringValue<SearchType>("search_type").GetStringValue();
				if (searchType == "query_then_fetch")
					searchType = null;

				var header = new
				{
					index = indices?.GetString(settings),
					type = types?.GetString(settings),
					search_type = searchType,
					preference = operation.Preference,
					routing = operation.Routing,
					ignore_unavailable = operation.IgnoreUnavalable
				};

				var headerString = elasticsearchSerializer.SerializeToString(header, SerializationFormatting.None);
				writer.WriteRaw($"{headerString}\n");
				var bodyString = elasticsearchSerializer.SerializeToString(operation, SerializationFormatting.None);
				writer.WriteRaw($"{bodyString}\n");
			};
		}
	}
}
