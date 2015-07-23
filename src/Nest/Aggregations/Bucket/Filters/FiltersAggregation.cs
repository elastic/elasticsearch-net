using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Aggregations;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FiltersAggregator>))]
	public interface IFiltersAggregator : IBucketAggregator
	{
		[JsonProperty("filters")]
		INamedFiltersContainer Filters { get; set; }
	}

	public class FiltersAggregator : BucketAggregator, IFiltersAggregator
	{
		public INamedFiltersContainer Filters { get; set; }
	}

	public class FiltersAgg : BucketAgg, IFiltersAggregator
	{
		public INamedFiltersContainer Filters { get; set; }

		public FiltersAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filters = this;
	}

	public class FiltersAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<FiltersAggregatorDescriptor<T>, IFiltersAggregator, T>
		, IFiltersAggregator
		where T : class
	{
		INamedFiltersContainer IFiltersAggregator.Filters { get; set; }

		public FiltersAggregatorDescriptor<T> Filters(Func<NamedFiltersContainerDescriptor<T>, INamedFiltersContainer> selector) =>
			Assign(a => a.Filters = selector?.Invoke(new NamedFiltersContainerDescriptor<T>()));

	}
}