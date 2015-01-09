using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	public class RegexpFilterJsonReader : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return false; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (j == null || !j.HasValues)
				return null;
			IRegexpFilter filter = new RegexpFilterDescriptor<object>();
			foreach (var jv in j)
			{
				switch (jv.Key)
				{
					case "_cache":
						filter.Cache = jv.Value.Value<bool>();
						break;
					case "_cache_key":
						filter.CacheKey = jv.Value.Value<string>();
						break;
					case "_name":
						filter.FilterName = jv.Value.Value<string>();
						break;
					default:
						filter.Field = jv.Key;
						var value = jv.Value["value"];
						if (value != null)
							filter.Value = value.Value<string>();
						var flags = jv.Value["flags"];
						if (flags != null)
							filter.Flags = flags.Value<string>();
						break;
				}
			}

			return filter;

		}
	}
	
}
