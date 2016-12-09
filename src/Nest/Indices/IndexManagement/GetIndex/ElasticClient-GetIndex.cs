using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;
	using GetIndexResponseConverter = Func<IApiCallDetails, Stream, GetIndexResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null);

		/// <inheritdoc/>
		IGetIndexResponse GetIndex(IGetIndexRequest request);

		/// <inheritdoc/>
		Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null) =>
			this.GetIndex(selector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc/>
		public IGetIndexResponse GetIndex(IGetIndexRequest request) =>
			this.Dispatcher.Dispatch<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesGetDispatch<GetIndexResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetIndexAsync(selector.InvokeOrDefault(new GetIndexDescriptor(indices)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse, IGetIndexResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IndicesGetDispatchAsync<GetIndexResponse>(p, c)
			);

	}
}
