using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeMultiLineStringQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiLineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiLineStringQuery : FieldNameQueryBase, IGeoShapeMultiLineStringQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IMultiLineStringGeoShape Shape { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeMultiLineStringQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeMultiLineStringQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapeMultiLineStringQueryDescriptor<T>, IGeoShapeMultiLineStringQuery, T>
		, IGeoShapeMultiLineStringQuery where T : class
	{
		protected override bool Conditionless => GeoShapeMultiLineStringQuery.IsConditionless(this);
		IMultiLineStringGeoShape IGeoShapeMultiLineStringQuery.Shape { get; set; }

		public GeoShapeMultiLineStringQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) => Assign(a =>
		{
			a.Shape = a.Shape ?? new MultiLineStringGeoShape();
			a.Shape.Coordinates = coordinates;
		});
	}
}
