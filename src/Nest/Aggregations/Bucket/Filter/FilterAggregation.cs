using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FilterAggregatorJsonConverter))]
	public interface IFilterAggregator : IBucketAggregator
	{
		IQueryContainer Filter { get; set; }
	}

	public class FilterAggregator : BucketAggregator, IFilterAggregator
	{
		private IFilterAggregator Self => this;

		IQueryContainer IFilterAggregator.Filter { get; set; }
		[JsonProperty(PropertyName = "filter")]
		public QueryContainer Filter { get { return Self.Filter as QueryContainer; } set { Self.Filter = value; } }
	}

	public class FilterAgg : BucketAgg, IFilterAggregator
	{
		private IFilterAggregator Self => this;

		IQueryContainer IFilterAggregator.Filter { get; set; }
		[JsonProperty(PropertyName = "filter")]
		public QueryContainer Filter { get { return Self.Filter as QueryContainer; } set { Self.Filter = value; } }

		public FilterAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filter = this;
	}

	public class FilterAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<FilterAggregatorDescriptor<T>,IFilterAggregator, T> 
			, IFilterAggregator 
		where T : class
	{
		IQueryContainer IFilterAggregator.Filter { get; set; }

		public FilterAggregatorDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a=> a.Filter = selector?.Invoke(new QueryContainerDescriptor<T>()));

	}
}