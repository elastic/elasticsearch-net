using Elasticsearch.Net;

namespace Nest
{
	internal static class NodesHotThreadsPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<NodesHotThreadsRequestParameters> pathInfo, INodesHotThreadsRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}

	public interface INodesHotThreadsRequest 
		: INodeIdOptionalPath<NodesHotThreadsRequestParameters>
	{
	}

	public partial class NodesHotThreadsRequest 
		: NodeIdOptionalPathBase<NodesHotThreadsRequestParameters>, INodesHotThreadsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<NodesHotThreadsRequestParameters> pathInfo)
		{
			NodesHotThreadsPathInfo.Update(settings, pathInfo, this);
		}
	}

	public partial class NodesHotThreadsDescriptor 
		: NodeIdOptionalPathBase<NodesHotThreadsRequestParameters>, INodesHotThreadsRequest
	{
		private INodesHotThreadsRequest Self { get { return this; } }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<NodesHotThreadsRequestParameters> pathInfo)
		{
			NodesHotThreadsPathInfo.Update(settings, pathInfo, this.Self);
		}
	}
}
