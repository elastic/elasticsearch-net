using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Pauses a follower index. When this API returns, the follower index will not fetch any additional operations from
		/// the leader index. You can resume following with the resume follower API. Pausing and resuming a follower index can be
		/// used to change the configuration of the following task.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PauseFollowIndexResponse PauseFollowIndex(this IElasticClient client, IndexName index,
			Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null
		)
			=> client.CrossClusterReplication.PauseFollowIndex(index, selector);

		/// <inheritdoc
		///     cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PauseFollowIndexResponse PauseFollowIndex(this IElasticClient client, IPauseFollowIndexRequest request)
			=> client.CrossClusterReplication.PauseFollowIndex(request);

		/// <inheritdoc
		///     cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PauseFollowIndexResponse> PauseFollowIndexAsync(this IElasticClient client, IndexName index,
			Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.PauseFollowIndexAsync(index, selector, ct);

		/// <inheritdoc
		///     cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PauseFollowIndexResponse> PauseFollowIndexAsync(this IElasticClient client, IPauseFollowIndexRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.PauseFollowIndexAsync(request, ct);
	}
}
