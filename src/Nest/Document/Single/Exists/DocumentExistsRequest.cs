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
        public T Document { get; set; }

		public DocumentExistsRequest(string id) 
            : base(p => p.RequiredId(id)) { }

		public DocumentExistsRequest(long id) 
            : base(p => p.RequiredId(id.ToString())) { }

		public DocumentExistsRequest(T document)
        {
            Document = document;
        }

        protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath path)
        {
        }
    }

	[DescriptorFor("Exists")]
	public partial class DocumentExistsDescriptor<T>
		: RequestDescriptorBase<DocumentExistsDescriptor<T>, DocumentExistsRequestParameters>, IDocumentExistsRequest
		where T : class
	{
        public T Document { get; set; }

		public DocumentExistsDescriptor(string id) 
            : base(p => p.RequiredId(id)) { }

		public DocumentExistsDescriptor(long id) 
            : base(p => p.RequiredId(id.ToString())) { }

		public DocumentExistsDescriptor(T document)
        {
            Document = document;
        }

        protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
        {
            pathInfo.IdFrom(settings, this.Document);
        }
    }
}
