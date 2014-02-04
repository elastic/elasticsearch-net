using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{

	public partial class ElasticClient
	{
		public IGlobalStatsResponse IndicesStats(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<IndicesStatsDescriptor, IndicesStatsQueryString, GlobalStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatsDispatch(p)
			);
		}

		public Task<IGlobalStatsResponse> IndicesStatsAsync(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<IndicesStatsDescriptor, IndicesStatsQueryString, GlobalStatsResponse, IGlobalStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatsDispatchAsync(p)
			);
		}
	}
}
