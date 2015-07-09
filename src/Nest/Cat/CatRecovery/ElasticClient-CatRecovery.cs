using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null) =>
			this.DoCat<CatRecoveryDescriptor, CatRecoveryRequestParameters, CatRecoveryRecord>(selector, this.LowLevelDispatch.CatRecoveryDispatch<CatResponse<CatRecoveryRecord>>);

		public ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request) =>
			this.DoCat<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, this.LowLevelDispatch.CatRecoveryDispatch<CatResponse<CatRecoveryRecord>>);

		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null) =>
			this.DoCatAsync<CatRecoveryDescriptor, CatRecoveryRequestParameters, CatRecoveryRecord>(selector, this.LowLevelDispatch.CatRecoveryDispatchAsync<CatResponse<CatRecoveryRecord>>);

		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request) =>
			this.DoCatAsync<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, this.LowLevelDispatch.CatRecoveryDispatchAsync<CatResponse<CatRecoveryRecord>>);
	}
}