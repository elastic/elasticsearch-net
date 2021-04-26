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
		/// Before any snapshot or restore operation can be performed a snapshot repository should be registered in Elasticsearch.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_repositories
		/// </summary>
		/// <param name="repository">The name for the repository</param>
		/// <param name="selector">describe what the repository looks like</param>
		CreateRepositoryResponse CreateRepository(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector);

		/// <inheritdoc />
		CreateRepositoryResponse CreateRepository(ICreateRepositoryRequest request);

		/// <inheritdoc />
		Task<CreateRepositoryResponse> CreateRepositoryAsync(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<CreateRepositoryResponse> CreateRepositoryAsync(ICreateRepositoryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CreateRepositoryResponse CreateRepository(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector) =>
			CreateRepository(selector?.Invoke(new CreateRepositoryDescriptor(repository)));

		/// <inheritdoc />
		public CreateRepositoryResponse CreateRepository(ICreateRepositoryRequest request) =>
			DoRequest<ICreateRepositoryRequest, CreateRepositoryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<CreateRepositoryResponse> CreateRepositoryAsync(
			Name repository,
			Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector,
			CancellationToken ct = default
		) => CreateRepositoryAsync(selector?.Invoke(new CreateRepositoryDescriptor(repository)), ct);

		/// <inheritdoc />
		public Task<CreateRepositoryResponse> CreateRepositoryAsync(ICreateRepositoryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ICreateRepositoryRequest, CreateRepositoryResponse>(request, request.RequestParameters, ct);
	}
}
