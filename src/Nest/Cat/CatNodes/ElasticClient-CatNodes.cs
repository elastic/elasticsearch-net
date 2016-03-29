using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null) =>
			this.CatNodes(selector.InvokeOrDefault(new CatNodesDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request)=>
			this.DoCat<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.LowLevelDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null) =>
			this.CatNodesAsync(selector.InvokeOrDefault(new CatNodesDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request) =>
			this.DoCatAsync<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.LowLevelDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);
	}
}