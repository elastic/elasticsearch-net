using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<MissingAggregation>))]
	public interface IMissingAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class MissingAggregation : BucketAggregationBase, IMissingAggregation
	{
		public Field Field { get; set; }

		internal MissingAggregation() { }

		public MissingAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Missing = this;
	}

	public class MissingAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<MissingAggregationDescriptor<T>,IMissingAggregation, T>
			, IMissingAggregation
		where T : class
	{
		Field IMissingAggregation.Field { get; set; }

		public MissingAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public MissingAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);
	}
}
