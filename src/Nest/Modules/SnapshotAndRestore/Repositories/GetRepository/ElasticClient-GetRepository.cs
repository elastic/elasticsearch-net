using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null);

		/// <inheritdoc />
		IGetRepositoryResponse GetRepository(IGetRepositoryRequest request);

		/// <inheritdoc />
		Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null) =>
			GetRepository(selector.InvokeOrDefault(new GetRepositoryDescriptor()));

		/// <inheritdoc />
		public IGetRepositoryResponse GetRepository(IGetRepositoryRequest request) =>
			Dispatcher.Dispatch<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse>(
				request,
				(p, d) => LowLevelDispatch.SnapshotGetRepositoryDispatch<GetRepositoryResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetRepositoryAsync(selector.InvokeOrDefault(new GetRepositoryDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse, IGetRepositoryResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SnapshotGetRepositoryDispatchAsync<GetRepositoryResponse>(p, c)
			);
	}
}
