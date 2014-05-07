using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	public class GeoDistanceRangeFilterJsonReader : JsonConverter
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
			IGeoDistanceRangeFilter filter = new GeoDistanceRangeFilterDescriptor();
			foreach (var jv in j)
			{
				switch (jv.Key)
				{
					case "include_lower":
						filter.IncludeLower = jv.Value.Value<bool>();
						break;
					case "include_upper":
						filter.IncludeUpper = jv.Value.Value<bool>();
						break;
					case "from":
						if (jv.Value.Type == JTokenType.String)
							filter.From = jv.Value.Value<string>();
						else
							filter.From = jv.Value.Value<double>();
						break;
					case "to":
						filter.To = jv.Value.Value<object>();
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
						filter.CacheName = jv.Value.Value<string>();
						break;
					default:
						filter.Field = jv.Key;
						filter.Location = jv.Value.Value<string>();
						break;
				}
			}



			return filter;

		}
		
		private void ReadBox(IGeoBoundingBoxFilter filter, JsonReader reader)
		{
			reader.Read();
			if (reader.TokenType != JsonToken.StartObject)
				return;
			reader.Read();
			var firstProperty = reader.Value as string;
			if (firstProperty == "top_left")
			{
				reader.Read();
				if (reader.ValueType == typeof(string))
				{
					filter.TopLeft = reader.Value as string;
					reader.Read();
					reader.Read();
					filter.BottomRight = reader.Value as string;
				}
				else if (reader.TokenType == JsonToken.StartArray)
				{
					var values = JArray.Load(reader).Values<double>();
					filter.TopLeft = string.Join(", ", values);
					reader.Read();
					reader.Read();
					values = JArray.Load(reader).Values<double>();
					filter.BottomRight =string.Join(", ", values); 
				}
				else if (reader.TokenType == JsonToken.StartObject)
				{
					var latlon = JObject.Load(reader).ToObject<LatLon>();
					filter.TopLeft = "{0}, {1}".F(latlon.Lon, latlon.Lat); 
					reader.Read();
					reader.Read();
					latlon = JObject.Load(reader).ToObject<LatLon>();
					filter.BottomRight = "{0}, {1}".F(latlon.Lon, latlon.Lat); 
				}
			}
			//vertices
			else if (firstProperty == "top")
			{
				reader.Read();
				var top = reader.Value as double?;
				reader.Read();
				reader.Read();
				var left = reader.Value as double?;
				reader.Read();
				reader.Read();
				var bottom = reader.Value as double?;
				reader.Read();
				reader.Read();
				var right = reader.Value as double?;

				filter.TopLeft = "{0}, {1}".F(top, left);
				filter.BottomRight = "{0}, {1}".F(bottom, right);

			}
			reader.Read();
		}
	}
	
}
