using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(NestedAggregation))]
	public interface INestedAggregation : IBucketAggregation
	{
		[DataMember(Name ="path")]
		Field Path { get; set; }
	}

	public class NestedAggregation : BucketAggregationBase, INestedAggregation
	{
		internal NestedAggregation() { }

		public NestedAggregation(string name) : base(name) { }

		public Field Path { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Nested = this;
	}

	public class NestedAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<NestedAggregationDescriptor<T>, INestedAggregation, T>
			, INestedAggregation
		where T : class
	{
		Field INestedAggregation.Path { get; set; }

		public NestedAggregationDescriptor<T> Path(Field path) => Assign(a => a.Path = path);

		public NestedAggregationDescriptor<T> Path(Expression<Func<T, object>> path) => Assign(a => a.Path = path);
	}
}
