using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	public class GeoShapeFilterJsonReader : JsonConverter
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

			string cacheKey = null, cacheName = null, field = null;
			bool? cache = null;
			IGeoShapeBaseFilter filter = null;
			foreach (var jv in j)
			{
				switch (jv.Key)
				{
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

						var shape = jv.Value["shape"];
						var indexedShape = jv.Value["indexed_shape"];
						if (shape != null)
						{
							var type = shape["type"];
							if (type != null)
							{
								var typeName = type.Value<string>();
								if (typeName == "circle")
								{
									IGeoShapeCircleFilter f = new GeoShapeCircleFilterDescriptor();
									f.Shape = new CircleGeoShape();
									var radius = shape["radius"];
									if (radius != null)
										f.Shape.Radius = radius.Value<string>();
									var coordinates = shape["coordinates"];
									if (coordinates != null)
										f.Shape.Coordinates = coordinates.Values<double>();
									filter = f;
									break;
								}
								else if (typeName == "envelope")
								{
									IGeoShapeEnvelopeFilter f = new GeoShapeEnvelopeFilterDescriptor();
									f.Shape = new EnvelopeGeoShape();
									var coordinates = shape["coordinates"];
									if (coordinates != null)
										f.Shape.Coordinates = coordinates.Values<double[]>();
									filter = f;
									break;
								}
								else if (typeName == "linestring")
								{
									IGeoShapeLineStringFilter f = new GeoShapeLineStringFilterDescriptor();
									f.Shape = new LineStringGeoShape();
									var coordinates = shape["coordinates"];
									if (coordinates != null)
										f.Shape.Coordinates = coordinates.Values<double[]>();
									filter = f;
									break;
								}
								else if (typeName == "multilinestring")
								{
									IGeoShapeMultiLineStringFilter f = new GeoShapeMultiLineStringFilterDescriptor();
									f.Shape = new MultiLineStringGeoShape();
									var coordinates = shape["coordinates"];
									if (coordinates != null)
										f.Shape.Coordinates = coordinates.Values<double[][]>();
									filter = f;
									break;
								}
								else if (typeName == "point")
								{
									IGeoShapePointFilter f = new GeoShapePointFilterDescriptor();
									f.Shape = new PointGeoShape();
									var coordinates = shape["coordinates"];
									if (coordinates != null)
										f.Shape.Coordinates = coordinates.Values<double>();
									filter = f;
									break;
								}
								else if (typeName == "multipoint")
								{
									IGeoShapeMultiPointFilter f = new GeoShapeMultiPointFilterDescriptor();
									f.Shape = new MultiPointGeoShape();
									var coordinates = shape["coordinates"];
									if (coordinates != null)
										f.Shape.Coordinates = coordinates.Values<double[]>();
									filter = f;
									break;
								}
								else if (typeName == "polygon")
								{
									IGeoShapePolygonFilter f = new GeoShapePolygonFilterDescriptor();
									f.Shape = new PolygonGeoShape();
									var coordinates = shape["coordinates"];
									if (coordinates != null)
										f.Shape.Coordinates = coordinates.Values<double[][]>();
									filter = f;
									break;
								}
								else if (typeName == "multipolygon")
								{
									IGeoShapeMultiPolygonFilter f = new GeoShapeMultiPolygonFilterDescriptor();
									f.Shape = new MultiPolygonGeoShape();
									var coordinates = shape["coordinates"];
									if (coordinates != null)
										f.Shape.Coordinates = coordinates.Values<double[][][]>();
									filter = f;
									break;
								}
							}
						}
						else if (indexedShape != null)
						{
							IGeoIndexedShapeFilter f = new GeoIndexedShapeFilterDescriptor();
							f.IndexedShape = new IndexedGeoShape();
							var id = indexedShape["id"];
							var index = indexedShape["index"];
							var type = indexedShape["type"];
							var shapeField = indexedShape["path"];

							if (id != null) f.IndexedShape.Id = id.Value<string>();
							if (index != null) f.IndexedShape.Index = index.Value<string>();
							if (type != null) f.IndexedShape.Type = type.Value<string>();
							if (shapeField != null) f.IndexedShape.Field = shapeField.Value<string>();
							filter = f;
							break;
						}
						break;
				}
			}
			if (filter == null) return null;
			filter.Field = field;
			filter.Cache = cache;
			filter.CacheKey = cacheKey;
			filter.FilterName = cacheName;
			return filter;

		}
	}
	
}
