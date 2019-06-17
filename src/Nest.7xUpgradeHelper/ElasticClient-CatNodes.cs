using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatNodesRecord> CatNodes(this IElasticClient client, Func<CatNodesDescriptor, ICatNodesRequest> selector = null)
			=> client.Cat.Nodes(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatNodesRecord> CatNodes(this IElasticClient client, ICatNodesRequest request)
			=> client.Cat.Nodes(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatNodesRecord>> CatNodesAsync(this IElasticClient client,
			Func<CatNodesDescriptor, ICatNodesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.NodesAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatNodesRecord>> CatNodesAsync(this IElasticClient client, ICatNodesRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.NodesAsync(request, ct);
	}
}
