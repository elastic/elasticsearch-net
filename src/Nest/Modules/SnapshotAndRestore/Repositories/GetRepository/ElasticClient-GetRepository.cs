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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null) =>
			GetRepository(selector.InvokeOrDefault(new GetRepositoryDescriptor()));

		/// <inheritdoc />
		public IGetRepositoryResponse GetRepository(IGetRepositoryRequest request) =>
			Dispatch2<IGetRepositoryRequest, GetRepositoryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetRepositoryResponse> GetRepositoryAsync(
			Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken ct = default
		) => GetRepositoryAsync(selector.InvokeOrDefault(new GetRepositoryDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetRepositoryRequest, IGetRepositoryResponse, GetRepositoryResponse>(request, request.RequestParameters, ct);
	}
}
