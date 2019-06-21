using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.GetAutoFollowPattern(), please update this usage.")]
		public static GetAutoFollowPatternResponse GetAutoFollowPattern(this IElasticClient client,
			Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null
		)
			=> client.CrossClusterReplication.GetAutoFollowPattern(null, selector);

		[Obsolete("Moved to client.CrossClusterReplication.GetAutoFollowPattern(), please update this usage.")]
		public static GetAutoFollowPatternResponse GetAutoFollowPattern(this IElasticClient client, IGetAutoFollowPatternRequest request)
			=> client.CrossClusterReplication.GetAutoFollowPattern(request);

		[Obsolete("Moved to client.CrossClusterReplication.GetAutoFollowPatternAsync(), please update this usage.")]
		public static Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(this IElasticClient client,
			Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.GetAutoFollowPatternAsync(null, selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.GetAutoFollowPatternAsync(), please update this usage.")]
		public static Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(this IElasticClient client, IGetAutoFollowPatternRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.GetAutoFollowPatternAsync(request, ct);
	}
}
