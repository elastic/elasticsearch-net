using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector);

		/// <inheritdoc/>
		IGetRepositoryResponse GetRepository(IGetRepositoryRequest request);

		/// <inheritdoc/>
		Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector);

		/// <inheritdoc/>
		Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector) =>
			this.GetRepository(selector?.Invoke(new GetRepositoryDescriptor()));

		/// <inheritdoc/>
		public IGetRepositoryResponse GetRepository(IGetRepositoryRequest request) => 
			this.Dispatcher.Dispatch<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotGetRepositoryDispatch<GetRepositoryResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector) => 
			this.GetRepositoryAsync(selector?.Invoke(new GetRepositoryDescriptor()));

		/// <inheritdoc/>
		public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request) => 
			this.Dispatcher.DispatchAsync<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse, IGetRepositoryResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotGetRepositoryDispatchAsync<GetRepositoryResponse>(p)
			);
	}
}