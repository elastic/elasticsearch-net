using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Resumes a follower index that has been paused either explicitly with the pause follower API or
		/// implicitly due to execution that can not be retried due to failure during following. When this API returns,
		/// the follower index will resume fetching operations from the leader index.
		/// </summary>
		public static ResumeFollowIndexResponse ResumeFollowIndex(this IElasticClient client,IndexName index, Func<ResumeFollowIndexDescriptor, IResumeFollowIndexRequest> selector = null);

		/// <inheritdoc cref="ResumeFollowIndex(IndexName, System.Func{Nest.ResumeFollowIndexDescriptor,Nest.IResumeFollowIndexRequest})" />
		public static ResumeFollowIndexResponse ResumeFollowIndex(this IElasticClient client,IResumeFollowIndexRequest request);

		/// <inheritdoc cref="ResumeFollowIndex(IndexName, System.Func{Nest.ResumeFollowIndexDescriptor,Nest.IResumeFollowIndexRequest})" />
		public static Task<ResumeFollowIndexResponse> ResumeFollowIndexAsync(this IElasticClient client,IndexName index, Func<ResumeFollowIndexDescriptor, IResumeFollowIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ResumeFollowIndex(IndexName, System.Func{Nest.ResumeFollowIndexDescriptor,Nest.IResumeFollowIndexRequest})" />
		public static Task<ResumeFollowIndexResponse> ResumeFollowIndexAsync(this IElasticClient client,IResumeFollowIndexRequest request, CancellationToken ct = default);
	}

}
