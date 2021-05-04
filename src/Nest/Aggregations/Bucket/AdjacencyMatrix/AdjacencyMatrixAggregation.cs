// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
			Assign(selector, (a, v) => a.Filters = v?.Invoke(new NamedFiltersContainerDescriptor<T>())?.Value);
	}
}
