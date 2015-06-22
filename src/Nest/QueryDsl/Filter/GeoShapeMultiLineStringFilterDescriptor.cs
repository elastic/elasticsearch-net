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
	public interface IGeoShapeMultiLineStringFilter : IGeoShapeBaseFilter
	{
		[JsonProperty("shape")]
		IMultiLineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiLineStringFilter : PlainFilter, IGeoShapeMultiLineStringFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }

		public GeoShapeRelation? Relation { get; set; }

		public IMultiLineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiLineStringFilterDescriptor : FilterBase, IGeoShapeMultiLineStringFilter
	{
		IGeoShapeMultiLineStringFilter Self { get { return this; } }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Shape == null || !this.Self.Shape.Coordinates.HasAny();
			}
		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }
		IMultiLineStringGeoShape IGeoShapeMultiLineStringFilter.Shape { get; set; }

		public GeoShapeMultiLineStringFilterDescriptor Coordinates(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates)
		{
			if (this.Self.Shape == null)
				this.Self.Shape = new MultiLineStringGeoShape();
			this.Self.Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapeMultiLineStringFilterDescriptor Relation(GeoShapeRelation relation)
		{
			this.Self.Relation = relation;
			return this;
		}
	}

}
