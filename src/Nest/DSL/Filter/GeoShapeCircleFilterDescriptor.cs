using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeCircleFilter : IGeoShapeBaseFilter
	{
		[JsonProperty("shape")]
		ICircleGeoShape Shape { get; set; }
	}

	public class GeoShapeCircleFilter : PlainFilter, IGeoShapeCircleFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }

		public ICircleGeoShape Shape { get; set; }

		public GeoShapeRelation? Relation { get; set; }
	}

	public class GeoShapeCircleFilterDescriptor : FilterBase, IGeoShapeCircleFilter
	{
		IGeoShapeCircleFilter Self { get { return this; } }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Shape == null || !this.Self.Shape.Coordinates.HasAny();
			}
		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }
		ICircleGeoShape IGeoShapeCircleFilter.Shape { get; set; }

		public GeoShapeCircleFilterDescriptor Coordinates(IEnumerable<double> coordinates)
		{
			if (this.Self.Shape == null)
				this.Self.Shape = new CircleGeoShape();
			this.Self.Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapeCircleFilterDescriptor Radius(string radius)
		{
			if (this.Self.Shape == null)
				this.Self.Shape = new CircleGeoShape();
			this.Self.Shape.Radius = radius;
			return this;
		}

		public GeoShapeCircleFilterDescriptor Relation(GeoShapeRelation relation)
		{
			this.Self.Relation = relation;
			return this;
		}
	}

}
