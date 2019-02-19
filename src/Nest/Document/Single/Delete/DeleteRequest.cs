using Elasticsearch.Net;

namespace Nest
{
	[MapsApi("delete.json")]
	public partial interface IDeleteRequest { }

	public partial interface IDeleteRequest<TDocument> where TDocument : class { }

	public partial class DeleteRequest { }

	public partial class DeleteRequest<TDocument> where TDocument : class
	{
		private object AutoRouteDocument() => null;
	}

	public partial class DeleteDescriptor<TDocument> where TDocument : class
	{
		private object AutoRouteDocument() => null;
	}
}
