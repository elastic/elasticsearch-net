using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	public class GeoPolygonFilterJsonReader : JsonConverter
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
			IGeoPolygonFilter filter = new GeoPolygonFilterDescriptor();
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
						var points = jv.Value["points"];
						if (points == null)
						{
							filter.Points = Enumerable.Empty<string>();
							break;
						}
						
						var stringValues = points.Values<string>();
						//var doublePairArray = points.Values<double[]>()
						//	.Where(dd=>dd.Count() == 2)
						//	.Select(dd=>"{0}, {1}".F(dd[0], dd[1]));
						//var latLongs = points.Values<LatLon>().Select(ll=>"{0}, {1}".F(ll.Lon, ll.Lat));

						filter.Points = stringValues;
						break;
				}
			}

			return filter;

		}
	}
	
}
