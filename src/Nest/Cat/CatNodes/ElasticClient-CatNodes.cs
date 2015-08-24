using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null) =>
			this.DoCat<CatNodesDescriptor, CatNodesRequestParameters, CatNodesRecord>(selector, this.LowLevelDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);

		public ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request)=>
			this.DoCat<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.LowLevelDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);

		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null) =>
			this.DoCatAsync<CatNodesDescriptor, CatNodesRequestParameters, CatNodesRecord>(selector, this.LowLevelDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);

		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request) =>
			this.DoCatAsync<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.LowLevelDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);
	}
}