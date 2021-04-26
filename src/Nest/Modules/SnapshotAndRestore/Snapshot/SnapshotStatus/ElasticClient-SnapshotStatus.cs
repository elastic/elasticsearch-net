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
		/// <inheritdoc />
		SnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null);

		/// <inheritdoc />
		SnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request);

		/// <inheritdoc />
		Task<SnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<SnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public SnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null) =>
			SnapshotStatus(selector.InvokeOrDefault(new SnapshotStatusDescriptor()));

		/// <inheritdoc />
		public SnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request) =>
			DoRequest<ISnapshotStatusRequest, SnapshotStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<SnapshotStatusResponse> SnapshotStatusAsync(
			Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken ct = default
		) => SnapshotStatusAsync(selector.InvokeOrDefault(new SnapshotStatusDescriptor()), ct);

		/// <inheritdoc />
		public Task<SnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISnapshotStatusRequest, SnapshotStatusResponse>(request, request.RequestParameters, ct);
	}
}
