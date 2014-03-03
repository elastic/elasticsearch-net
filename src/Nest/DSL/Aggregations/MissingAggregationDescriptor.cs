using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class MissingAggregationDescriptor<T> : BucketAggregationBaseDescriptor<MissingAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public MissingAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public MissingAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}
	}
}