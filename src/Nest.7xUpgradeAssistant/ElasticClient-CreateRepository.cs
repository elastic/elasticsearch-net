using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.CreateRepository(), please update this usage.")]
		public static CreateRepositoryResponse CreateRepository(this IElasticClient client, Name repository,
			Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector
		)
			=> client.Snapshot.CreateRepository(repository, selector);

		[Obsolete("Moved to client.Snapshot.CreateRepository(), please update this usage.")]
		public static CreateRepositoryResponse CreateRepository(this IElasticClient client, ICreateRepositoryRequest request)
			=> client.Snapshot.CreateRepository(request);

		[Obsolete("Moved to client.Snapshot.CreateRepositoryAsync(), please update this usage.")]
		public static Task<CreateRepositoryResponse> CreateRepositoryAsync(this IElasticClient client, Name repository,
			Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector,
			CancellationToken ct = default
		)
			=> client.Snapshot.CreateRepositoryAsync(repository, selector, ct);

		[Obsolete("Moved to client.Snapshot.CreateRepositoryAsync(), please update this usage.")]
		public static Task<CreateRepositoryResponse> CreateRepositoryAsync(this IElasticClient client, ICreateRepositoryRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.CreateRepositoryAsync(request, ct);
	}
}
