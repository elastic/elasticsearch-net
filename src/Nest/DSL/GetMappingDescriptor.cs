using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor : IndexTypePathDescriptor<GetMappingDescriptor, GetMappingRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
