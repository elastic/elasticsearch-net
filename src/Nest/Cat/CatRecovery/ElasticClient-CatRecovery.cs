using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null) =>
			this.CatRecovery(selector.InvokeOrDefault(new CatRecoveryDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request) =>
			this.DoCat<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, this.LowLevelDispatch.CatRecoveryDispatch<CatResponse<CatRecoveryRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatRecoveryAsync(selector.InvokeOrDefault(new CatRecoveryDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, cancellationToken, this.LowLevelDispatch.CatRecoveryDispatchAsync<CatResponse<CatRecoveryRecord>>);
	}
}
