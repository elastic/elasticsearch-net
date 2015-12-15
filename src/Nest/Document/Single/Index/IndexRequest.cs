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
		protected override HttpMethod HttpMethod => ((IIndexRequest<TDocument>)this).Id == null ? HttpMethod.POST : HttpMethod.PUT;

		partial void DocumentFromPath(TDocument doc) => this.Document = doc;

		object IIndexRequest.UntypedDocument => this.Document;

		public TDocument Document { get; set; }
	}

	public partial class IndexDescriptor<TDocument>  where TDocument : class
	{
		protected override HttpMethod HttpMethod => Self.Id == null ? HttpMethod.POST : HttpMethod.PUT;
		partial void DocumentFromPath(TDocument doc) => Assign(a => a.Document = doc); 
		object IIndexRequest.UntypedDocument => Self.Document;

		TDocument IIndexRequest<TDocument>.Document { get; set; }
	}
}
