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
		/// Registers a new watch in Watcher or updates an existing one.
		/// Once registered, a new document will be added to the .watches index, representing the watch,
		/// and its trigger will immediately be registered with the relevant trigger engine.
		/// </summary>
		PutWatchResponse PutWatch(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		PutWatchResponse PutWatch(IPutWatchRequest request);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		Task<PutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		Task<PutWatchResponse> PutWatchAsync(IPutWatchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutWatchResponse PutWatch(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null) =>
			PutWatch(selector.InvokeOrDefault(new PutWatchDescriptor(watchId)));

		/// <inheritdoc />
		public PutWatchResponse PutWatch(IPutWatchRequest request) =>
			DoRequest<IPutWatchRequest, PutWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PutWatchResponse> PutWatchAsync(
			Id watchId,
			Func<PutWatchDescriptor, IPutWatchRequest> selector = null,
			CancellationToken ct = default
		) => PutWatchAsync(selector.InvokeOrDefault(new PutWatchDescriptor(watchId)), ct);

		/// <inheritdoc />
		public Task<PutWatchResponse> PutWatchAsync(IPutWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutWatchRequest, PutWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
