using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class GeoShapeQueryJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public virtual T GetCoordinates<T>(JToken shape)
		{
			var coordinates = shape["coordinates"];
			if (coordinates != null)
				return coordinates.ToObject<T>();
			return default(T);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (j == null || !j.HasValues)
				return null;

			IGeoShapeQuery query = null;

			var firstProp = j.Properties().FirstOrDefault();
			if (firstProp == null) return null;

			var field = firstProp.Name;
			var jo = firstProp.Value.Value<JObject>();
			if (jo == null) return null;

			JToken shape;
			jo.TryGetValue("shape", out shape);

			if (shape != null)
			{
				var type = shape["type"];
				if (type != null)
				{
					var typeName = type.Value<string>();
					if (typeName == "circle")
					{
						IGeoShapeCircleQuery q = new GeoShapeCircleQueryDescriptor<object>();
						q.Shape = new CircleGeoShape();
						q.Shape.Coordinates = GetCoordinates<IEnumerable<double>>(shape);
						var radius = shape["radius"];
						if (radius != null)
							q.Shape.Radius = radius.Value<string>();
						query = q;
					}
					else if (typeName == "envelope")
					{
						IGeoShapeEnvelopeQuery q = new GeoShapeEnvelopeQueryDescriptor<object>();
						q.Shape = new EnvelopeGeoShape();
						q.Shape.Coordinates = GetCoordinates<IEnumerable<IEnumerable<double>>>(shape);
						query = q;
					}
					else if (typeName == "linestring")
					{
						IGeoShapeLineStringQuery q = new GeoShapeLineStringQueryDescriptor<object>();
						q.Shape = new LineStringGeoShape();
						q.Shape.Coordinates = GetCoordinates<IEnumerable<IEnumerable<double>>>(shape);
						query = q;
					}
					else if (typeName == "multilinestring")
					{
						IGeoShapeMultiLineStringQuery q = new GeoShapeMultiLineStringQueryDescriptor<object>();
						q.Shape = new MultiLineStringGeoShape();
						q.Shape.Coordinates = GetCoordinates<IEnumerable<IEnumerable<IEnumerable<double>>>>(shape);
						query = q;
					}
					else if (typeName == "point")
					{
						IGeoShapePointQuery q = new GeoShapePointQueryDescriptor<object>();
						q.Shape = new PointGeoShape();
						q.Shape.Coordinates = GetCoordinates<IEnumerable<double>>(shape);
						query = q;
					}
					else if (typeName == "multipoint")
					{
						IGeoShapeMultiPointQuery q = new GeoShapeMultiPointQueryDescriptor<object>();
						q.Shape = new MultiPointGeoShape();
						q.Shape.Coordinates = GetCoordinates<IEnumerable<IEnumerable<double>>>(shape);
						query = q;
					}
					else if (typeName == "polygon")
					{
						IGeoShapePolygonQuery q = new GeoShapePolygonQueryDescriptor<object>();
						q.Shape = new PolygonGeoShape();
						q.Shape.Coordinates = GetCoordinates<IEnumerable<IEnumerable<IEnumerable<double>>>>(shape);
						query = q;
					}
					else if (typeName == "multipolygon")
					{
						IGeoShapeMultiPolygonQuery q = new GeoShapeMultiPolygonQueryDescriptor<object>();
						q.Shape = new MultiPolygonGeoShape();
						q.Shape.Coordinates = GetCoordinates<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>(shape);
						query = q;
					}
				}
			}
			if (query == null) return null;
			query.Field = field;
			return query;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
