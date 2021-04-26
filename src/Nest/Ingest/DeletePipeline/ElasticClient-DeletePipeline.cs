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
		DeletePipelineResponse DeletePipeline(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null);

		/// <inheritdoc />
		DeletePipelineResponse DeletePipeline(IDeletePipelineRequest request);

		/// <inheritdoc />
		Task<DeletePipelineResponse> DeletePipelineAsync(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<DeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeletePipelineResponse DeletePipeline(IDeletePipelineRequest request) =>
			DoRequest<IDeletePipelineRequest, DeletePipelineResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public DeletePipelineResponse DeletePipeline(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null) =>
			DeletePipeline(selector.InvokeOrDefault(new DeletePipelineDescriptor(id)));

		/// <inheritdoc />
		public Task<DeletePipelineResponse> DeletePipelineAsync(
			Id id,
			Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken cancellationToken = default
		) => DeletePipelineAsync(selector.InvokeOrDefault(new DeletePipelineDescriptor(id)), cancellationToken);

		/// <inheritdoc />
		public Task<DeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeletePipelineRequest, DeletePipelineResponse>(request, request.RequestParameters, ct);
	}
}
