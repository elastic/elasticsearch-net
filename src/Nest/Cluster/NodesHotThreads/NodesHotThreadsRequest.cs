using Elasticsearch.Net.Specification.NodesApi;

namespace Nest
{
	[MapsApi("nodes.hot_threads.json")]
	public partial interface INodesHotThreadsRequest { }

	public partial class NodesHotThreadsRequest
	{
		protected sealed override void RequestDefaults(NodesHotThreadsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = NodeHotThreadsResponseBuilder.Instance;
	}

	public partial class NodesHotThreadsDescriptor
	{
		protected sealed override void RequestDefaults(NodesHotThreadsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = NodeHotThreadsResponseBuilder.Instance;
	}
}
