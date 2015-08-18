using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class IndexSettingsResponseJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			//This would have recurse to death
			//serializer.Serialize(writer, value);
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

