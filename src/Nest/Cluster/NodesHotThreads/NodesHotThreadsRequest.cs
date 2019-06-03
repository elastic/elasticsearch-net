namespace Nest
{
	[MapsApi("nodes.hot_threads.json")]
	public partial interface INodesHotThreadsRequest { }

	public partial class NodesHotThreadsRequest
	{
		protected override void Initialize() => Self.RequestParameters.CustomResponseBuilder = NodeHotThreadsResponseBuilder.Instance;
	}

	public partial class NodesHotThreadsDescriptor
	{
		protected override void Initialize() => Self.RequestParameters.CustomResponseBuilder = NodeHotThreadsResponseBuilder.Instance;
	}
}
