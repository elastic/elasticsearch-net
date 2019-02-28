using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(AdjacencyMatrixAggregation))]
	public interface IAdjacencyMatrixAggregation : IBucketAggregation
	{
		[DataMember(Name ="filters")]
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
