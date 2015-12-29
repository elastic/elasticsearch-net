using Elasticsearch.Net;

namespace Nest
{
	public partial interface IDocumentExistsRequest { }

	public interface IDocumentExistsRequest<T> : IDocumentExistsRequest where T : class { }

	public partial class DocumentExistsRequest { }

	public partial class DocumentExistsRequest<T> : RequestBase<DocumentExistsRequestParameters>, IDocumentExistsRequest
		where T : class
	{
	}

	[DescriptorFor("Exists")]
	public partial class DocumentExistsDescriptor<T> where T : class
	{
	}
}
