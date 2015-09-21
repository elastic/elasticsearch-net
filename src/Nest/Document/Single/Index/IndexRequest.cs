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
		object IIndexRequest.UntypedDocument => this.Document;

		public TDocument Document { get; set; }
	}
	
	public partial class IndexDescriptor<T> : IIndexRequest<T>
		where T : class
	{
		object IIndexRequest.UntypedDocument => ((IIndexRequest<T>)this).Document;

		T IIndexRequest<T>.Document { get; set; }

		public IndexDescriptor<T> Document(T document) => Assign(a => ((IIndexRequest<T>)this).Document = document);

	}
}
