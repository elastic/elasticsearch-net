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
		/// Starts the Watcher/Alerting service, if the service is not already running
		/// </summary>
		StartWatcherResponse StartWatcher(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null);

		/// <inheritdoc />
		StartWatcherResponse StartWatcher(IStartWatcherRequest request);

		/// <inheritdoc />
		Task<StartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<StartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public StartWatcherResponse StartWatcher(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null) =>
			StartWatcher(selector.InvokeOrDefault(new StartWatcherDescriptor()));

		/// <inheritdoc />
		public StartWatcherResponse StartWatcher(IStartWatcherRequest request) =>
			DoRequest<IStartWatcherRequest, StartWatcherResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<StartWatcherResponse> StartWatcherAsync(
			Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null,
			CancellationToken ct = default
		) => StartWatcherAsync(selector.InvokeOrDefault(new StartWatcherDescriptor()), ct);

		/// <inheritdoc />
		public Task<StartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStartWatcherRequest, StartWatcherResponse>
				(request, request.RequestParameters, ct);
	}
}
