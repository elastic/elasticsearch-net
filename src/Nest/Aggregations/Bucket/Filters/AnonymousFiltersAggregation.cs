using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<AnonymousFiltersAggregation>))]
	public interface IAnonymousFiltersAggregation : IFiltersAggregation
	{
		[JsonProperty("filters")]
		List<QueryContainer> Filters { get; set; }
	}

	public class AnonymousFiltersAggregation : FiltersAggregationBase, IAnonymousFiltersAggregation
	{
		public override string TypeName => "anonymous_filters";

		public List<QueryContainer> Filters { get; set; }

		public AnonymousFiltersAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filters = this;
	}

	public class AnonymousFiltersAggregationDescriptor<T>
		: FiltersAggregationDescriptorBase<AnonymousFiltersAggregationDescriptor<T>, IAnonymousFiltersAggregation, T>
		, IAnonymousFiltersAggregation
		where T : class
	{
		public override string TypeName => "anonymous_filters";

		List<QueryContainer> IAnonymousFiltersAggregation.Filters { get; set; }

		public AnonymousFiltersAggregationDescriptor<T> Filters(params Func<QueryContainerDescriptor<T>, QueryContainer>[] selectors) =>
			Assign(a => a.Filters = selectors.Select(s=>s?.InvokeQuery(new QueryContainerDescriptor<T>())).ToList());

		public AnonymousFiltersAggregationDescriptor<T> Filters(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> selectors) =>
			Assign(a => a.Filters = selectors.Select(s=>s?.InvokeQuery(new QueryContainerDescriptor<T>())).ToList());
	}
}
