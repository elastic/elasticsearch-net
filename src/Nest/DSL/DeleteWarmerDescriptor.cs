using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesDeleteWarmer")]
	public partial class DeleteWarmerDescriptor : IndicesOptionalTypesNamePathDecriptor<DeleteWarmerDescriptor, DeleteWarmerRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteWarmerRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
}
