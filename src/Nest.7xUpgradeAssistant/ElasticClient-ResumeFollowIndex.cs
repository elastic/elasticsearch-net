using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.ResumeFollowIndex(), please update this usage.")]
		public static ResumeFollowIndexResponse ResumeFollowIndex(this IElasticClient client, IndexName index,
			Func<ResumeFollowIndexDescriptor, IResumeFollowIndexRequest> selector = null
		)
			=> client.CrossClusterReplication.ResumeFollowIndex(index, selector);

		[Obsolete("Moved to client.CrossClusterReplication.ResumeFollowIndex(), please update this usage.")]
		public static ResumeFollowIndexResponse ResumeFollowIndex(this IElasticClient client, IResumeFollowIndexRequest request)
			=> client.CrossClusterReplication.ResumeFollowIndex(request);

		[Obsolete("Moved to client.CrossClusterReplication.ResumeFollowIndexAsync(), please update this usage.")]
		public static Task<ResumeFollowIndexResponse> ResumeFollowIndexAsync(this IElasticClient client, IndexName index,
			Func<ResumeFollowIndexDescriptor, IResumeFollowIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.ResumeFollowIndexAsync(index, selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.ResumeFollowIndexAsync(), please update this usage.")]
		public static Task<ResumeFollowIndexResponse> ResumeFollowIndexAsync(this IElasticClient client, IResumeFollowIndexRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.ResumeFollowIndexAsync(request, ct);
	}
}
