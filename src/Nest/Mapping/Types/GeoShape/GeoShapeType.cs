using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IGeoShapeType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GeoShapeType : ElasticType, IGeoShapeType
	{
		public GeoShapeType() : base("geo_shape") { }
		
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

	public class GeoShapeTypeDescriptor<T>
	{
		internal GeoShapeType _Mapping = new GeoShapeType();

		public GeoShapeTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public GeoShapeTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public GeoShapeTypeDescriptor<T> Tree(GeoTree geoTree)
		{
			this._Mapping.Tree = geoTree;
			return this;
		}

		public GeoShapeTypeDescriptor<T> TreeLevels(int treeLevels)
		{
			this._Mapping.TreeLevels = treeLevels;
			return this;
		}

		public GeoShapeTypeDescriptor<T> Precision(double precision, GeoPrecisionUnit unit)
		{
			this._Mapping.Precision = new GeoPrecision(precision, unit);
			return this;
		}

		public GeoShapeTypeDescriptor<T> Orientation(GeoOrientation orientation)
		{
			this._Mapping.Orientation = orientation;
			return this;
		}

		public GeoShapeTypeDescriptor<T> DistanceErrorPercentage(double distanceErrorPercentage)
		{
			this._Mapping.DistanceErrorPercentage = distanceErrorPercentage;
			return this;
		}
	}
}