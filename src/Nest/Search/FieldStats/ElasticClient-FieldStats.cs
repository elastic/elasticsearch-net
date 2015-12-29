using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IFieldStatsResponse FieldStats(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector = null);

		/// <inheritdoc/>
		IFieldStatsResponse FieldStats(IFieldStatsRequest request);

		/// <inheritdoc/>
		Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector= null);

		/// <inheritdoc/>
		Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IFieldStatsResponse FieldStats(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector = null) =>
			this.FieldStats(selector.InvokeOrDefault(new FieldStatsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IFieldStatsResponse FieldStats(IFieldStatsRequest request) => 
			this.Dispatcher.Dispatch<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse>(
				request, this.LowLevelDispatch.FieldStatsDispatch<FieldStatsResponse>
			);

		/// <inheritdoc/>
		public Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector = null) => 
			this.FieldStatsAsync(selector.InvokeOrDefault(new FieldStatsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request) 
			=> this.Dispatcher.DispatchAsync<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse, IFieldStatsResponse>(
				request,
				this.LowLevelDispatch.FieldStatsDispatchAsync<FieldStatsResponse>
			);
	}
}