using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Get the data when you hit the elasticsearch endpoint at the root
		/// </summary>
		/// <returns></returns>
		public IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? ((i) => i);
			return this.Dispatch<InfoDescriptor, InfoQueryString, RootInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.InfoDispatch(p)
			);
		}

		/// <summary>
		/// Get the data when you hit the elasticsearch endpoint at the root
		/// </summary>
		/// <returns></returns>
		public Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? ((i) => i);
			return this.DispatchAsync<InfoDescriptor, InfoQueryString, RootInfoResponse, IRootInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.InfoDispatchAsync(p)
			);
		}
	}
}
