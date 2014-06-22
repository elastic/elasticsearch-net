using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Extensions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers.Converters.Filters
{
	public class GeoDistanceFilterConverter : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as IGeoDistanceFilter;
			if (f == null || (f.IsConditionless && !f.IsVerbatim)) return;
			
			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;
			
			var field = contract.Infer.PropertyPath(f.Field);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteStartObject();
			{
				WriteProperty(writer, f, field, f.Location);
				WriteProperty(writer, f, "distance", f.Distance);
				SerializeProperty(writer, serializer, f, "distance_type", f.DistanceType);
				SerializeProperty(writer, serializer, f, "unit", f.Unit);
				SerializeProperty(writer, serializer, f, "optimize_bbox", f.OptimizeBoundingBox);
				
				WriteProperty(writer, f, "_cache", f.Cache);
				WriteProperty(writer, f, "_cache_key", f.CacheKey);
				WriteProperty(writer, f, "_name", f.FilterName);
			}
			writer.WriteEndObject();


		}
		
		private static void SerializeProperty(JsonWriter writer, JsonSerializer serializer , IFilter filter, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			serializer.Serialize(writer, value);
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
			IGeoDistanceFilter filter = new GeoDistanceFilterDescriptor();
			foreach (var jv in j)
			{
				switch (jv.Key)
				{
					case "distance":
						filter.Distance = jv.Value.Value<string>();
						break;
					case "distance_type":
						filter.DistanceType = Enum.Parse(typeof(GeoDistanceType), jv.Value.Value<string>()) as GeoDistanceType?;
						break;
					case "optimize_bbox":
						filter.OptimizeBoundingBox = Enum.Parse(typeof(GeoOptimizeBBox), jv.Value.Value<string>()) as GeoOptimizeBBox?;
						break;
					case "unit":
						filter.Unit = Enum.Parse(typeof(GeoUnit), jv.Value.Value<string>()) as GeoUnit?;
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
						filter.Location = jv.Value.Value<string>();
						break;
				}
			}



			return filter;

		}
		
	}
	
}
