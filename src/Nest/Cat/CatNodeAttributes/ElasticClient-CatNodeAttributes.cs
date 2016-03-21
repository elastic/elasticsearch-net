using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(ICatNodeAttributesRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null) =>
			this.CatNodeAttributes(selector.InvokeOrDefault(new CatNodeAttributesDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(ICatNodeAttributesRequest request) =>
			this.DoCat<ICatNodeAttributesRequest, CatNodeAttributesRequestParameters, CatNodeAttributesRecord>(request, this.LowLevelDispatch.CatNodeattrsDispatch<CatResponse<CatNodeAttributesRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null) =>
			this.CatNodeAttributesAsync(selector.InvokeOrDefault(new CatNodeAttributesDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request) =>
			this.DoCatAsync<ICatNodeAttributesRequest, CatNodeAttributesRequestParameters, CatNodeAttributesRecord>(request, this.LowLevelDispatch.CatNodeattrsDispatchAsync<CatResponse<CatNodeAttributesRecord>>);

	}
}
