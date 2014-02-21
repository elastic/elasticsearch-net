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
	public class GeoIndexedShapeFilterDescriptor : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return this._Shape == null || this._Shape.Id.IsNullOrEmpty();
			}

		}

		[JsonProperty("indexed_shape")]
		internal GeoIndexedShapeVector _Shape { get; set; }


		public GeoIndexedShapeFilterDescriptor Lookup<T>(string field, string id, string index = null, string type = null)
		{
			return _SetShape<T>(field, id, index, type);
		}

		private GeoIndexedShapeFilterDescriptor _SetShape<T>(PropertyPathMarker field, string id, string index, string type)
		{
			this._Shape = new GeoIndexedShapeVector
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
