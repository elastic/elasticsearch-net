using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatRepositoriesRecord> CatRepositories(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatRepositoriesRecord> CatRepositories(ICatRepositoriesRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatRepositoriesRecord> CatRepositories(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null) =>
			this.CatRepositories(selector.InvokeOrDefault(new CatRepositoriesDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatRepositoriesRecord> CatRepositories(ICatRepositoriesRequest request) =>
			this.DoCat<ICatRepositoriesRequest, CatRepositoriesRequestParameters, CatRepositoriesRecord>(request, this.LowLevelDispatch.CatRepositoriesDispatch<CatResponse<CatRepositoriesRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatRepositoriesAsync(selector.InvokeOrDefault(new CatRepositoriesDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatRepositoriesRequest, CatRepositoriesRequestParameters, CatRepositoriesRecord>(request, cancellationToken, this.LowLevelDispatch.CatRepositoriesDispatchAsync<CatResponse<CatRepositoriesRecord>>);

	}
}
