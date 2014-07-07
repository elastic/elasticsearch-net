using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesDelete")]
	public partial class DeleteIndexDescriptor : IndicesOptionalPathDescriptor<DeleteIndexDescriptor, DeleteIndexRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteIndexRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
}
