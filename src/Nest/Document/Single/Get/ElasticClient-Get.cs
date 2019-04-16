using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Use the /{index}/{type}/{id} to get the document and its metadata
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">A descriptor that describes which document's source to fetch</param>
		IGetResponse<TDocument> Get<TDocument>(DocumentPath<TDocument> document, Func<GetDescriptor<TDocument>, IGetRequest> selector = null) where TDocument : class;

		/// <inheritdoc />
		IGetResponse<TDocument> Get<TDocument>(IGetRequest request) where TDocument : class;

		/// <inheritdoc />
		Task<IGetResponse<TDocument>> GetAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<GetDescriptor<TDocument>, IGetRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class;

		/// <inheritdoc />
		Task<IGetResponse<TDocument>> GetAsync<TDocument>(IGetRequest request, CancellationToken cancellationToken = default(CancellationToken)) where TDocument : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetResponse<TDocument> Get<TDocument>(DocumentPath<TDocument> document, Func<GetDescriptor<TDocument>, IGetRequest> selector = null) where TDocument : class =>
			Get<TDocument>(selector.InvokeOrDefault(new GetDescriptor<TDocument>(document.Document, document.Self.Index, document.Self.Id)));

		/// <inheritdoc />
		public IGetResponse<TDocument> Get<TDocument>(IGetRequest request) where TDocument : class =>
			Dispatcher.Dispatch<IGetRequest, GetRequestParameters, GetResponse<TDocument>>(
				request,
				(p, d) => LowLevelDispatch.GetDispatch<GetResponse<TDocument>>(p)
			);

		/// <inheritdoc />
		public Task<IGetResponse<TDocument>> GetAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<GetDescriptor<TDocument>, IGetRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class => GetAsync<TDocument>(selector.InvokeOrDefault(new GetDescriptor<TDocument>(document.Document, document.Self.Index, document.Self.Id)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetResponse<TDocument>> GetAsync<TDocument>(IGetRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where TDocument : class =>
			Dispatcher.DispatchAsync<IGetRequest, GetRequestParameters, GetResponse<TDocument>, IGetResponse<TDocument>>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.GetDispatchAsync<GetResponse<TDocument>>(p, c)
			);
	}
}
