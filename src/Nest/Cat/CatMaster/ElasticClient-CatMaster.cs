using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, ICatMasterRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, ICatMasterRequest> selector = null);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, ICatMasterRequest> selector = null) =>
			this.CatMaster(selector.InvokeOrDefault(new CatMasterDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request) =>
			this.DoCat<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request, this.LowLevelDispatch.CatMasterDispatch<CatResponse<CatMasterRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, ICatMasterRequest> selector = null) =>
			this.CatMasterAsync(selector.InvokeOrDefault(new CatMasterDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request) =>
			this.DoCatAsync<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request, this.LowLevelDispatch.CatMasterDispatchAsync<CatResponse<CatMasterRecord>>);
	}
}
		