using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IndexRequestJsonConverter))]
	public interface IIndexRequest : IRequest<IndexRequestParameters>
	{
		object UntypedDocument { get; }
	}

	public partial interface IIndexRequest<TDocument> : IIndexRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class IndexRequest<TDocument> 
		where TDocument : class
	{
		protected override HttpMethod HttpMethod => GetHttpMethod(this);

		partial void DocumentFromPath(TDocument document) => this.Document = document;

		object IIndexRequest.UntypedDocument => this.Document;

		public TDocument Document { get; set; }

		internal static HttpMethod GetHttpMethod(IIndexRequest<TDocument> r) => r.Id?.Value != null ? HttpMethod.PUT : HttpMethod.POST;
	}

	public partial class IndexDescriptor<TDocument>  where TDocument : class
	{
		protected override HttpMethod HttpMethod => IndexRequest<TDocument>.GetHttpMethod(this);
		partial void DocumentFromPath(TDocument document) => Assign(a => a.Document = document); 
		object IIndexRequest.UntypedDocument => Self.Document;

		TDocument IIndexRequest<TDocument>.Document { get; set; }
	}
}
