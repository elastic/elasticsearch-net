using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.CreateAutoFollowPattern(), please update this usage.")]
		public static CreateAutoFollowPatternResponse CreateAutoFollowPattern(this IElasticClient client, Name name,
			Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector
		)
			=> client.CrossClusterReplication.CreateAutoFollowPattern(name, selector);

		[Obsolete("Moved to client.CrossClusterReplication.CreateAutoFollowPattern(), please update this usage.")]
		public static CreateAutoFollowPatternResponse CreateAutoFollowPattern(this IElasticClient client, ICreateAutoFollowPatternRequest request)
			=> client.CrossClusterReplication.CreateAutoFollowPattern(request);

		[Obsolete("Moved to client.CrossClusterReplication.CreateAutoFollowPatternAsync(), please update this usage.")]
		public static Task<CreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(this IElasticClient client, Name name,
			Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.CreateAutoFollowPatternAsync(name, selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.CreateAutoFollowPatternAsync(), please update this usage.")]
		public static Task<CreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(this IElasticClient client,
			ICreateAutoFollowPatternRequest request, CancellationToken ct = default
		)
			=> client.CrossClusterReplication.CreateAutoFollowPatternAsync(request, ct);
	}
}
