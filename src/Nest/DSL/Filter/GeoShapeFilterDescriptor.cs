using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGeoShapeFilter : IFilterBase
	{
		[JsonProperty("shape")]
		GeoShapeVector _Shape { get; set; }
	}

	public class GeoShapeFilterDescriptor : FilterBase, IGeoShapeFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IGeoShapeFilter)this)._Shape == null || !((IGeoShapeFilter)this)._Shape.Coordinates.HasAny();
			}

		}

		GeoShapeVector IGeoShapeFilter._Shape { get; set; }


		public GeoShapeFilterDescriptor Type(string type)
		{
			if (((IGeoShapeFilter)this)._Shape == null)
				((IGeoShapeFilter)this)._Shape = new GeoShapeVector();
			((IGeoShapeFilter)this)._Shape.Type = type;
			return this;
		}

		public GeoShapeFilterDescriptor Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (((IGeoShapeFilter)this)._Shape == null)
				((IGeoShapeFilter)this)._Shape = new GeoShapeVector();
			((IGeoShapeFilter)this)._Shape.Coordinates = coordinates;
			return this;
		}

	}

}
