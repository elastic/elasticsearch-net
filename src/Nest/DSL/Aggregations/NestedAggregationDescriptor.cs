using System;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	public class NestedAggregationDescriptor<T> : BucketAggregationBaseDescriptor<NestedAggregationDescriptor<T>, T>
		, ICustomJson
		where T : class
	{
		internal class NestedAgg
		{
			[JsonProperty("path")] 
			internal PropertyPathMarker _Path;
		}

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

		object ICustomJson.GetCustomJson()
		{
			return this._Nested;
		}
	}
}