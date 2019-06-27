using Elasticsearch.Net;
using Elasticsearch.Net.Specification.NodesApi;

namespace Nest
{
	[MapsApi("nodes.hot_threads.json")]
	public partial interface INodesHotThreadsRequest { }

	public partial class NodesHotThreadsRequest
	{
		protected override string ContentType => RequestData.MimeTypeTextPlain;

		protected sealed override void RequestDefaults(NodesHotThreadsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = NodeHotThreadsResponseBuilder.Instance;
	}

	public partial class NodesHotThreadsDescriptor
	{
		protected override string ContentType => RequestData.MimeTypeTextPlain;
		
		protected sealed override void RequestDefaults(NodesHotThreadsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = NodeHotThreadsResponseBuilder.Instance;
	}
}
