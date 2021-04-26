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
		SimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector);

		/// <inheritdoc />
		SimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request);

		/// <inheritdoc />
		Task<SimulatePipelineResponse> SimulatePipelineAsync(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<SimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public SimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector) =>
			SimulatePipeline(selector?.Invoke(new SimulatePipelineDescriptor()));

		public SimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request) =>
			DoRequest<ISimulatePipelineRequest, SimulatePipelineResponse>(request, request.RequestParameters);

		public Task<SimulatePipelineResponse> SimulatePipelineAsync(
			Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		) => SimulatePipelineAsync(selector?.Invoke(new SimulatePipelineDescriptor()), ct);

		public Task<SimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISimulatePipelineRequest, SimulatePipelineResponse>(request, request.RequestParameters, ct);
	}
}
