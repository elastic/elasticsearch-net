using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IFieldStatsResponse FieldStats(Func<FieldStatsDescriptor, IFieldStatsRequest> selector);

		/// <inheritdoc/>
		IFieldStatsResponse FieldStats(IFieldStatsRequest request);

		/// <inheritdoc/>
		Task<IFieldStatsResponse> FieldStatsAsync(Func<FieldStatsDescriptor, IFieldStatsRequest> selector);

		/// <inheritdoc/>
		Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IFieldStatsResponse FieldStats(Func<FieldStatsDescriptor, IFieldStatsRequest> selector) => 
			this.Dispatcher.Dispatch<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse>(
				selector?.Invoke(new FieldStatsDescriptor()),
				this.LowLevelDispatch.FieldStatsDispatch<FieldStatsResponse>
			);

		/// <inheritdoc/>
		public IFieldStatsResponse FieldStats(IFieldStatsRequest request) => 
			this.Dispatcher.Dispatch<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse>(
				request, this.LowLevelDispatch.FieldStatsDispatch<FieldStatsResponse>
			);

		/// <inheritdoc/>
		public Task<IFieldStatsResponse> FieldStatsAsync(Func<FieldStatsDescriptor, IFieldStatsRequest> selector) => 
			this.Dispatcher.DispatchAsync<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse, IFieldStatsResponse>(
				selector?.Invoke(new FieldStatsDescriptor()),
				this.LowLevelDispatch.FieldStatsDispatchAsync<FieldStatsResponse>
			);

		/// <inheritdoc/>
		public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request) 
			=> this.Dispatcher.DispatchAsync<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse, IFieldStatsResponse>(
				request,
				this.LowLevelDispatch.FieldStatsDispatchAsync<FieldStatsResponse>
			);
	}
}