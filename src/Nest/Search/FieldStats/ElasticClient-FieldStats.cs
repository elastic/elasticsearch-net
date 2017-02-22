using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest_5_2_0
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IFieldStatsResponse FieldStats(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector = null);

		/// <inheritdoc/>
		IFieldStatsResponse FieldStats(IFieldStatsRequest request);

		/// <inheritdoc/>
		Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector= null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
		public Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.FieldStatsAsync(selector.InvokeOrDefault(new FieldStatsDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc/>
		public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request, CancellationToken cancellationToken = default(CancellationToken))
			=> this.Dispatcher.DispatchAsync<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse, IFieldStatsResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.FieldStatsDispatchAsync<FieldStatsResponse>
			);
	}
}
