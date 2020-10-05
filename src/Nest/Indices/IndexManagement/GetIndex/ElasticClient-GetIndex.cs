// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	using GetIndexResponseConverter = Func<IApiCallDetails, Stream, GetIndexResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null);

		/// <inheritdoc />
		GetIndexResponse GetIndex(IGetIndexRequest request);

		/// <inheritdoc />
		Task<GetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null) =>
			GetIndex(selector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc />
		public GetIndexResponse GetIndex(IGetIndexRequest request) =>
			DoRequest<IGetIndexRequest, GetIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetIndexResponse> GetIndexAsync(
			Indices indices,
			Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken ct = default
		) =>
			GetIndexAsync(selector.InvokeOrDefault(new GetIndexDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<GetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetIndexRequest, GetIndexResponse>(request, request.RequestParameters, ct);
	}
}
