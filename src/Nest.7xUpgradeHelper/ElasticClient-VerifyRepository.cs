using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static VerifyRepositoryResponse VerifyRepository(this IElasticClient client,Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null);

		/// <inheritdoc />
		public static VerifyRepositoryResponse VerifyRepository(this IElasticClient client,IVerifyRepositoryRequest request);

		/// <inheritdoc />
		public static Task<VerifyRepositoryResponse> VerifyRepositoryAsync(this IElasticClient client,Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<VerifyRepositoryResponse> VerifyRepositoryAsync(this IElasticClient client,IVerifyRepositoryRequest request,
			CancellationToken ct = default
		);
	}

}
