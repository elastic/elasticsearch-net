using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<NamedFiltersAggregation>))]
	public interface INamedFiltersAggregation : IFiltersAggregation
	{
		[JsonProperty("filters")]
		INamedFiltersContainer Filters { get; set; }
	}

	public class NamedFiltersAggregation : FiltersAggregationBase, INamedFiltersAggregation
	{
		public override string TypeName => "named_filters";

		public INamedFiltersContainer Filters { get; set; }

		public NamedFiltersAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filters = this;
	}

	public class NamedFiltersAggregationDescriptor<T>
		: FiltersAggregationDescriptorBase<NamedFiltersAggregationDescriptor<T>, INamedFiltersAggregation, T>
		, INamedFiltersAggregation
		where T : class
	{
		public override string TypeName => "named_filters";

		INamedFiltersContainer INamedFiltersAggregation.Filters { get; set; }

		public NamedFiltersAggregationDescriptor<T> Filters(Func<NamedFiltersContainerDescriptor<T>, IPromise<INamedFiltersContainer>> selector) =>
			Assign(a => a.Filters = selector?.Invoke(new NamedFiltersContainerDescriptor<T>())?.Value);
	}
}
