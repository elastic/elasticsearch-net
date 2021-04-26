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
		PutPipelineResponse PutPipeline(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector);

		/// <inheritdoc />
		PutPipelineResponse PutPipeline(IPutPipelineRequest request);

		/// <inheritdoc />
		Task<PutPipelineResponse> PutPipelineAsync(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<PutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		public PutPipelineResponse PutPipeline(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector) =>
			PutPipeline(selector?.Invoke(new PutPipelineDescriptor(id)));

		public PutPipelineResponse PutPipeline(IPutPipelineRequest request) =>
			DoRequest<IPutPipelineRequest, PutPipelineResponse>(request, request.RequestParameters);

		public Task<PutPipelineResponse> PutPipelineAsync(
			Id id,
			Func<PutPipelineDescriptor, IPutPipelineRequest> selector,
			CancellationToken cancellationToken = default
		) => PutPipelineAsync(selector?.Invoke(new PutPipelineDescriptor(id)), cancellationToken);

		public Task<PutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutPipelineRequest, PutPipelineResponse>(request, request.RequestParameters, ct);
	}
}
