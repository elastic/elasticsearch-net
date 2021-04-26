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
		ShrinkIndexResponse ShrinkIndex(IndexName source, IndexName target, Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null);

		ShrinkIndexResponse ShrinkIndex(IShrinkIndexRequest request);

		Task<ShrinkIndexResponse> ShrinkIndexAsync(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		Task<ShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		public ShrinkIndexResponse ShrinkIndex(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null
		) => ShrinkIndex(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public ShrinkIndexResponse ShrinkIndex(IShrinkIndexRequest request) =>
			DoRequest<IShrinkIndexRequest, ShrinkIndexResponse>(request, request.RequestParameters);

		public Task<ShrinkIndexResponse> ShrinkIndexAsync(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		) => ShrinkIndexAsync(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public Task<ShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IShrinkIndexRequest, ShrinkIndexResponse>(request, request.RequestParameters, ct);
	}
}
