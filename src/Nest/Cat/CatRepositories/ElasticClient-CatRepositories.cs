using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatRepositoriesRecord> CatRepositories(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatRepositoriesRecord> CatRepositories(ICatRepositoriesRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatRepositoriesRecord> CatRepositories(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null) =>
			CatRepositories(selector.InvokeOrDefault(new CatRepositoriesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatRepositoriesRecord> CatRepositories(ICatRepositoriesRequest request) =>
			DoCat<ICatRepositoriesRequest, CatRepositoriesRequestParameters, CatRepositoriesRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null,
			CancellationToken ct = default
		) => CatRepositoriesAsync(selector.InvokeOrDefault(new CatRepositoriesDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatRepositoriesRequest, CatRepositoriesRequestParameters, CatRepositoriesRecord>(request, ct);
	}
}
