using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGeoIndexedShapeFilter : IFilterBase
	{
		[JsonProperty("indexed_shape")]
		GeoIndexedShapeVector _Shape { get; set; }
	}

	public class GeoIndexedShapeFilterDescriptor : FilterBase, IGeoIndexedShapeFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IGeoIndexedShapeFilter)this)._Shape == null || ((IGeoIndexedShapeFilter)this)._Shape.Id.IsNullOrEmpty();
			}

		}

		GeoIndexedShapeVector IGeoIndexedShapeFilter._Shape { get; set; }


		public GeoIndexedShapeFilterDescriptor Lookup<T>(string field, string id, string index = null, string type = null)
		{
			return _SetShape<T>(field, id, index, type);
		}

		private GeoIndexedShapeFilterDescriptor _SetShape<T>(PropertyPathMarker field, string id, string index, string type)
		{
			((IGeoIndexedShapeFilter)this)._Shape = new GeoIndexedShapeVector
			{
				Field = field,
				Id = id,
				Type = type ?? new TypeNameMarker {Type = typeof (T)},
				Index = index ?? new IndexNameMarker {Type = typeof (T)}
			};
			return this;
		}

		public GeoIndexedShapeFilterDescriptor Lookup<T>(Expression<Func<T, object>> field, string id, string index = null, string type = null)
		{
			return _SetShape<T>(field, id, index, type);
		}

	}

}
