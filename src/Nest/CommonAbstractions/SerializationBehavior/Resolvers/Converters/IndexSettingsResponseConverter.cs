using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


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
			var dict = new Dictionary<string, IndexSettings>();
			serializer.Populate(reader, dict);
			var response = new IndexSettingsResponse();
			response.Nodes = dict;
			return response;
		}

	}
}

