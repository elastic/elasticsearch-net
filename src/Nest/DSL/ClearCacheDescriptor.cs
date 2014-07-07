using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesClearCache")]
	public partial class ClearCacheDescriptor : IndicesOptionalPathDescriptor<ClearCacheDescriptor, ClearCacheRequestParameters>
	{

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClearCacheRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
}
