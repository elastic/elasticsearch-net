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
		/// Deactivates a currently active watch.
		/// </summary>
		DeactivateWatchResponse DeactivateWatch(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null);

		/// <inheritdoc />
		DeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request);

		/// <inheritdoc />
		Task<DeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeactivateWatchResponse DeactivateWatch(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null) =>
			DeactivateWatch(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)));

		/// <inheritdoc />
		public DeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request) =>
			DoRequest<IDeactivateWatchRequest, DeactivateWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeactivateWatchResponse> DeactivateWatchAsync(
			Id id,
			Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null,
			CancellationToken ct = default
		) => DeactivateWatchAsync(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<DeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeactivateWatchRequest, DeactivateWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
