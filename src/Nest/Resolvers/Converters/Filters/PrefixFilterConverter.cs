using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	public class PrefixFilterConverter : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as IPrefixFilter;
			if (f == null || (f.IsConditionless && !f.IsVerbatim)) return;
			
			var contract = serializer.ContractResolver as ElasticContractResolver;
			if (contract == null)
				return;
			
			var field = contract.Infer.PropertyPath(f.Field);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteStartObject();
			{
				WriteProperty(writer, f, field, f.Prefix);
				WriteProperty(writer, f, "_cache", f.Cache);
				WriteProperty(writer, f, "_cache_key", f.CacheKey);
				WriteProperty(writer, f, "_name", f.FilterName);
				
			}
			writer.WriteEndObject();
		}

		private static void WriteProperty(JsonWriter writer, IFilter filter, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			writer.WriteValue(value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (j == null || !j.HasValues)
				return null;

			IPrefixFilter filter = new PrefixFilter();
			foreach (var jv in j)
			{
				switch (jv.Key)
				{
					case "_cache":
						filter.Cache = jv.Value.Value<bool?>();
						break;
					case "_cacheKey":
					case "_cache_key":
						filter.CacheKey = jv.Value.Value<string>();
						break;
					case "_name":
						filter.FilterName = jv.Value.Value<string>();
						break;
					default:
						var field = jv.Key;
						filter.Field = field;
						filter.Prefix = jv.Value.Value<string>();
						break;
				}
			}
			return filter;

		}
	}
	
}
