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
		/// Deletes an existing machine learning model snapshot.
		/// </summary>
		/// <remarks>
		/// You cannot delete the active model snapshot, unless you first revert to a different one.
		/// </remarks>
		DeleteModelSnapshotResponse DeleteModelSnapshot(Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		DeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request);

		/// <inheritdoc />
		Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteModelSnapshotResponse DeleteModelSnapshot(
			Id jobId,
			Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		) => DeleteModelSnapshot(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc />
		public DeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request) =>
			DoRequest<IDeleteModelSnapshotRequest, DeleteModelSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(
			Id jobId,
			Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		) => DeleteModelSnapshotAsync(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId, snapshotId)), ct);

		/// <inheritdoc />
		public Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteModelSnapshotRequest, DeleteModelSnapshotResponse>(request, request.RequestParameters, ct);
	}
}
