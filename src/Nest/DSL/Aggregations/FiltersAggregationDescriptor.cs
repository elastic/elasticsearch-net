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
        IEnumerable<IFilterContainer> Filters { get; set; }
	}

	public class FiltersAggregator : BucketAggregator, IFiltersAggregator
	{
        public IEnumerable<IFilterContainer> Filters { get; set; }
	}

	public class FiltersAggregationDescriptor<T> : BucketAggregationBaseDescriptor<FiltersAggregationDescriptor<T>, T> , IFiltersAggregator 
		where T : class
	{
        IEnumerable<IFilterContainer> IFiltersAggregator.Filters { get; set; }

        public FiltersAggregationDescriptor<T> Filters(params Func<FilterDescriptor<T>, FilterContainer>[] filterDescriptors)
		{
            ((IFiltersAggregator)this).Filters = filterDescriptors.Select(f => f.Invoke(new FilterDescriptor<T>())).ToList();
			return this;
		}
	}
}