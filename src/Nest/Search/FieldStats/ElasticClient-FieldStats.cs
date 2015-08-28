using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		public IFieldStatsResponse FieldStats(Func<FieldStatsDescriptor, FieldStatsDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<FieldStatsDescriptor, FieldStatsRequestParameters, FieldStatsResponse>(
				selector, this.LowLevelDispatch.FieldStatsDispatch<FieldStatsResponse>
			);
		}

		public IFieldStatsResponse FieldStats(IFieldStatsRequest request)
		{
			return this.Dispatcher.Dispatch<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse>(
				request, this.LowLevelDispatch.FieldStatsDispatch<FieldStatsResponse>
			);
		}

		public Task<IFieldStatsResponse> FieldStatsAsync(Func<FieldStatsDescriptor, FieldStatsDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<FieldStatsDescriptor, FieldStatsRequestParameters, FieldStatsResponse, IFieldStatsResponse>(
				selector, this.LowLevelDispatch.FieldStatsDispatchAsync<FieldStatsResponse>
			);
		}

		public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request)
		{
			return this.Dispatcher.DispatchAsync<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse, IFieldStatsResponse>(
				request, this.LowLevelDispatch.FieldStatsDispatchAsync<FieldStatsResponse>
			);
		}
	}
}