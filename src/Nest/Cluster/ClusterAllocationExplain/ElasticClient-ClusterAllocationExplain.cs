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
		ClusterAllocationExplainResponse ClusterAllocationExplain(
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null
		);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		ClusterAllocationExplainResponse ClusterAllocationExplain(IClusterAllocationExplainRequest request);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		Task<ClusterAllocationExplainResponse> ClusterAllocationExplainAsync(
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		Task<ClusterAllocationExplainResponse> ClusterAllocationExplainAsync(
			IClusterAllocationExplainRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public ClusterAllocationExplainResponse ClusterAllocationExplain(
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null
		) =>
			ClusterAllocationExplain(selector.InvokeOrDefault(new ClusterAllocationExplainDescriptor()));

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public ClusterAllocationExplainResponse ClusterAllocationExplain(IClusterAllocationExplainRequest request) =>
			DoRequest<IClusterAllocationExplainRequest, ClusterAllocationExplainResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public Task<ClusterAllocationExplainResponse> ClusterAllocationExplainAsync(
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null,
			CancellationToken ct = default
		) =>
			ClusterAllocationExplainAsync(selector.InvokeOrDefault(new ClusterAllocationExplainDescriptor()), ct);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public Task<ClusterAllocationExplainResponse> ClusterAllocationExplainAsync(
			IClusterAllocationExplainRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IClusterAllocationExplainRequest, ClusterAllocationExplainResponse, ClusterAllocationExplainResponse>
				(request, request.RequestParameters, ct);
	}
}
