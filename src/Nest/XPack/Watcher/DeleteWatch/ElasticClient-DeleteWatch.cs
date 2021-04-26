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
		/// Removes a watch identified by its id from Watcher. Once removed, the document representing the watch in the .watches index is gone
		/// and it will never be executed again.
		/// </summary>
		/// <remarks>
		/// Deleting a watch does not delete any watch execution records related to this watch from the watch history.
		/// </remarks>
		DeleteWatchResponse DeleteWatch(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null);

		/// <inheritdoc />
		DeleteWatchResponse DeleteWatch(IDeleteWatchRequest request);

		/// <inheritdoc />
		Task<DeleteWatchResponse> DeleteWatchAsync(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteWatchResponse DeleteWatch(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null) =>
			DeleteWatch(selector.InvokeOrDefault(new DeleteWatchDescriptor(watchId)));

		/// <inheritdoc />
		public DeleteWatchResponse DeleteWatch(IDeleteWatchRequest request) =>
			DoRequest<IDeleteWatchRequest, DeleteWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteWatchResponse> DeleteWatchAsync(
			Id watchId,
			Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null,
			CancellationToken ct = default
		) => DeleteWatchAsync(selector.InvokeOrDefault(new DeleteWatchDescriptor(watchId)), ct);

		/// <inheritdoc />
		public Task<DeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteWatchRequest, DeleteWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
