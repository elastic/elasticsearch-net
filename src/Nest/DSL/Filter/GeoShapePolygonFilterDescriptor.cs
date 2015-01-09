using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapePolygonFilter : IGeoShapeBaseFilter
	{
		[JsonProperty("shape")]
		IPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapePolygonFilter : PlainFilter, IGeoShapePolygonFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }
		public GeoShapeRelation? Relation { get; set; }
		public IPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapePolygonFilterDescriptor : FilterBase, IGeoShapePolygonFilter
	{
		IGeoShapePolygonFilter Self { get { return this; } }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Shape == null || !this.Self.Shape.Coordinates.HasAny();
			}
		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }
		IPolygonGeoShape IGeoShapePolygonFilter.Shape { get; set; }

		public GeoShapePolygonFilterDescriptor Coordinates(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates)
		{
			if (this.Self.Shape == null)
				this.Self.Shape = new PolygonGeoShape();
			this.Self.Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapePolygonFilterDescriptor Relation(GeoShapeRelation relation)
		{
			this.Self.Relation = relation;
			return this;
		}
	}

}
