using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDocumentExistsRequest : IRequest<DocumentExistsRequestParameters> { }

	public interface IDocumentExistsRequest<T> : IDocumentExistsRequest where T : class {}

	public partial class DocumentExistsRequest : RequestBase<DocumentExistsRequestParameters>, IDocumentExistsRequest
	{
	}

    public partial class DocumentExistsRequest<T> : RequestBase<DocumentExistsRequestParameters>, IDocumentExistsRequest
        where T : class
    {
        public DocumentExistsRequest(Id id) : base(r => r.Required(Ids.Single(id))) { }
        public DocumentExistsRequest(T document) : base(r => r.Required(Ids.Single(document))) { }
    }

	[DescriptorFor("Exists")]
	public partial class DocumentExistsDescriptor<T>
		: RequestDescriptorBase<DocumentExistsDescriptor<T>, DocumentExistsRequestParameters>, IDocumentExistsRequest
		where T : class
	{
		public DocumentExistsDescriptor(Id id) : base(r => r.Required(Ids.Single(id))) { }
		public DocumentExistsDescriptor(T document) : base(r => r.Required(Ids.Single(document))) { }
    }
}
