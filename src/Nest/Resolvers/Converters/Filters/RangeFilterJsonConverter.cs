using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	[Obsolete("Scheduled to be removed in 2.0, please use RangeFilterJsonConverter instead")]
	public class RangeFilterJsonReader : RangeFilterJsonConverter
	{
		
	}

	public class RangeFilterJsonConverter : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as IRangeFilter;
			if (f == null || (f.IsConditionless && !f.IsVerbatim)) return;

			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;

			var field = contract.Infer.PropertyPath(f.Field);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteStartObject();
			{
				writer.WritePropertyName(field);
				writer.WriteStartObject();
				{
					SerializeProperty(writer, serializer, "lt", f.LowerThan);
					SerializeProperty(writer, serializer, "lte", f.LowerThanOrEqualTo);
					SerializeProperty(writer, serializer, "gt", f.GreaterThan);
					SerializeProperty(writer, serializer, "gte", f.GreaterThanOrEqualTo);
					SerializeProperty(writer, serializer, "time_zone", f.TimeZone);
				}
				writer.WriteEndObject();

				if (f.Execution.HasValue)
				{
					writer.WritePropertyName("execution");
					serializer.Serialize(writer, f.Execution.Value);
				}
				WriteProperty(writer, "_cache", f.Cache);
				WriteProperty(writer, "_cache_key", f.CacheKey);
				WriteProperty(writer, "_name", f.FilterName);
			}
			writer.WriteEndObject();
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
					case "execution":
						var execution = jv.Value.Value<string>();
						if(!string.IsNullOrEmpty(execution)) filter.Execution = execution.ToEnum<RangeExecution>();
						break;
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
					
						var gte = jv.Value["gte"];
						if (gte != null)
							filter.GreaterThanOrEqualTo = ToString(gte);
						
						var gt = jv.Value["gt"];
						if (gt != null)
							filter.GreaterThanOrEqualTo = ToString(gt);

						var lte = jv.Value["lte"];
						if (lte != null)
							filter.LowerThanOrEqualTo = ToString(lte);
							
						var lt = jv.Value["lt"];
						if (lt != null)
							filter.LowerThanOrEqualTo = ToString(lt);
						
						break;
				}
			}

			return filter;

		}

		private static void SerializeProperty(JsonWriter writer, JsonSerializer serializer, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			serializer.Serialize(writer, value);
		}

		private static void WriteProperty(JsonWriter writer, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			writer.WriteValue(value);
		}

		private static string ToString(JToken token)
		{
			if (token.Type == JTokenType.Date)
				return token.Value<DateTime>().ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture);
			return token.Value<string>();
		}
	}
	
}
