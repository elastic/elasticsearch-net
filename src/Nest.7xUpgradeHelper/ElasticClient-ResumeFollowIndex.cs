using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Resumes a follower index that has been paused either explicitly with the pause follower API or
		/// implicitly due to execution that can not be retried due to failure during following. When this API returns,
		/// the follower index will resume fetching operations from the leader index.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ResumeFollowIndexResponse ResumeFollowIndex(this IElasticClient client, IndexName index,
			Func<ResumeFollowIndexDescriptor, IResumeFollowIndexRequest> selector = null
		)
			=> client.CrossClusterReplication.ResumeFollowIndex(index, selector);

		/// <inheritdoc
		///     cref="ResumeFollowIndex(IndexName, System.Func{Nest.ResumeFollowIndexDescriptor,Nest.IResumeFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ResumeFollowIndexResponse ResumeFollowIndex(this IElasticClient client, IResumeFollowIndexRequest request)
			=> client.CrossClusterReplication.ResumeFollowIndex(request);

		/// <inheritdoc
		///     cref="ResumeFollowIndex(IndexName, System.Func{Nest.ResumeFollowIndexDescriptor,Nest.IResumeFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ResumeFollowIndexResponse> ResumeFollowIndexAsync(this IElasticClient client, IndexName index,
			Func<ResumeFollowIndexDescriptor, IResumeFollowIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.ResumeFollowIndexAsync(index, selector, ct);

		/// <inheritdoc
		///     cref="ResumeFollowIndex(IndexName, System.Func{Nest.ResumeFollowIndexDescriptor,Nest.IResumeFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ResumeFollowIndexResponse> ResumeFollowIndexAsync(this IElasticClient client, IResumeFollowIndexRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.ResumeFollowIndexAsync(request, ct);
	}
}
