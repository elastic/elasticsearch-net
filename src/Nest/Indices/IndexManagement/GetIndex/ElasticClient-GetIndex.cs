using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexResponseConverter = Func<IApiCallDetails, Stream, GetIndexResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null);

		/// <inheritdoc />
		IGetIndexResponse GetIndex(IGetIndexRequest request);

		/// <inheritdoc />
		Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null) =>
			GetIndex(selector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc />
		public IGetIndexResponse GetIndex(IGetIndexRequest request) =>
			Dispatch2<IGetIndexRequest, GetIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetIndexResponse> GetIndexAsync(
			Indices indices,
			Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken ct = default
		) =>
			GetIndexAsync(selector.InvokeOrDefault(new GetIndexDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetIndexRequest, IGetIndexResponse, GetIndexResponse>(request, request.RequestParameters, ct);
	}
}
