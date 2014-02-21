using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;


namespace Nest.Resolvers.Converters
{
	public class IndexSettingsResponseConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);

		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var dict = new Dictionary<string, Dictionary<string, Dictionary<string, IndexSettings>>>();
			serializer.Populate(reader, dict);
			var response = new IndexSettingsResponse();
			if (!dict.HasAny() || !dict.First().Value.HasAny() || !dict.First().Value.First().Value.HasAny())
				return response;
			response.Nodes = dict.ToDictionary(k => k.Key, v => v.Value.First().Value.First().Value);
			return response;
		}

	}
}

