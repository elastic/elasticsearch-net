using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatSegmentsRecord> CatSegments(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatSegmentsRecord> CatSegments(ICatSegmentsRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatSegmentsRecord> CatSegments(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null) =>
			this.CatSegments(selector.InvokeOrDefault(new CatSegmentsDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatSegmentsRecord> CatSegments(ICatSegmentsRequest request) =>
			this.DoCat<ICatSegmentsRequest, CatSegmentsRequestParameters, CatSegmentsRecord>(request, this.LowLevelDispatch.CatSegmentsDispatch<CatResponse<CatSegmentsRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null) =>
			this.CatSegmentsAsync(selector.InvokeOrDefault(new CatSegmentsDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request) =>
			this.DoCatAsync<ICatSegmentsRequest, CatSegmentsRequestParameters, CatSegmentsRecord>(request, this.LowLevelDispatch.CatSegmentsDispatchAsync<CatResponse<CatSegmentsRecord>>);

	}
}