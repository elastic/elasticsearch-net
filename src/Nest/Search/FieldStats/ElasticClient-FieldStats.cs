using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Find statistical properties of a field without executing a search,
		/// but looking up measurements that are natively available in the Lucene index.
		/// </summary>
		[Obsolete("Scheduled to be removed in 6.0. Use FieldCapabilities instead")]
		IFieldStatsResponse FieldStats(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector = null);

		/// <summary>
		/// Find statistical properties of a field without executing a search,
		/// but looking up measurements that are natively available in the Lucene index.
		/// </summary>
		[Obsolete("Scheduled to be removed in 6.0. Use FieldCapabilities instead")]
		IFieldStatsResponse FieldStats(IFieldStatsRequest request);

		/// <summary>
		/// Find statistical properties of a field without executing a search,
		/// but looking up measurements that are natively available in the Lucene index.
		/// </summary>
		[Obsolete("Scheduled to be removed in 6.0. Use FieldCapabilities instead")]
		Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector= null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Find statistical properties of a field without executing a search,
		/// but looking up measurements that are natively available in the Lucene index.
		/// </summary>
		[Obsolete("Scheduled to be removed in 6.0. Use FieldCapabilities instead")]
		Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		[Obsolete("Scheduled to be removed in 6.0. Use FieldCapabilities instead")]
		public IFieldStatsResponse FieldStats(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector = null) =>
			this.FieldStats(selector.InvokeOrDefault(new FieldStatsDescriptor().Index(indices)));

		/// <inheritdoc/>
		[Obsolete("Scheduled to be removed in 6.0. Use FieldCapabilities instead")]
		public IFieldStatsResponse FieldStats(IFieldStatsRequest request) =>
			this.Dispatcher.Dispatch<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse>(
				request, this.LowLevelDispatch.FieldStatsDispatch<FieldStatsResponse>
			);

		/// <inheritdoc/>
		[Obsolete("Scheduled to be removed in 6.0. Use FieldCapabilities instead")]
		public Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.FieldStatsAsync(selector.InvokeOrDefault(new FieldStatsDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc/>
		[Obsolete("Scheduled to be removed in 6.0. Use FieldCapabilities instead")]
		public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request, CancellationToken cancellationToken = default(CancellationToken))
			=> this.Dispatcher.DispatchAsync<IFieldStatsRequest, FieldStatsRequestParameters, FieldStatsResponse, IFieldStatsResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.FieldStatsDispatchAsync<FieldStatsResponse>
			);
	}
}
