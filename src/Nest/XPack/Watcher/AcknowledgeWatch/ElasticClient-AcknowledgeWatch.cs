using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Acknowledges a watch, to manually throttle execution of the watch's actions
		/// while the watch condition remains <c>true</c>.
		/// An acknowledged watch action remains in the acknowledged (acked) state until the watch’s condition
		/// evaluates to <c>false</c>.
		/// </summary>
		IAcknowledgeWatchResponse AcknowledgeWatch(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null);

		/// <inheritdoc/>
		IAcknowledgeWatchResponse AcknowledgeWatch(IAcknowledgeWatchRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(IAcknowledgeWatchRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgeWatchResponse AcknowledgeWatch(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null) =>
			this.AcknowledgeWatch(selector.InvokeOrDefault(new AcknowledgeWatchDescriptor(id)));

		/// <inheritdoc/>
		public IAcknowledgeWatchResponse AcknowledgeWatch(IAcknowledgeWatchRequest request) =>
			this.Dispatcher.Dispatch<IAcknowledgeWatchRequest, AcknowledgeWatchRequestParameters, AcknowledgeWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherAckWatchDispatch<AcknowledgeWatchResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null) =>
			this.AcknowledgeWatchAsync(selector.InvokeOrDefault(new AcknowledgeWatchDescriptor(id)));

		/// <inheritdoc/>
		public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(IAcknowledgeWatchRequest request) =>
			this.Dispatcher.DispatchAsync<IAcknowledgeWatchRequest, AcknowledgeWatchRequestParameters, AcknowledgeWatchResponse, IAcknowledgeWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherAckWatchDispatchAsync<AcknowledgeWatchResponse>(p)
			);
	}
}
