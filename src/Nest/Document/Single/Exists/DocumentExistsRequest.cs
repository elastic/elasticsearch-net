using Elasticsearch.Net;

namespace Nest_5_2_0
{
	public partial interface IDocumentExistsRequest { }

	public interface IDocumentExistsRequest<T> : IDocumentExistsRequest where T : class { }

	public partial class DocumentExistsRequest { }

	public partial class DocumentExistsRequest<T> where T : class { }

	[DescriptorFor("Exists")]
	public partial class DocumentExistsDescriptor<T> where T : class
	{
	}
}
