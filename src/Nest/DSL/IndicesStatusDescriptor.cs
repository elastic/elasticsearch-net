using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesStatus")]
	public partial class IndicesStatusDescriptor : IndicesOptionalPathDescriptor<IndicesStatusDescriptor, IndicesStatusRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
