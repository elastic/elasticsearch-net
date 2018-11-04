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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
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
			Dispatcher.Dispatch<IVerifyRepositoryRequest, VerifyRepositoryRequestParameters, VerifyRepositoryResponse>(
				request,
				(p, d) => LowLevelDispatch.SnapshotVerifyRepositoryDispatch<VerifyRepositoryResponse>(p)
			);

		/// <inheritdoc />
		public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			VerifyRepositoryAsync(selector.InvokeOrDefault(new VerifyRepositoryDescriptor(repository)), cancellationToken);

		/// <inheritdoc />
		public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IVerifyRepositoryRequest, VerifyRepositoryRequestParameters, VerifyRepositoryResponse, IVerifyRepositoryResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.SnapshotVerifyRepositoryDispatchAsync<VerifyRepositoryResponse>(p, c)
				);
	}
}
