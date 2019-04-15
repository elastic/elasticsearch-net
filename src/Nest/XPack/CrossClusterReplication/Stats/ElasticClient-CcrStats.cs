using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets cross-cluster replication stats. This API will return all stats related to cross-cluster replication.
		/// In particular, this API returns stats about auto-following, and returns the same shard-level stats as in the get
		/// follower stats API. <see cref="IElasticClient.FollowIndexStats(Nest.Indices,System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})"/>
		/// </summary>
		CcrStatsResponse CcrStats(Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		CcrStatsResponse CcrStats(ICcrStatsRequest request);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		Task<CcrStatsResponse> CcrStatsAsync(Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		Task<CcrStatsResponse> CcrStatsAsync(ICcrStatsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public CcrStatsResponse CcrStats(Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null) =>
			CcrStats(selector.InvokeOrDefault(new CcrStatsDescriptor()));

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public CcrStatsResponse CcrStats(ICcrStatsRequest request) =>
			DoRequest<ICcrStatsRequest, CcrStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public Task<CcrStatsResponse> CcrStatsAsync(
			Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null,
			CancellationToken ct = default
		) => CcrStatsAsync(selector.InvokeOrDefault(new CcrStatsDescriptor()), ct);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public Task<CcrStatsResponse> CcrStatsAsync(ICcrStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ICcrStatsRequest, CcrStatsResponse, CcrStatsResponse>(request, request.RequestParameters, ct);
	}
}
