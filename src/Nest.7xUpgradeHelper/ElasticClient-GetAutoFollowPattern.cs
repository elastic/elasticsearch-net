using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary> Gets configured auto-follow patterns. Returns the specified auto-follow pattern collection. </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetAutoFollowPatternResponse GetAutoFollowPattern(this IElasticClient client,
			Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null
		)
			=> client.CrossClusterReplication.GetAutoFollowPattern(null, selector);

		/// <inheritdoc
		///     cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetAutoFollowPatternResponse GetAutoFollowPattern(this IElasticClient client, IGetAutoFollowPatternRequest request)
			=> client.CrossClusterReplication.GetAutoFollowPattern(request);

		/// <inheritdoc
		///     cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(this IElasticClient client,
			Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.GetAutoFollowPatternAsync(null, selector, ct);

		/// <inheritdoc
		///     cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(this IElasticClient client, IGetAutoFollowPatternRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.GetAutoFollowPatternAsync(request, ct);
	}
}
