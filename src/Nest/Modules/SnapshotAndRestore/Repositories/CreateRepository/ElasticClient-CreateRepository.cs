// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
