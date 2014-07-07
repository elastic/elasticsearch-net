using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesDeleteTemplate")]
	public partial class DeleteTemplateDescriptor : NamePathDescriptor<DeleteTemplateDescriptor, DeleteTemplateRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}

	}
}
