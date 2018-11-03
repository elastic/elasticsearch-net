using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGeoShapeGeometryCollectionQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IGeometryCollection Shape { get; set; }
	}

	public class GeoShapeGeometryCollectionQuery : GeoShapeQueryBase, IGeoShapeGeometryCollectionQuery
	{
		public IGeometryCollection Shape { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;

		internal static bool IsConditionless(IGeoShapeGeometryCollectionQuery q) => q.Field.IsConditionless() || q.Shape?.Geometries == null;
	}

	public class GeoShapeGeometryCollectionQueryDescriptor<T>
		: GeoShapeQueryDescriptorBase<GeoShapeGeometryCollectionQueryDescriptor<T>, IGeoShapeGeometryCollectionQuery, T>
			, IGeoShapeGeometryCollectionQuery where T : class
	{
		protected override bool Conditionless => GeoShapeGeometryCollectionQuery.IsConditionless(this);

		IGeometryCollection IGeoShapeGeometryCollectionQuery.Shape { get; set; }

		public GeoShapeGeometryCollectionQueryDescriptor<T> Geometries(IEnumerable<IGeoShape> geometries) => Assign(a =>
		{
			a.Shape = a.Shape ?? new GeometryCollection();
			a.Shape.Geometries = geometries;
		});

		public GeoShapeGeometryCollectionQueryDescriptor<T> Geometries(params IGeoShape[] geometries) => Assign(a =>
		{
			a.Shape = a.Shape ?? new GeometryCollection();
			a.Shape.Geometries = geometries;
		});
	}
}
