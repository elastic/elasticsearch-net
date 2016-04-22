using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapePolygonQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapePolygonQuery : FieldNameQueryBase, IGeoShapePolygonQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IPolygonGeoShape Shape { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapePolygonQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapePolygonQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapePolygonQueryDescriptor<T>, IGeoShapePolygonQuery, T>
		, IGeoShapePolygonQuery where T : class
	{
		protected override bool Conditionless => GeoShapePolygonQuery.IsConditionless(this);
		IPolygonGeoShape IGeoShapePolygonQuery.Shape { get; set; }

		public GeoShapePolygonQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) =>
			Assign(a => a.Shape = new PolygonGeoShape { Coordinates = coordinates });
	}
}
