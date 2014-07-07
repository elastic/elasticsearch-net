using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesDeleteMapping")]
	public partial class DeleteMappingDescriptor : IndexTypePathDescriptor<DeleteMappingDescriptor, DeleteMappingRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteMappingRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
}
