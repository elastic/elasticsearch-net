using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster allocation explanation API is designed to assist in answering the question "why is this shard unassigned?"
		/// <para> </para><a href="https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html">https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the cluster allocation explain operation</param>
		IClusterAllocationExplainResponse ClusterAllocationExplain(Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null);

		/// <inheritdoc/>
		IClusterAllocationExplainResponse ClusterAllocationExplain(IClusterAllocationExplainRequest request);

		/// <inheritdoc/>
		Task<IClusterAllocationExplainResponse> ClusterAllocationExplainAsync(Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IClusterAllocationExplainResponse> ClusterAllocationExplainAsync(IClusterAllocationExplainRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterAllocationExplainResponse ClusterAllocationExplain(Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null) =>
			this.ClusterAllocationExplain(selector.InvokeOrDefault(new ClusterAllocationExplainDescriptor()));

		/// <inheritdoc/>
		public IClusterAllocationExplainResponse ClusterAllocationExplain(IClusterAllocationExplainRequest request) =>
			this.Dispatcher.Dispatch<IClusterAllocationExplainRequest, ClusterAllocationExplainRequestParameters, ClusterAllocationExplainResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ClusterAllocationExplainDispatch<ClusterAllocationExplainResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IClusterAllocationExplainResponse> ClusterAllocationExplainAsync(Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ClusterAllocationExplainAsync(selector.InvokeOrDefault(new ClusterAllocationExplainDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IClusterAllocationExplainResponse> ClusterAllocationExplainAsync(IClusterAllocationExplainRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IClusterAllocationExplainRequest, ClusterAllocationExplainRequestParameters, ClusterAllocationExplainResponse, IClusterAllocationExplainResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.ClusterAllocationExplainDispatchAsync<ClusterAllocationExplainResponse>(p, d, c)
			);
	}
}
