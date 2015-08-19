using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<NestedAggregator>))]
	public interface INestedAggregator : IBucketAggregator
	{
		[JsonProperty("path")] 
		FieldName Path { get; set;}
	}

	public class NestedAggregator : BucketAggregator, INestedAggregator
	{
		public FieldName Path { get; set; }
	}

	public class NestedAgg : BucketAgg, INestedAggregator
	{
		public FieldName Path { get; set; }

		public NestedAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Nested = this;
	}

	public class NestedAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<NestedAggregatorDescriptor<T>, INestedAggregator, T>
			, INestedAggregator 
		where T : class
	{
		FieldName INestedAggregator.Path { get; set; }

		public NestedAggregatorDescriptor<T> Path(string path) => Assign(a => a.Path = path);

		public NestedAggregatorDescriptor<T> Path(Expression<Func<T, object>> path) => Assign(a => a.Path = path);
	}
}