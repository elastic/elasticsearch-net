using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGlobalStatsResponse IndicesStats(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<IndicesStatsDescriptor, IndicesStatsRequestParameters, GlobalStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatsDispatch<GlobalStatsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IGlobalStatsResponse> IndicesStatsAsync(
			Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<IndicesStatsDescriptor, IndicesStatsRequestParameters, GlobalStatsResponse, IGlobalStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatsDispatchAsync<GlobalStatsResponse>(p)
			);
		}
	}
}