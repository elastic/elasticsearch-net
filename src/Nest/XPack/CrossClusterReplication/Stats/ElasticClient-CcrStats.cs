using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// This API gets cross-cluster replication stats. This API will return all stats related to cross-cluster replication.
		/// In particular, this API returns stats about auto-following, and returns the same shard-level stats as in the get
		/// follower stats API. <see cref="IElasticClient.FollowIndexStats(Nest.Indices,System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})"/>
		/// </summary>
		ICcrStatsResponse CcrStats(Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		ICcrStatsResponse CcrStats(ICcrStatsRequest request);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		Task<ICcrStatsResponse> CcrStatsAsync(Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		Task<ICcrStatsResponse> CcrStatsAsync(ICcrStatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public ICcrStatsResponse CcrStats(Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null) =>
			CcrStats(selector.InvokeOrDefault(new CcrStatsDescriptor()));

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public ICcrStatsResponse CcrStats(ICcrStatsRequest request) =>
			Dispatcher.Dispatch<ICcrStatsRequest, CcrStatsRequestParameters, CcrStatsResponse>(
				request,
				(p, d) => LowLevelDispatch.CcrStatsDispatch<CcrStatsResponse>(p)
			);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public Task<ICcrStatsResponse> CcrStatsAsync(Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CcrStatsAsync(selector.InvokeOrDefault(new CcrStatsDescriptor()), cancellationToken);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public Task<ICcrStatsResponse> CcrStatsAsync(ICcrStatsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<ICcrStatsRequest, CcrStatsRequestParameters, CcrStatsResponse, ICcrStatsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.CcrStatsDispatchAsync<CcrStatsResponse>(p, c)
			);
	}
}
