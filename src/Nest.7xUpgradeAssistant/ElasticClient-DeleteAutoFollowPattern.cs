using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.DeleteAutoFollowPattern(), please update this usage.")]
		public static DeleteAutoFollowPatternResponse DeleteAutoFollowPattern(this IElasticClient client, Name name,
			Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null
		)
			=> client.CrossClusterReplication.DeleteAutoFollowPattern(name, selector);

		[Obsolete("Moved to client.CrossClusterReplication.DeleteAutoFollowPattern(), please update this usage.")]
		public static DeleteAutoFollowPatternResponse DeleteAutoFollowPattern(this IElasticClient client, IDeleteAutoFollowPatternRequest request)
			=> client.CrossClusterReplication.DeleteAutoFollowPattern(request);

		[Obsolete("Moved to client.CrossClusterReplication.DeleteAutoFollowPatternAsync(), please update this usage.")]
		public static Task<DeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(this IElasticClient client, Name name,
			Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.DeleteAutoFollowPatternAsync(name, selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.DeleteAutoFollowPatternAsync(), please update this usage.")]
		public static Task<DeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(this IElasticClient client,
			IDeleteAutoFollowPatternRequest request, CancellationToken ct = default
		)
			=> client.CrossClusterReplication.DeleteAutoFollowPatternAsync(request, ct);
	}
}
