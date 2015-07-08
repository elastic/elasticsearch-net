using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class GeoShapeMapping : IElasticType
	{
		public FieldName Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeName Type { get { return new TypeName { Name = "geo_shape" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("tree"), JsonConverter(typeof(StringEnumConverter))]
		public GeoTree? Tree { get; set; }

		[JsonProperty("precision")]
		public GeoPrecision Precision { get; set; }

		[JsonProperty("orientation")]
		public GeoOrientation? Orientation { get; set; }

		[JsonProperty("tree_levels")]
		public int? TreeLevels { get; set; }

		[JsonProperty("distance_error_pct")]
		public double? DistanceErrorPercentage { get; set; }
	}

	public class GeoShapeMappingDescriptor<T>
	{
		internal GeoShapeMapping _Mapping = new GeoShapeMapping();

		public GeoShapeMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public GeoShapeMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public GeoShapeMappingDescriptor<T> Tree(GeoTree geoTree)
		{
			this._Mapping.Tree = geoTree;
			return this;
		}

		public GeoShapeMappingDescriptor<T> TreeLevels(int treeLevels)
		{
			this._Mapping.TreeLevels = treeLevels;
			return this;
		}

		public GeoShapeMappingDescriptor<T> Precision(double precision, GeoPrecisionUnit unit)
		{
			this._Mapping.Precision = new GeoPrecision(precision, unit);
			return this;
		}

		public GeoShapeMappingDescriptor<T> Orientation(GeoOrientation orientation)
		{
			this._Mapping.Orientation = orientation;
			return this;
		}

		public GeoShapeMappingDescriptor<T> DistanceErrorPercentage(double distanceErrorPercentage)
		{
			this._Mapping.DistanceErrorPercentage = distanceErrorPercentage;
			return this;
		}
	}
}