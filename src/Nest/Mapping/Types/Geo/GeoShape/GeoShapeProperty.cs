using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoShapeProperty : IElasticsearchProperty
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

	public class GeoShapeProperty : ElasticsearchProperty, IGeoShapeProperty
	{
		public GeoShapeProperty() : base("geo_shape") { }
		
		internal GeoShapeProperty(GeoShapeAttribute attribute)
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

	public class GeoShapePropertyDescriptor<T>
		: PropertyDescriptorBase<GeoShapePropertyDescriptor<T>, IGeoShapeProperty, T>, IGeoShapeProperty
		where T : class
	{
		GeoTree? IGeoShapeProperty.Tree { get; set; }
		GeoPrecision IGeoShapeProperty.Precision { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }
		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }

		public GeoShapePropertyDescriptor<T> Tree(GeoTree tree) => Assign(a => a.Tree = tree);

		public GeoShapePropertyDescriptor<T> TreeLevels(int treeLevels) => Assign(a => a.TreeLevels = treeLevels);

		public GeoShapePropertyDescriptor<T> Precision(double precision, GeoPrecisionUnit unit) =>
			Assign(a => a.Precision = new GeoPrecision(precision, unit));

		public GeoShapePropertyDescriptor<T> Orientation(GeoOrientation orientation) => Assign(a => a.Orientation = orientation);

		public GeoShapePropertyDescriptor<T> DistanceErrorPercentage(double distanceErrorPercentage) => 
			Assign(a => a.DistanceErrorPercentage = distanceErrorPercentage);
	}
}