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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatRepositoriesRecord> CatRepositories(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null) =>
			CatRepositories(selector.InvokeOrDefault(new CatRepositoriesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatRepositoriesRecord> CatRepositories(ICatRepositoriesRequest request) =>
			DoCat<ICatRepositoriesRequest, CatRepositoriesRequestParameters, CatRepositoriesRecord>(request,
				LowLevelDispatch.CatRepositoriesDispatch<CatResponse<CatRepositoriesRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CatRepositoriesAsync(selector.InvokeOrDefault(new CatRepositoriesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatRepositoriesRequest, CatRepositoriesRequestParameters, CatRepositoriesRecord>(request, cancellationToken,
				LowLevelDispatch.CatRepositoriesDispatchAsync<CatResponse<CatRepositoriesRecord>>);
	}
}
