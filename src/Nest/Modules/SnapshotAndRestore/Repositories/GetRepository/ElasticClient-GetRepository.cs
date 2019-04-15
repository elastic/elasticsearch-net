using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null);

		/// <inheritdoc />
		GetRepositoryResponse GetRepository(IGetRepositoryRequest request);

		/// <inheritdoc />
		Task<GetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null) =>
			GetRepository(selector.InvokeOrDefault(new GetRepositoryDescriptor()));

		/// <inheritdoc />
		public GetRepositoryResponse GetRepository(IGetRepositoryRequest request) =>
			DoRequest<IGetRepositoryRequest, GetRepositoryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetRepositoryResponse> GetRepositoryAsync(
			Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken ct = default
		) => GetRepositoryAsync(selector.InvokeOrDefault(new GetRepositoryDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetRepositoryRequest, GetRepositoryResponse>(request, request.RequestParameters, ct);
	}
}
