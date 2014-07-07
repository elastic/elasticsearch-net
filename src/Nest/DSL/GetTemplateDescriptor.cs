using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesGetTemplate")]
	public partial class GetTemplateDescriptor : NamePathDescriptor<GetTemplateDescriptor, GetTemplateRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
		
	}
}
