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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null) =>
			GetIndex(selector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc />
		public IGetIndexResponse GetIndex(IGetIndexRequest request) =>
			Dispatcher.Dispatch<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesGetDispatch<GetIndexResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetIndexAsync(selector.InvokeOrDefault(new GetIndexDescriptor(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse, IGetIndexResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesGetDispatchAsync<GetIndexResponse>(p, c)
			);
	}
}
