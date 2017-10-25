using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(IndexJsonConverter))]
	public partial interface IIndexRequest : IRequest<IndexRequestParameters>, IUntypedDocumentRequest { }

	public partial interface IIndexRequest<TDocument> : IIndexRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class IndexRequest<TDocument>
		where TDocument : class
	{
		protected override HttpMethod HttpMethod => GetHttpMethod(this);

		partial void DocumentFromPath(TDocument document) => this.Document = document;

		object IUntypedDocumentRequest.UntypedDocument => this.Document;

		public TDocument Document { get; set; }

		internal static HttpMethod GetHttpMethod(IIndexRequest<TDocument> request) => request.Id?.Value != null ? HttpMethod.PUT : HttpMethod.POST;
	}

	public partial class IndexDescriptor<TDocument>  where TDocument : class
	{
		protected override HttpMethod HttpMethod => IndexRequest<TDocument>.GetHttpMethod(this);
		partial void DocumentFromPath(TDocument document) => Assign(a => a.Document = document);
		object IUntypedDocumentRequest.UntypedDocument => Self.Document;

		TDocument IIndexRequest<TDocument>.Document { get; set; }
	}
}
