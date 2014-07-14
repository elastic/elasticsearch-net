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
	public interface IGeoShapeBaseFilter : IFieldNameFilter
	{
	}

	public interface IGeoShapeFilter : IGeoShapeBaseFilter
	{
		[JsonProperty("shape")]
		GeoShape Shape { get; set; }
	}

	public class GeoShapeFilter : PlainFilter, IGeoShapeFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }

		public GeoShape Shape { get; set; }
	}

	public class GeoShapeFilterDescriptor : FilterBase, IGeoShapeFilter
	{
		IGeoShapeFilter Self { get { return this; } }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Shape == null;
			}
		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShape IGeoShapeFilter.Shape { get; set; }

		public GeoShapeFilterDescriptor Shape<TCoordinates>(IGeometryObject<TCoordinates> shape)
		{
			this.Self.Shape = shape.ToGeoShape();
			return this;
		}
	}

}
