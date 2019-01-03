using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary></summary>
		IFollowIndexStatsResponse FollowIndexStats(Indices indices, Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null);

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		IFollowIndexStatsResponse FollowIndexStats(IFollowIndexStatsRequest request);

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		Task<IFollowIndexStatsResponse> FollowIndexStatsAsync(Indices indices, Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		Task<IFollowIndexStatsResponse> FollowIndexStatsAsync(IFollowIndexStatsRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		public IFollowIndexStatsResponse FollowIndexStats(Indices indices, Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null) =>
			FollowIndexStats(selector.InvokeOrDefault(new FollowIndexStatsDescriptor().Index(indices)));

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		public IFollowIndexStatsResponse FollowIndexStats(IFollowIndexStatsRequest request) =>
			Dispatcher.Dispatch<IFollowIndexStatsRequest, FollowIndexStatsRequestParameters, FollowIndexStatsResponse>(
				request,
				(p, d) => LowLevelDispatch.CcrFollowStatsDispatch<FollowIndexStatsResponse>(p)
			);

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		public Task<IFollowIndexStatsResponse> FollowIndexStatsAsync(Indices indices, Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			FollowIndexStatsAsync(selector.InvokeOrDefault(new FollowIndexStatsDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		public Task<IFollowIndexStatsResponse> FollowIndexStatsAsync(IFollowIndexStatsRequest request, CancellationToken cancellationToken = default) =>
			Dispatcher.DispatchAsync<IFollowIndexStatsRequest, FollowIndexStatsRequestParameters, FollowIndexStatsResponse, IFollowIndexStatsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.CcrFollowStatsDispatchAsync<FollowIndexStatsResponse>(p, c)
			);
	}
}
