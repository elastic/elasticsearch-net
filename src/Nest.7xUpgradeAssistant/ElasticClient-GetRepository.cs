using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.GetRepository(), please update this usage.")]
		public static GetRepositoryResponse GetRepository(this IElasticClient client,
			Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null
		)
			=> client.Snapshot.GetRepository(selector);

		[Obsolete("Moved to client.Snapshot.GetRepository(), please update this usage.")]
		public static GetRepositoryResponse GetRepository(this IElasticClient client, IGetRepositoryRequest request)
			=> client.Snapshot.GetRepository(request);

		[Obsolete("Moved to client.Snapshot.GetRepositoryAsync(), please update this usage.")]
		public static Task<GetRepositoryResponse> GetRepositoryAsync(this IElasticClient client,
			Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.GetRepositoryAsync(selector, ct);

		[Obsolete("Moved to client.Snapshot.GetRepositoryAsync(), please update this usage.")]
		public static Task<GetRepositoryResponse> GetRepositoryAsync(this IElasticClient client, IGetRepositoryRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.GetRepositoryAsync(request, ct);
	}
}
