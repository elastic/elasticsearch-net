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
		GetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null);

		/// <inheritdoc />
		GetRepositoryResponse GetRepository(IGetRepositoryRequest request);

		/// <inheritdoc />
		Task<GetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null) =>
			GetRepository(selector.InvokeOrDefault(new GetRepositoryDescriptor()));

		/// <inheritdoc />
		public GetRepositoryResponse GetRepository(IGetRepositoryRequest request) =>
			DoRequest<IGetRepositoryRequest, GetRepositoryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetRepositoryResponse> GetRepositoryAsync(
			Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector = null,
			CancellationToken ct = default
		) => GetRepositoryAsync(selector.InvokeOrDefault(new GetRepositoryDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetRepositoryRequest, GetRepositoryResponse>(request, request.RequestParameters, ct);
	}
}
