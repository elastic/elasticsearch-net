using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IVerifyRepositoryResponse VerifyRepository(Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null);

		/// <inheritdoc />
		IVerifyRepositoryResponse VerifyRepository(IVerifyRepositoryRequest request);

		/// <inheritdoc />
		Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IVerifyRepositoryResponse VerifyRepository(Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null
		) =>
			VerifyRepository(selector.InvokeOrDefault(new VerifyRepositoryDescriptor(repository)));

		/// <inheritdoc />
		public IVerifyRepositoryResponse VerifyRepository(IVerifyRepositoryRequest request) =>
			DoRequest<IVerifyRepositoryRequest, VerifyRepositoryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(
			Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null,
			CancellationToken cancellationToken = default
		) => VerifyRepositoryAsync(selector.InvokeOrDefault(new VerifyRepositoryDescriptor(repository)), cancellationToken);

		/// <inheritdoc />
		public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IVerifyRepositoryRequest, IVerifyRepositoryResponse, VerifyRepositoryResponse>(request, request.RequestParameters, ct);
	}
}
