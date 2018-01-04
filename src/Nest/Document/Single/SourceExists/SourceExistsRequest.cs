using Elasticsearch.Net;

namespace Nest
{
	public partial interface ISourceExistsRequest { }

	public interface ISourceExistsRequest<T> : ISourceExistsRequest where T : class { }

	public partial class SourceExistsRequest
	{
		private object AutoRouteDocument() => null;
	}

	public partial class SourceExistsRequest<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}

	[DescriptorFor("ExistsSource")]
	public partial class SourceExistsDescriptor<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}
}
