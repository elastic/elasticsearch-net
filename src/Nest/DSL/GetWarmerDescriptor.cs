using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesGetWarmer")]
	public partial class GetWarmerDescriptor : IndicesOptionalTypesNamePathDecriptor<GetWarmerDescriptor, GetWarmerRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetWarmerRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
