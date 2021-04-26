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
		/// Forces the execution of a stored watch. It can be used to force execution of the watch outside of its triggering logic,
		/// or to simulate the watch execution for debugging purposes.
		/// </summary>
		ExecuteWatchResponse ExecuteWatch(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector);

		/// <inheritdoc />
		ExecuteWatchResponse ExecuteWatch(IExecuteWatchRequest request);

		/// <inheritdoc />
		Task<ExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ExecuteWatchResponse ExecuteWatch(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector) =>
			ExecuteWatch(selector.InvokeOrDefault(new ExecuteWatchDescriptor()));

		/// <inheritdoc />
		public ExecuteWatchResponse ExecuteWatch(IExecuteWatchRequest request) =>
			DoRequest<IExecuteWatchRequest, ExecuteWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ExecuteWatchResponse> ExecuteWatchAsync(
			Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector,
			CancellationToken ct = default
		) => ExecuteWatchAsync(selector?.InvokeOrDefault(new ExecuteWatchDescriptor()), ct);

		/// <inheritdoc />
		public Task<ExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IExecuteWatchRequest, ExecuteWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
