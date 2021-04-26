/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
