using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>Deletes a configured collection of auto-follow patterns.</summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteAutoFollowPatternResponse DeleteAutoFollowPattern(this IElasticClient client, Name name,
			Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null
		)
			=> client.CrossClusterReplication.DeleteAutoFollowPattern(name, selector);

		/// <inheritdoc
		///     cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteAutoFollowPatternResponse DeleteAutoFollowPattern(this IElasticClient client, IDeleteAutoFollowPatternRequest request)
			=> client.CrossClusterReplication.DeleteAutoFollowPattern(request);

		/// <inheritdoc
		///     cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(this IElasticClient client, Name name,
			Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.DeleteAutoFollowPatternAsync(name, selector, ct);

		/// <inheritdoc
		///     cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(this IElasticClient client,
			IDeleteAutoFollowPatternRequest request, CancellationToken ct = default
		)
			=> client.CrossClusterReplication.DeleteAutoFollowPatternAsync(request, ct);
	}
}
