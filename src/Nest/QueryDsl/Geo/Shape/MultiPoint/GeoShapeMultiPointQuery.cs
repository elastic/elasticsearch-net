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

	public class GeoShapeMultiPointQuery : GeoShapeQueryBase, IGeoShapeMultiPointQuery
	{
		private IMultiPointGeoShape _shape;
		protected override bool Conditionless => IsConditionless(this);

		public IMultiPointGeoShape Shape
		{
			get => _shape;
			set
			{
#pragma warning disable 618
				if (value?.IgnoreUnmapped != null)
				{
					IgnoreUnmapped = value.IgnoreUnmapped;
					value.IgnoreUnmapped = null;
				}
#pragma warning restore 618
				_shape = value;
			}
		}

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeMultiPointQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeMultiPointQueryDescriptor<T>
		: GeoShapeQueryDescriptorBase<GeoShapeMultiPointQueryDescriptor<T>, IGeoShapeMultiPointQuery, T>
		, IGeoShapeMultiPointQuery where T : class
	{
		protected override bool Conditionless => GeoShapeMultiPointQuery.IsConditionless(this);
		IMultiPointGeoShape IGeoShapeMultiPointQuery.Shape { get; set; }

		public GeoShapeMultiPointQueryDescriptor<T> Coordinates(IEnumerable<GeoCoordinate> coordinates, bool? ignoreUnmapped = null) => Assign(a =>
		{
			a.Shape = a.Shape ?? new MultiPointGeoShape();
			a.Shape.Coordinates = coordinates;
			a.IgnoreUnmapped = ignoreUnmapped;
		});
	}
}
