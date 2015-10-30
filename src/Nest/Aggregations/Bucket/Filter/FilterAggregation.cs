using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FilterAggregationJsonConverter))]
	public interface IFilterAggregation : IBucketAggregation
	{
		IQueryContainer Filter { get; set; }
	}

	public class FilterAggregation : BucketAggregation, IFilterAggregation
	{
		private IFilterAggregation Self => this;

		IQueryContainer IFilterAggregation.Filter { get; set; }
		[JsonProperty(PropertyName = "filter")]
		public QueryContainer Filter { get { return Self.Filter as QueryContainer; } set { Self.Filter = value; } }

		public FilterAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filter = this;
	}

	public class FilterAggregationDescriptor<T> 
		: BucketAggregationDescriptorBase<FilterAggregationDescriptor<T>,IFilterAggregation, T> 
			, IFilterAggregation 
		where T : class
	{
		IQueryContainer IFilterAggregation.Filter { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a=> a.Filter = selector?.Invoke(new QueryContainerDescriptor<T>()));

	}
}