using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a new named collection of auto-follow patterns against the remote cluster specified
		/// in the request body. Newly created indices on the remote cluster matching any of the specified patterns
		/// will be automatically configured as follower indices.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CreateAutoFollowPatternResponse CreateAutoFollowPattern(this IElasticClient client, Name name,
			Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector
		)
			=> client.CrossClusterReplication.CreateAutoFollowPattern(name, selector);

		/// <inheritdoc
		///     cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CreateAutoFollowPatternResponse CreateAutoFollowPattern(this IElasticClient client, ICreateAutoFollowPatternRequest request)
			=> client.CrossClusterReplication.CreateAutoFollowPattern(request);

		/// <inheritdoc
		///     cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(this IElasticClient client, Name name,
			Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.CreateAutoFollowPatternAsync(name, selector, ct);

		/// <inheritdoc
		///     cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(this IElasticClient client,
			ICreateAutoFollowPatternRequest request, CancellationToken ct = default
		)
			=> client.CrossClusterReplication.CreateAutoFollowPatternAsync(request, ct);
	}
}
