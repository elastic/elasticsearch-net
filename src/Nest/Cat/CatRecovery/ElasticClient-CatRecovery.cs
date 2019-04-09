using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request,
			CancellationToken ct = default
		);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null) =>
			CatRecovery(selector.InvokeOrDefault(new CatRecoveryDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request) =>
			DoCat<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null,
			CancellationToken ct = default
		) => CatRecoveryAsync(selector.InvokeOrDefault(new CatRecoveryDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, ct);
	}
}
