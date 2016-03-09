using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeMultiPolygonQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPolygonQuery : FieldNameQueryBase, IGeoShapeMultiPolygonQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IMultiPolygonGeoShape Shape { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeMultiPolygonQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeMultiPolygonQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapeMultiPolygonQueryDescriptor<T>, IGeoShapeMultiPolygonQuery, T>
		, IGeoShapeMultiPolygonQuery where T : class
	{
		protected override bool Conditionless => GeoShapeMultiPolygonQuery.IsConditionless(this);
		IMultiPolygonGeoShape IGeoShapeMultiPolygonQuery.Shape { get; set; }

		public GeoShapeMultiPolygonQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> coordinates) =>
			Assign(a => a.Shape = new MultiPolygonGeoShape { Coordinates = coordinates });
	}
}
