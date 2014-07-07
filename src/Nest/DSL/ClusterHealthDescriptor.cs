using Elasticsearch.Net;

namespace Nest
{
	public partial class ClusterHealthDescriptor : IndicesOptionalPathDescriptor<ClusterHealthDescriptor, ClusterHealthRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterHealthRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
