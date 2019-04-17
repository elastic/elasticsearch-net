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
		GetResponse<TDocument> Get<TDocument>(DocumentPath<TDocument> document, Func<GetDescriptor<TDocument>, IGetRequest> selector = null) where TDocument : class;

		/// <inheritdoc />
		GetResponse<TDocument> Get<TDocument>(IGetRequest request) where TDocument : class;

		/// <inheritdoc />
		Task<GetResponse<TDocument>> GetAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<GetDescriptor<TDocument>, IGetRequest> selector = null,
			CancellationToken ct = default
		) where TDocument : class;

		/// <inheritdoc />
		Task<GetResponse<TDocument>> GetAsync<TDocument>(IGetRequest request, CancellationToken ct = default) where TDocument : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetResponse<TDocument> Get<TDocument>(DocumentPath<TDocument> document, Func<GetDescriptor<TDocument>, IGetRequest> selector = null)
			where TDocument : class =>
			Get<TDocument>(selector.InvokeOrDefault(new GetDescriptor<TDocument>(document.Document, document.Self.Index, document.Self.Id)));

		/// <inheritdoc />
		public GetResponse<TDocument> Get<TDocument>(IGetRequest request) where TDocument : class =>
			DoRequest<IGetRequest, GetResponse<TDocument>>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetResponse<TDocument>> GetAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<GetDescriptor<TDocument>, IGetRequest> selector = null,
			CancellationToken ct = default
		)
			where TDocument : class =>
			GetAsync<TDocument>(selector.InvokeOrDefault(new GetDescriptor<TDocument>(document.Document, document.Self.Index, document.Self.Id)), ct);

		/// <inheritdoc />
		public Task<GetResponse<TDocument>> GetAsync<TDocument>(IGetRequest request, CancellationToken ct = default)
			where TDocument : class =>
			DoRequestAsync<IGetRequest, GetResponse<TDocument>>(request, request.RequestParameters, ct);
	}
}
