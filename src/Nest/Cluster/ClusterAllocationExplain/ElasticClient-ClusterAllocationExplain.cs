using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster allocation explanation API is designed to assist in answering the question "why is this shard unassigned?"
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html">https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the cluster allocation explain operation</param>
		IClusterAllocationExplainResponse ClusterAllocationExplain(
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null
		);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		IClusterAllocationExplainResponse ClusterAllocationExplain(IClusterAllocationExplainRequest request);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		Task<IClusterAllocationExplainResponse> ClusterAllocationExplainAsync(
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		Task<IClusterAllocationExplainResponse> ClusterAllocationExplainAsync(
			IClusterAllocationExplainRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public IClusterAllocationExplainResponse ClusterAllocationExplain(
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null
		) =>
			ClusterAllocationExplain(selector.InvokeOrDefault(new ClusterAllocationExplainDescriptor()));

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public IClusterAllocationExplainResponse ClusterAllocationExplain(IClusterAllocationExplainRequest request) =>
			Dispatch2<IClusterAllocationExplainRequest, ClusterAllocationExplainResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public Task<IClusterAllocationExplainResponse> ClusterAllocationExplainAsync(
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null,
			CancellationToken ct = default
		) =>
			ClusterAllocationExplainAsync(selector.InvokeOrDefault(new ClusterAllocationExplainDescriptor()), ct);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public Task<IClusterAllocationExplainResponse> ClusterAllocationExplainAsync(
			IClusterAllocationExplainRequest request,
			CancellationToken ct = default
		) =>
			Dispatch2Async<IClusterAllocationExplainRequest, IClusterAllocationExplainResponse, ClusterAllocationExplainResponse>
				(request, request.RequestParameters, ct);
	}
}
