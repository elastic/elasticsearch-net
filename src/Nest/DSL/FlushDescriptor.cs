using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesFlush")]
	public partial class FlushDescriptor : IndicesOptionalExplicitAllPathDescriptor<FlushDescriptor, FlushRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<FlushRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
}
