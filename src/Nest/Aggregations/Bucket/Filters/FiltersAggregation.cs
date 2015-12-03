using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<FiltersAggregation>))]
	public interface IFiltersAggregation : IBucketAggregation
	{
		[JsonProperty("filters")]
		Union<INamedFiltersContainer, List<QueryContainer>> Filters { get; set; }
	}

	public class FiltersAggregation : BucketAggregationBase, IFiltersAggregation
	{
		public Union<INamedFiltersContainer, List<QueryContainer>> Filters { get; set; }

		internal FiltersAggregation() { }

		public FiltersAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filters = this;
	}

	public class FiltersAggregationDescriptor<T> 
		: BucketAggregationDescriptorBase<FiltersAggregationDescriptor<T>, IFiltersAggregation, T>
		, IFiltersAggregation
		where T : class
	{
		Union<INamedFiltersContainer, List<QueryContainer>> IFiltersAggregation.Filters { get; set; }

		public FiltersAggregationDescriptor<T> NamedFilters(Func<NamedFiltersContainerDescriptor<T>, IPromise<INamedFiltersContainer>> selector) =>
			Assign(a => a.Filters = new Union<INamedFiltersContainer, List<QueryContainer>>(selector?.Invoke(new NamedFiltersContainerDescriptor<T>())?.Value));

		public FiltersAggregationDescriptor<T> AnonymousFilters(params Func<QueryContainerDescriptor<T>, QueryContainer>[] selectors) =>
			Assign(a => a.Filters = selectors.Select(s=>s?.InvokeQuery(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		public FiltersAggregationDescriptor<T> AnonymousFilters(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> selectors) =>
			Assign(a => a.Filters = selectors.Select(s=>s?.InvokeQuery(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

	}
}