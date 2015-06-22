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
		public ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null)
		{
			return this.DoCat<CatNodesDescriptor, CatNodesRequestParameters, CatNodesRecord>(selector, this.RawDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);
		}

		public ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request)
		{
			return this.DoCat<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.RawDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);
		}

		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null)
		{
			return this.DoCatAsync<CatNodesDescriptor, CatNodesRequestParameters, CatNodesRecord>(selector, this.RawDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);
		}

		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request)
		{
			return this.DoCatAsync<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.RawDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);
		}
	}
}