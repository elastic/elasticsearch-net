using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters.Aggregations;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FiltersAggregatorConverter))]
	public interface IFiltersAggregator : IBucketAggregator
	{
		[JsonProperty("filters")]
		IEnumerable<IQueryContainer> Filters { get; set; }
	}

	public class FiltersAggregator : BucketAggregator, IFiltersAggregator
	{
		public IEnumerable<IQueryContainer> Filters { get; set; }
	}

	public class FiltersAgg : BucketAgg, IFiltersAggregator
	{
		public IEnumerable<IQueryContainer> Filters { get; set; }

		public FiltersAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filters = this;
	}

	public class FiltersAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<FiltersAggregatorDescriptor<T>, IFiltersAggregator, T>
		, IFiltersAggregator
		where T : class
	{
		IEnumerable<IQueryContainer> IFiltersAggregator.Filters { get; set; }

		public FiltersAggregatorDescriptor<T> Filters(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queryDescriptors) =>
			Assign(a=>a.Filters = queryDescriptors?.Select(f => f?.Invoke(new QueryContainerDescriptor<T>()))?.ToListOrNullIfEmpty());

	}
}