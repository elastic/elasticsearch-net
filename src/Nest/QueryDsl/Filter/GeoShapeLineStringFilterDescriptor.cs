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
	public interface IGeoShapeLineStringFilter : IGeoShapeBaseFilter
	{
		[JsonProperty("shape")]
		ILineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeLineStringFilter : PlainFilter, IGeoShapeLineStringFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }
		public GeoShapeRelation? Relation { get; set; }
		public ILineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeLineStringFilterDescriptor : FilterBase, IGeoShapeLineStringFilter
	{
		IGeoShapeLineStringFilter Self { get { return this; } }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Shape == null || !this.Self.Shape.Coordinates.HasAny();
			}
		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }
		ILineStringGeoShape IGeoShapeLineStringFilter.Shape { get; set; }

		public GeoShapeLineStringFilterDescriptor Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (this.Self.Shape == null)
				this.Self.Shape = new LineStringGeoShape();
			this.Self.Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapeLineStringFilterDescriptor Relation(GeoShapeRelation relation)
		{
			this.Self.Relation = relation;
			return this;
		}
	}

}
