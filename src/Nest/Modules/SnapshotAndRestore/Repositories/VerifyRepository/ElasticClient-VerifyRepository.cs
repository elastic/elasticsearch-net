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
		VerifyRepositoryResponse VerifyRepository(Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null);

		/// <inheritdoc />
		VerifyRepositoryResponse VerifyRepository(IVerifyRepositoryRequest request);

		/// <inheritdoc />
		Task<VerifyRepositoryResponse> VerifyRepositoryAsync(Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<VerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public VerifyRepositoryResponse VerifyRepository(Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null
		) =>
			VerifyRepository(selector.InvokeOrDefault(new VerifyRepositoryDescriptor(repository)));

		/// <inheritdoc />
		public VerifyRepositoryResponse VerifyRepository(IVerifyRepositoryRequest request) =>
			DoRequest<IVerifyRepositoryRequest, VerifyRepositoryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<VerifyRepositoryResponse> VerifyRepositoryAsync(
			Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null,
			CancellationToken cancellationToken = default
		) => VerifyRepositoryAsync(selector.InvokeOrDefault(new VerifyRepositoryDescriptor(repository)), cancellationToken);

		/// <inheritdoc />
		public Task<VerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IVerifyRepositoryRequest, VerifyRepositoryResponse>(request, request.RequestParameters, ct);
	}
}
