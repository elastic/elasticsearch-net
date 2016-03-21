using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatRepositoriesRecord> CatRepositories(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatRepositoriesRecord> CatRepositories(ICatRepositoriesRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request);

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
		public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null) =>
			this.CatRepositoriesAsync(selector.InvokeOrDefault(new CatRepositoriesDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request) =>
			this.DoCatAsync<ICatRepositoriesRequest, CatRepositoriesRequestParameters, CatRepositoriesRecord>(request, this.LowLevelDispatch.CatRepositoriesDispatchAsync<CatResponse<CatRepositoriesRecord>>);

	}
}
