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
		public ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, CatAllocationDescriptor> selector = null) =>
			this.DoCat<CatAllocationDescriptor, CatAllocationRequestParameters, CatAllocationRecord>(selector, this.RawDispatch.CatAllocationDispatch<CatResponse<CatAllocationRecord>>);

		public ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request) =>
			this.DoCat<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request, this.RawDispatch.CatAllocationDispatch<CatResponse<CatAllocationRecord>>);

		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, CatAllocationDescriptor> selector = null) =>
			this.DoCatAsync<CatAllocationDescriptor, CatAllocationRequestParameters, CatAllocationRecord>(selector, this.RawDispatch.CatAllocationDispatchAsync<CatResponse<CatAllocationRecord>>);

		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request) =>
			this.DoCatAsync<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request, this.RawDispatch.CatAllocationDispatchAsync<CatResponse<CatAllocationRecord>>);
	}
}