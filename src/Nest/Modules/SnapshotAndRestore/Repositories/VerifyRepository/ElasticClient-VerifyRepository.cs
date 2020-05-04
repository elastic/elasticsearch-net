// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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
