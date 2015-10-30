using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ReverseNestedAggregation>))]
	public interface IReverseNestedAggregation : IBucketAggregation
	{
		[JsonProperty("path")]
		FieldName Path { get; set; }
	}

	public class ReverseNestedAggregation : BucketAggregation, IReverseNestedAggregation
	{
		[JsonProperty("path")]
		public FieldName Path { get; set; }

		public ReverseNestedAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ReverseNested = this;
	}

	public class ReverseNestedAggregationDescriptor<T> 
		: BucketAggregationDescriptorBase<ReverseNestedAggregationDescriptor<T>,IReverseNestedAggregation, T>
			, IReverseNestedAggregation 
		where T : class
	{
		FieldName IReverseNestedAggregation.Path { get; set; }

		public ReverseNestedAggregationDescriptor<T> Path(string path) => Assign(a => a.Path = path);

		public ReverseNestedAggregationDescriptor<T> Path(Expression<Func<T, object>> path) => Assign(a => a.Path = path);
	}
}
