using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MissingAggregator>))]
	public interface IMissingAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class MissingAggregator : BucketAggregator, IMissingAggregator
	{
		public Field Field { get; set; }
	}

	public class MissingAgg : BucketAgg, IMissingAggregator
	{
		public Field Field { get; set; }

		public MissingAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Missing = this;
	}

	public class MissingAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<MissingAggregatorDescriptor<T>,IMissingAggregator, T>
			, IMissingAggregator 
		where T : class
	{
		Field IMissingAggregator.Field { get; set; }

		public MissingAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public MissingAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

	}
}