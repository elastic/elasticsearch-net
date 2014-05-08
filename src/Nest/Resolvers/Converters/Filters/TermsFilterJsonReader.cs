using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	public class TermsFilterJsonReader : JsonConverter
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

			string cacheKey = null, cacheName = null, field = null, execution = null;
			bool? cache = null;
			ITermsBaseFilter filter = null;
			foreach (var jv in j)
			{
				switch (jv.Key)
				{
					case "execution":
						execution = jv.Value.Value<string>();
						break;
					case "_cache":
						cache = jv.Value.Value<bool>();
						break;
					case "_cache_key":
						cacheKey = jv.Value.Value<string>();
						break;
					case "_name":
						cacheName = jv.Value.Value<string>();
						break;
					default:
						field = jv.Key;
						
						if (jv.Value.Type == JTokenType.Array)
						{
							ITermsFilter f = new TermsFilter();
							f.Terms = jv.Value.Values<string>();
							filter = f;
						}
						else 
						{
							ITermsLookupFilter f = new TermsLookupFilterDescriptor();
							var id = jv.Value["id"];
							var index = jv.Value["index"];
							var type = jv.Value["type"];
							var path = jv.Value["path"];

							if (id != null) f.Id = id.Value<string>();
							if (index != null) f.Index = index.Value<string>();
							if (type != null) f.Type = type.Value<string>();
							if (path != null) f.Path = path.Value<string>();
							filter = f;
						}
						break;
				}
			}
			if (filter == null) return null;
			if (execution != null)
				filter.Execution = Enum.Parse(typeof(TermsExecution), execution) as TermsExecution?;
			filter.Field = field;
			filter.Cache = cache;
			filter.CacheKey = cacheKey;
			filter.CacheName = cacheName;
			return filter;

		}
	}
	
}
