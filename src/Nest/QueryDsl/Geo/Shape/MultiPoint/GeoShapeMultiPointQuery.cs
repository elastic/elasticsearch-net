using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeMultiPointQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiPointGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPointQuery : FieldNameQueryBase, IGeoShapeMultiPointQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IMultiPointGeoShape Shape { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeMultiPointQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}
	
	public class GeoShapeMultiPointQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapeMultiPointQueryDescriptor<T>, IGeoShapeMultiPointQuery, T>
		, IGeoShapeMultiPointQuery where T : class
	{
		protected override bool Conditionless => GeoShapeMultiPointQuery.IsConditionless(this);
		IMultiPointGeoShape IGeoShapeMultiPointQuery.Shape { get; set; }

		public GeoShapeMultiPointQueryDescriptor<T> Coordinates(IEnumerable<GeoCoordinate> coordinates) => Assign(a =>
		{
			a.Shape = a.Shape ?? new MultiPointGeoShape();
			a.Shape.Coordinates = coordinates;
		});
	}
}
