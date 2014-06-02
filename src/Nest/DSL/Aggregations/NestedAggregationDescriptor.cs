using System;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<NestedAggregator>))]
	public interface INestedAggregator : IBucketAggregator
	{
		[JsonProperty("path")] 
		PropertyPathMarker Path { get; set;}
	}

	public class NestedAggregator : BucketAggregator, INestedAggregator
	{
		public PropertyPathMarker Path { get; set; }
	}

	public class NestedAggregationDescriptor<T> : BucketAggregationBaseDescriptor<NestedAggregationDescriptor<T>, T>, INestedAggregator where T : class
	{
		PropertyPathMarker INestedAggregator.Path { get; set; }
		
		public NestedAggregationDescriptor<T> Path(string path)
		{
			((INestedAggregator)this).Path = path;
			return this;
		}

		public NestedAggregationDescriptor<T> Path(Expression<Func<T, object>> path)
		{
			((INestedAggregator)this).Path = path;
			return this;
		}
	}
}