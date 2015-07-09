using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		
		/// <inheritdoc />
		public IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, GetRepositoryDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<GetRepositoryDescriptor, GetRepositoryRequestParameters, GetRepositoryResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.SnapshotGetRepositoryDispatch<GetRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public IGetRepositoryResponse GetRepository(IGetRepositoryRequest request)
		{
			return this.Dispatcher.Dispatch<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotGetRepositoryDispatch<GetRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, GetRepositoryDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<GetRepositoryDescriptor, GetRepositoryRequestParameters, GetRepositoryResponse, IGetRepositoryResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.SnapshotGetRepositoryDispatchAsync<GetRepositoryResponse>(p)
			);
		}
		
		/// <inheritdoc />
		public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request)
		{
			return this.Dispatcher.DispatchAsync<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse, IGetRepositoryResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotGetRepositoryDispatchAsync<GetRepositoryResponse>(p)
			);
		}
	}
}