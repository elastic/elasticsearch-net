using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null);

		/// <inheritdoc/>
		IGetRepositoryResponse GetRepository(IGetRepositoryRequest request);

		/// <inheritdoc/>
		Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null) =>
			this.GetRepository(selector.InvokeOrDefault(new GetRepositoryDescriptor()));

		/// <inheritdoc/>
		public IGetRepositoryResponse GetRepository(IGetRepositoryRequest request) =>
			this.Dispatcher.Dispatch<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotGetRepositoryDispatch<GetRepositoryResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetRepositoryAsync(selector.InvokeOrDefault(new GetRepositoryDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse, IGetRepositoryResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.SnapshotGetRepositoryDispatchAsync<GetRepositoryResponse>(p, c)
			);
	}
}
