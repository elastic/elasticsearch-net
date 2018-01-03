using Elasticsearch.Net;

namespace Nest
{
	public partial interface IDocumentExistsRequest { }

	public interface IDocumentExistsRequest<T> : IDocumentExistsRequest where T : class { }

	public partial class DocumentExistsRequest
	{
		private object AutoRouteDocument() => null;
	}

	public partial class DocumentExistsRequest<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}

	[DescriptorFor("Exists")]
	public partial class DocumentExistsDescriptor<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}
}
