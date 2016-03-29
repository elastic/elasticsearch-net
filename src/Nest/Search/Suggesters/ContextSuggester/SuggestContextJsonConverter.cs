using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class SuggestContextJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jo = JObject.Load(reader);
			var prop = jo.Property("type");
			if (prop == null) return null;
			switch(prop.Value.Value<string>())
			{
				case "geo":
					var g = new GeoSuggestContext();
					serializer.Populate(jo.CreateReader(), g);
					return g;

				case "category":
					var c = new CategorySuggestContext();
					serializer.Populate(jo.CreateReader(), c);
					return c;
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }
	}
}
