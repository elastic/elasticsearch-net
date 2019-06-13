using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetRepositoryResponse GetRepository(this IElasticClient client,Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null);

		/// <inheritdoc />
		public static GetRepositoryResponse GetRepository(this IElasticClient client,IGetRepositoryRequest request);

		/// <inheritdoc />
		public static Task<GetRepositoryResponse> GetRepositoryAsync(this IElasticClient client,Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetRepositoryResponse> GetRepositoryAsync(this IElasticClient client,IGetRepositoryRequest request,
			CancellationToken ct = default
		);
	}

}
