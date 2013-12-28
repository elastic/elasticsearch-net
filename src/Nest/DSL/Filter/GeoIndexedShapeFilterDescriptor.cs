using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

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
			this._Shape = new GeoIndexedShapeVector
			{
				Field = field,
				Id = id,
				Type = type ?? new TypeNameMarker { Type = typeof(T) },
				Index = index ?? new IndexNameMarker { Type = typeof(T) }
			};
			return this;
		}

		public GeoIndexedShapeFilterDescriptor Lookup<T>(Expression<Func<T, object>> field, string id, string index = null, string type = null)
		{
			var fieldName = new PropertyNameResolver().Resolve(field);
			return this.Lookup<T>(fieldName, id, index, type);
		}

	}

}
