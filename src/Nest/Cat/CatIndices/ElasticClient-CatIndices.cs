using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null) =>
			this.CatIndices(selector.InvokeOrDefault(new CatIndicesDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request) =>
			this.DoCat<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, this.LowLevelDispatch.CatIndicesDispatch<CatResponse<CatIndicesRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null) =>
			this.CatIndicesAsync(selector.InvokeOrDefault(new CatIndicesDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request) =>
			this.DoCatAsync<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, this.LowLevelDispatch.CatIndicesDispatchAsync<CatResponse<CatIndicesRecord>>);
	}
}