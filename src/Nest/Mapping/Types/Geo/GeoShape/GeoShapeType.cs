using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoShapeType : IElasticType
	{
		[JsonProperty("tree")]
		GeoTree? Tree { get; set; }

		[JsonProperty("precision")]
		GeoPrecision Precision { get; set; }

		[JsonProperty("orientation")]
		GeoOrientation? Orientation { get; set; }

		[JsonProperty("tree_levels")]
		int? TreeLevels { get; set; }

		[JsonProperty("distance_error_pct")]
		double? DistanceErrorPercentage { get; set; }
	}

	public class GeoShapeType : ElasticType, IGeoShapeType
	{
		public GeoShapeType() : base("geo_shape") { }
		
		internal GeoShapeType(GeoShapeAttribute attribute)
			: base("geo_shape", attribute)
		{
			Tree = attribute.Tree;
			Orientation = attribute.Orientation;
			TreeLevels = attribute.TreeLevels;
			DistanceErrorPercentage = attribute.DistanceErrorPercentage;
		}

		public GeoTree? Tree { get; set; }

		public GeoPrecision Precision { get; set; }

		public GeoOrientation? Orientation { get; set; }

		public int? TreeLevels { get; set; }

		public double? DistanceErrorPercentage { get; set; }
	}

	public class GeoShapeTypeDescriptor<T>
		: TypeDescriptorBase<GeoShapeTypeDescriptor<T>, IGeoShapeType, T>, IGeoShapeType
		where T : class
	{
		GeoTree? IGeoShapeType.Tree { get; set; }
		GeoPrecision IGeoShapeType.Precision { get; set; }
		GeoOrientation? IGeoShapeType.Orientation { get; set; }
		int? IGeoShapeType.TreeLevels { get; set; }
		double? IGeoShapeType.DistanceErrorPercentage { get; set; }

		public GeoShapeTypeDescriptor<T> Tree(GeoTree tree) => Assign(a => a.Tree = tree);

		public GeoShapeTypeDescriptor<T> TreeLevels(int treeLevels) => Assign(a => a.TreeLevels = treeLevels);

		public GeoShapeTypeDescriptor<T> Precision(double precision, GeoPrecisionUnit unit) =>
			Assign(a => a.Precision = new GeoPrecision(precision, unit));

		public GeoShapeTypeDescriptor<T> Orientation(GeoOrientation orientation) => Assign(a => a.Orientation = orientation);

		public GeoShapeTypeDescriptor<T> DistanceErrorPercentage(double distanceErrorPercentage) => 
			Assign(a => a.DistanceErrorPercentage = distanceErrorPercentage);
	}
}