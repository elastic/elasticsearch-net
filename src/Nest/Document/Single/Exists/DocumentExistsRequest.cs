using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDocumentExistsRequest : IDocumentOptionalPath<DocumentExistsRequestParameters> { }

	public interface IDocumentExistsRequest<T> : IDocumentExistsRequest where T : class {}

	public partial class DocumentExistsRequest : DocumentPathBase<DocumentExistsRequestParameters>, IDocumentExistsRequest
	{
		public DocumentExistsRequest(IndexName indexName, TypeName typeName, string id) : base(indexName, typeName, id) { }
	}
	
	public partial class DocumentExistsRequest<T> : DocumentPathBase<DocumentExistsRequestParameters, T>, IDocumentExistsRequest
		where T : class
	{
		public DocumentExistsRequest(string id) : base(id) { }

		public DocumentExistsRequest(long id) : base(id) { }

		public DocumentExistsRequest(T document) : base(document) { }
	}

	[DescriptorFor("Exists")]
	public partial class DocumentExistsDescriptor<T>
		: DocumentPathDescriptor<DocumentExistsDescriptor<T>, DocumentExistsRequestParameters, T>, IDocumentExistsRequest
		where T : class
	{
	}
}
