using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<AdjacencyMatrixAggregation>))]
	public interface IAdjacencyMatrixAggregation : IBucketAggregation
	{
		[JsonProperty("filters")]
		INamedFiltersContainer Filters { get; set; }
	}

	public class AdjacencyMatrixAggregation : BucketAggregationBase, IAdjacencyMatrixAggregation
	{
		internal AdjacencyMatrixAggregation() { }

		public AdjacencyMatrixAggregation(string name) : base(name) { }

		public INamedFiltersContainer Filters { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.AdjacencyMatrix = this;
	}

	public class AdjacencyMatrixAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<AdjacencyMatrixAggregationDescriptor<T>, IAdjacencyMatrixAggregation, T>
		, IAdjacencyMatrixAggregation
		where T : class
	{
		INamedFiltersContainer IAdjacencyMatrixAggregation.Filters { get; set; }

		public AdjacencyMatrixAggregationDescriptor<T> Filters(Func<NamedFiltersContainerDescriptor<T>, IPromise<INamedFiltersContainer>> selector) =>
			Assign(a => a.Filters = selector?.Invoke(new NamedFiltersContainerDescriptor<T>())?.Value);

	}
}
