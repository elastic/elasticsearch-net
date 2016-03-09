using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeEnvelopeQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IEnvelopeGeoShape Shape { get; set; }
	}

	public class GeoShapeEnvelopeQuery : FieldNameQueryBase, IGeoShapeEnvelopeQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IEnvelopeGeoShape Shape { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeEnvelopeQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeEnvelopeQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapeEnvelopeQueryDescriptor<T>, IGeoShapeEnvelopeQuery, T>
		, IGeoShapeEnvelopeQuery where T : class
	{
		protected override bool Conditionless => GeoShapeEnvelopeQuery.IsConditionless(this);
		IEnvelopeGeoShape IGeoShapeEnvelopeQuery.Shape { get; set; }

		public GeoShapeEnvelopeQueryDescriptor<T> Coordinates(IEnumerable<GeoCoordinate> coordinates) => Assign(a =>
		{
			a.Shape = a.Shape ?? new EnvelopeGeoShape();
			a.Shape.Coordinates = coordinates;
		});
	}
}
