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
		public ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null)
		{
			return this.DoCat<CatRecoveryDescriptor, CatRecoveryRequestParameters, CatRecoveryRecord>(selector, this.RawDispatch.CatRecoveryDispatch<CatResponse<CatRecoveryRecord>>);
		}

		public ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request)
		{
			return this.DoCat<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, this.RawDispatch.CatRecoveryDispatch<CatResponse<CatRecoveryRecord>>);
		}

		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null)
		{
			return this.DoCatAsync<CatRecoveryDescriptor, CatRecoveryRequestParameters, CatRecoveryRecord>(selector, this.RawDispatch.CatRecoveryDispatchAsync<CatResponse<CatRecoveryRecord>>);
		}

		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request)
		{
			return this.DoCatAsync<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, this.RawDispatch.CatRecoveryDispatchAsync<CatResponse<CatRecoveryRecord>>);
		}
	}
}