using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<GeoShapeFilterDescriptor>, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeBaseFilter : IFilterBase
	{
		PropertyPathMarker Field { get; set; }
	}

	public interface IGeoShapeFilter : IGeoShapeBaseFilter, ICustomJson
	{
		[JsonProperty("shape")]
		GeoShapeVector Shape { get; set; }
	}

	public class GeoShapeFilterDescriptor : FilterBase, IGeoShapeFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IGeoShapeFilter)this).Shape == null || !((IGeoShapeFilter)this).Shape.Coordinates.HasAny();
			}
		}

		PropertyPathMarker IGeoShapeBaseFilter.Field { get; set; }
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
		
		object ICustomJson.GetCustomJson()
		{
			var f = (IGeoShapeFilter)this;
			var shape = new { shape = f.Shape };
			return this.FieldNameAsKeyFormat(f.Field, shape);
		}
	
	}

}
