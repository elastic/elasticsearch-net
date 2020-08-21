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
		/// Retrieves a watch by its id
		/// </summary>
		GetWatchResponse GetWatch(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null);

		/// <inheritdoc />
		GetWatchResponse GetWatch(IGetWatchRequest request);

		/// <inheritdoc />
		Task<GetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetWatchResponse> GetWatchAsync(IGetWatchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetWatchResponse GetWatch(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null) =>
			GetWatch(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)));

		/// <inheritdoc />
		public GetWatchResponse GetWatch(IGetWatchRequest request) =>
			DoRequest<IGetWatchRequest, GetWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetWatchResponse> GetWatchAsync(
			Id watchId,
			Func<GetWatchDescriptor, IGetWatchRequest> selector = null,
			CancellationToken ct = default
		) => GetWatchAsync(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)), ct);

		/// <inheritdoc />
		public Task<GetWatchResponse> GetWatchAsync(IGetWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetWatchRequest, GetWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
