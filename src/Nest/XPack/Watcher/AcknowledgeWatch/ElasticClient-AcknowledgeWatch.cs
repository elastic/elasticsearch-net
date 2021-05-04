// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;

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
		AcknowledgeWatchResponse AcknowledgeWatch(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null);

		/// <inheritdoc />
		AcknowledgeWatchResponse AcknowledgeWatch(IAcknowledgeWatchRequest request);

		/// <inheritdoc />
		Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(IAcknowledgeWatchRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public AcknowledgeWatchResponse AcknowledgeWatch(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null) =>
			AcknowledgeWatch(selector.InvokeOrDefault(new AcknowledgeWatchDescriptor(id)));

		/// <inheritdoc />
		public AcknowledgeWatchResponse AcknowledgeWatch(IAcknowledgeWatchRequest request) =>
			DoRequest<IAcknowledgeWatchRequest, AcknowledgeWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(
			Id id,
			Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			AcknowledgeWatchAsync(selector.InvokeOrDefault(new AcknowledgeWatchDescriptor(id)), cancellationToken);

		/// <inheritdoc />
		public Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(IAcknowledgeWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IAcknowledgeWatchRequest, AcknowledgeWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
