using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IndexJsonConverter))]
	public partial interface IIndexRequest : IRequest<IndexRequestParameters>, IUntypedDocumentRequest { }

	public partial interface IIndexRequest<TDocument> : IIndexRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class IndexRequest<TDocument>
		where TDocument : class
	{
		public TDocument Document { get; set; }
		protected override HttpMethod HttpMethod => GetHttpMethod(this);

		object IUntypedDocumentRequest.UntypedDocument => Document;

		partial void DocumentFromPath(TDocument document) => Document = document;

		internal static HttpMethod GetHttpMethod(IIndexRequest<TDocument> request) => request.Id?.Value != null ? HttpMethod.PUT : HttpMethod.POST;
	}

	public partial class IndexDescriptor<TDocument> where TDocument : class
	{
		protected override HttpMethod HttpMethod => IndexRequest<TDocument>.GetHttpMethod(this);

		TDocument IIndexRequest<TDocument>.Document { get; set; }
		object IUntypedDocumentRequest.UntypedDocument => Self.Document;

		partial void DocumentFromPath(TDocument document) => Assign(a => a.Document = document);
	}
}
