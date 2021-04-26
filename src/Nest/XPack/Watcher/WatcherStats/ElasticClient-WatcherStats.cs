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
		/// Gets the current Watcher metrics
		/// </summary>
		WatcherStatsResponse WatcherStats(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null);

		/// <inheritdoc />
		WatcherStatsResponse WatcherStats(IWatcherStatsRequest request);

		/// <inheritdoc />
		Task<WatcherStatsResponse> WatcherStatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<WatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public WatcherStatsResponse WatcherStats(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null) =>
			WatcherStats(selector.InvokeOrDefault(new WatcherStatsDescriptor()));

		/// <inheritdoc />
		public WatcherStatsResponse WatcherStats(IWatcherStatsRequest request) =>
			DoRequest<IWatcherStatsRequest, WatcherStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<WatcherStatsResponse> WatcherStatsAsync(
			Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null,
			CancellationToken ct = default
		) => WatcherStatsAsync(selector.InvokeOrDefault(new WatcherStatsDescriptor()), ct);

		/// <inheritdoc />
		public Task<WatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IWatcherStatsRequest, WatcherStatsResponse>
				(request, request.RequestParameters, ct);
	}
}
