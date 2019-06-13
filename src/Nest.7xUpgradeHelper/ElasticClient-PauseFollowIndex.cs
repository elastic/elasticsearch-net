using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Pauses a follower index. When this API returns, the follower index will not fetch any additional operations from
		/// the leader index. You can resume following with the resume follower API. Pausing and resuming a follower index can be
		/// used to change the configuration of the following task.
		/// </summary>
		public static PauseFollowIndexResponse PauseFollowIndex(this IElasticClient client,IndexName index, Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public static PauseFollowIndexResponse PauseFollowIndex(this IElasticClient client,IPauseFollowIndexRequest request);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public static Task<PauseFollowIndexResponse> PauseFollowIndexAsync(this IElasticClient client,IndexName index, Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public static Task<PauseFollowIndexResponse> PauseFollowIndexAsync(this IElasticClient client,IPauseFollowIndexRequest request, CancellationToken ct = default);
	}

}
