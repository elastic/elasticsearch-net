using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FiltersAggregator>))]
	public interface IFiltersAggregator : IBucketAggregator
	{
		[JsonProperty("filters")]
		Union<INamedFiltersContainer, List<IQueryContainer>> Filters { get; set; }
	}

	public class FiltersAggregator : BucketAggregator, IFiltersAggregator
	{
		public Union<INamedFiltersContainer, List<IQueryContainer>> Filters { get; set; }
	}

	public class FiltersAgg : BucketAgg, IFiltersAggregator
	{
		public Union<INamedFiltersContainer, List<IQueryContainer>> Filters { get; set; }

		public FiltersAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filters = this;
	}

	public class FiltersAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<FiltersAggregatorDescriptor<T>, IFiltersAggregator, T>
		, IFiltersAggregator
		where T : class
	{
		Union<INamedFiltersContainer, List<IQueryContainer>> IFiltersAggregator.Filters { get; set; }

		public FiltersAggregatorDescriptor<T> NamedFilters(Func<NamedFiltersContainerDescriptor<T>, NamedFiltersContainerBase> selector) =>
			Assign(a => a.Filters = selector?.Invoke(new NamedFiltersContainerDescriptor<T>()));

		public FiltersAggregatorDescriptor<T> AnonymousFilters(params Func<QueryContainerDescriptor<T>, IQueryContainer>[] selectors) =>
			Assign(a => a.Filters = selectors.Select(s=>s?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		public FiltersAggregatorDescriptor<T> AnonymousFilters(IEnumerable<Func<QueryContainerDescriptor<T>, IQueryContainer>> selectors) =>
			Assign(a => a.Filters = selectors.Select(s=>s?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

	}
}