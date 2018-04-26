using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeLineStringQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		ILineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeLineStringQuery : GeoShapeQueryBase, IGeoShapeLineStringQuery
	{
		private ILineStringGeoShape _shape;
		protected override bool Conditionless => IsConditionless(this);

		public ILineStringGeoShape Shape
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
		internal static bool IsConditionless(IGeoShapeLineStringQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeLineStringQueryDescriptor<T>
		: GeoShapeQueryDescriptorBase<GeoShapeLineStringQueryDescriptor<T>, IGeoShapeLineStringQuery, T>
		, IGeoShapeLineStringQuery where T : class
	{
		protected override bool Conditionless => GeoShapeLineStringQuery.IsConditionless(this);
		ILineStringGeoShape IGeoShapeLineStringQuery.Shape { get; set; }

		public GeoShapeLineStringQueryDescriptor<T> Coordinates(IEnumerable<GeoCoordinate> coordinates, bool? ignoreUnmapped = null) => Assign(a =>
		{
			a.Shape = a.Shape ?? new LineStringGeoShape();
			a.Shape.Coordinates = coordinates;
			a.IgnoreUnmapped = ignoreUnmapped;
		});
	}
}
