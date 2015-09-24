using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	//TODO if id == null do a POST otherwise a PUT

	public partial interface IIndexRequest : IRequest<IndexRequestParameters>
	{
		object UntypedDocument { get; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(IndexRequestJsonConverter))]
	public interface IIndexRequest<TDocument> : IIndexRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class IndexRequest<TDocument> : IIndexRequest<TDocument>
		where TDocument : class
	{
		protected override HttpMethod HttpMethod => ((IIndexRequest)this).Id == null ? HttpMethod.POST : HttpMethod.PUT;
		partial void DocumentFromPath(TDocument doc) => this.Document = doc;

		object IIndexRequest.UntypedDocument => this.Document;

		public TDocument Document { get; set; }
	}

	public partial class IndexDescriptor<T> : IIndexRequest<T>
		where T : class
	{
		protected override HttpMethod HttpMethod => ((IIndexRequest)this).Id == null ? HttpMethod.POST : HttpMethod.PUT;
		partial void DocumentFromPath(T doc) => ((IIndexRequest<T>)this).Document = doc;
		object IIndexRequest.UntypedDocument => ((IIndexRequest<T>)this).Document;

		T IIndexRequest<T>.Document { get; set; }

		public IndexDescriptor<T> Index(IndexName index) => Assign(a => a.RouteValues.Required("index", index));
		public IndexDescriptor<T> Type(TypeName type) => Assign(a => a.RouteValues.Required("type", type));

	}
}
