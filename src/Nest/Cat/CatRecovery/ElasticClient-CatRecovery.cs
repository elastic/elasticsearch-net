using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatRecoveryRecord>> CatRecoveryAsync(
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<CatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request,
			CancellationToken ct = default
		);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null) =>
			CatRecovery(selector.InvokeOrDefault(new CatRecoveryDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request) =>
			DoCat<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatRecoveryRecord>> CatRecoveryAsync(
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null,
			CancellationToken ct = default
		) => CatRecoveryAsync(selector.InvokeOrDefault(new CatRecoveryDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, ct);
	}
}
