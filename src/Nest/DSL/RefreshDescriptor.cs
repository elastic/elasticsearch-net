using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesRefresh")]
	public partial class RefreshDescriptor : IndicesOptionalPathDescriptor<RefreshDescriptor, RefreshRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<RefreshRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
}
