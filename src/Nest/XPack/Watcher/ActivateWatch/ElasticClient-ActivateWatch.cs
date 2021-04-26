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
		/// Activates a currently inactive watch.
		/// </summary>
		ActivateWatchResponse ActivateWatch(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null);

		/// <inheritdoc />
		ActivateWatchResponse ActivateWatch(IActivateWatchRequest request);

		/// <inheritdoc />
		Task<ActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ActivateWatchResponse ActivateWatch(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null) =>
			ActivateWatch(selector.InvokeOrDefault(new ActivateWatchDescriptor(id)));

		/// <inheritdoc />
		public ActivateWatchResponse ActivateWatch(IActivateWatchRequest request) =>
			DoRequest<IActivateWatchRequest, ActivateWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ActivateWatchResponse> ActivateWatchAsync(
			Id id,
			Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		) => ActivateWatchAsync(selector.InvokeOrDefault(new ActivateWatchDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<ActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IActivateWatchRequest, ActivateWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
