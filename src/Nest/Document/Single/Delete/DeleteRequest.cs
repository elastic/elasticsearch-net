using Elasticsearch.Net;

namespace Nest
{
	public partial interface IDeleteRequest : IRequest<DeleteRequestParameters> { }

	public interface IDeleteRequest<T> : IDeleteRequest where T : class { }

	public partial class DeleteRequest
	{
		private object AutoRouteDocument() => null;
	}

	public partial class DeleteRequest<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}

	[DescriptorFor("Delete")]
	public partial class DeleteDescriptor<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}
}
