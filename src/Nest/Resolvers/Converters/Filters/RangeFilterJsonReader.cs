using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	public class RangeFilterJsonReader : JsonConverter
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
			IRangeFilter filter = new RangeFilterDescriptor<object>();
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
						filter.CacheName = jv.Value.Value<string>();
						break;
					default:
						filter.Field = jv.Key;
						var from = jv.Value["from"];
						if (from != null)
							filter.From = from.Value<string>();
						var to = jv.Value["to"];
						if (to != null)
							filter.To = to.Value<string>();
						break;
				}
			}

			return filter;

		}
	}
	
}
