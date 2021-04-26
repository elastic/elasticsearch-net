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
		/// Stops the Watcher/Alerting service, if the service is running
		/// </summary>
		StopWatcherResponse StopWatcher(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null);

		/// <inheritdoc />
		StopWatcherResponse StopWatcher(IStopWatcherRequest request);

		/// <inheritdoc />
		Task<StopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<StopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public StopWatcherResponse StopWatcher(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null) =>
			StopWatcher(selector.InvokeOrDefault(new StopWatcherDescriptor()));

		/// <inheritdoc />
		public StopWatcherResponse StopWatcher(IStopWatcherRequest request) =>
			DoRequest<IStopWatcherRequest, StopWatcherResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<StopWatcherResponse> StopWatcherAsync(
			Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		) => StopWatcherAsync(selector.InvokeOrDefault(new StopWatcherDescriptor()), ct);

		/// <inheritdoc />
		public Task<StopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStopWatcherRequest, StopWatcherResponse>
				(request, request.RequestParameters, ct);
	}
}
