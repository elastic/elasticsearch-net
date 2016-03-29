using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null) =>
			this.CatAllocation(selector.InvokeOrDefault(new CatAllocationDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request) =>
			this.DoCat<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request, this.LowLevelDispatch.CatAllocationDispatch<CatResponse<CatAllocationRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null) =>
			this.CatAllocationAsync(selector.InvokeOrDefault(new CatAllocationDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request) =>
			this.DoCatAsync<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request, this.LowLevelDispatch.CatAllocationDispatchAsync<CatResponse<CatAllocationRecord>>);
	}
}