using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request);
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
		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null) =>
			this.CatRecoveryAsync(selector.InvokeOrDefault(new CatRecoveryDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request) =>
			this.DoCatAsync<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, this.LowLevelDispatch.CatRecoveryDispatchAsync<CatResponse<CatRecoveryRecord>>);
	}
}