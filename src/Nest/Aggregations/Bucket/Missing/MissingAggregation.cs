using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MissingAggregation))]
	public interface IMissingAggregation : IBucketAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }
	}

	public class MissingAggregation : BucketAggregationBase, IMissingAggregation
	{
		internal MissingAggregation() { }

		public MissingAggregation(string name) : base(name) { }

		public Field Field { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Missing = this;
	}

	public class MissingAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<MissingAggregationDescriptor<T>, IMissingAggregation, T>
			, IMissingAggregation
		where T : class
	{
		Field IMissingAggregation.Field { get; set; }

		public MissingAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public MissingAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);
	}
}
