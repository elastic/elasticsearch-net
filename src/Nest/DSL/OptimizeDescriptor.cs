using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesOptimize")]
	public partial class OptimizeDescriptor : IndicesOptionalPathDescriptor<OptimizeDescriptor, OptimizeRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<OptimizeRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
}
