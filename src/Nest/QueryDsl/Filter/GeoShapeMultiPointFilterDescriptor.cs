using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeMultiPointFilter : IGeoShapeBaseFilter
	{
		[JsonProperty("shape")]
		IMultiPointGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPointFilter : PlainFilter, IGeoShapeMultiPointFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }

		public GeoShapeRelation? Relation { get; set; }

		public IMultiPointGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPointFilterDescriptor : FilterBase, IGeoShapeMultiPointFilter
	{
		IGeoShapeMultiPointFilter Self { get { return this; } }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Shape == null || !this.Self.Shape.Coordinates.HasAny();
			}
		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }
		IMultiPointGeoShape IGeoShapeMultiPointFilter.Shape { get; set; }

		public GeoShapeMultiPointFilterDescriptor Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (this.Self.Shape == null)
				this.Self.Shape = new MultiPointGeoShape();
			this.Self.Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapeMultiPointFilterDescriptor Relation(GeoShapeRelation relation)
		{
			this.Self.Relation = relation;
			return this;
		}
	}

}
