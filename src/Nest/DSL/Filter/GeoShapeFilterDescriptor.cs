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
		GeoShapeVector Shape { get; set; }
	}

	public class GeoShapeFilter : PlainFilter, IGeoShapeFilter
	{
		protected override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }

		public GeoShapeVector Shape { get; set; }
	}

	public class GeoShapeFilterDescriptor : FilterBase, IGeoShapeFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((IGeoShapeFilter)this).Shape == null || !((IGeoShapeFilter)this).Shape.Coordinates.HasAny();
			}
		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShapeVector IGeoShapeFilter.Shape { get; set; }

		public GeoShapeFilterDescriptor Type(string type)
		{
			if (((IGeoShapeFilter)this).Shape == null)
				((IGeoShapeFilter)this).Shape = new GeoShapeVector();
			((IGeoShapeFilter)this).Shape.Type = type;
			return this;
		}

		public GeoShapeFilterDescriptor Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (((IGeoShapeFilter)this).Shape == null)
				((IGeoShapeFilter)this).Shape = new GeoShapeVector();
			((IGeoShapeFilter)this).Shape.Coordinates = coordinates;
			return this;
		}
	
	}

}
