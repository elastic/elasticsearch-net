using System;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<NestedAggregator>))]
	public interface INestedAggregator : IBucketAggregator
	{
		[JsonProperty("path")] 
		PropertyPath Path { get; set;}
	}

	public class NestedAggregator : BucketAggregator, INestedAggregator
	{
		public PropertyPath Path { get; set; }
	}

	public class NestedAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<NestedAggregatorDescriptor<T>, INestedAggregator, T>
			, INestedAggregator 
		where T : class
	{
		PropertyPath INestedAggregator.Path { get; set; }

		public NestedAggregatorDescriptor<T> Path(string path) => Assign(a => a.Path = path);

		public NestedAggregatorDescriptor<T> Path(Expression<Func<T, object>> path) => Assign(a => a.Path = path);
	}
}