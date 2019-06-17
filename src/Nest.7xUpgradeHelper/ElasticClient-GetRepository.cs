using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRepositoryResponse GetRepository(this IElasticClient client,
			Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null
		)
			=> client.Snapshot.GetRepository(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRepositoryResponse GetRepository(this IElasticClient client, IGetRepositoryRequest request)
			=> client.Snapshot.GetRepository(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRepositoryResponse> GetRepositoryAsync(this IElasticClient client,
			Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.GetRepositoryAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRepositoryResponse> GetRepositoryAsync(this IElasticClient client, IGetRepositoryRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.GetRepositoryAsync(request, ct);
	}
}
