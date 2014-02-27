using System;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{
	public class NestedAggregationDescriptor<T> : BucketAggregationBaseDescriptor<NestedAggregationDescriptor<T>, T>
		where T : class
	{
		internal class NestedAgg
		{
			[JsonProperty("path")] 
			internal PropertyPathMarker _Path;
		}

		[JsonProperty("nested")]
		internal NestedAgg _Nested;


		public NestedAggregationDescriptor<T> Path(string path)
		{
			this._Nested = new NestedAgg();
			this._Nested._Path = path;
			return this;
		}

		public NestedAggregationDescriptor<T> Path(Expression<Func<T, object>> path)
		{
			this._Nested = new NestedAgg();
			this._Nested._Path = path;
			return this;
		}
	}
}