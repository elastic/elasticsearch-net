using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesExists")]
	public partial class IndexExistsDescriptor : IndexPathDescriptorBase<IndexExistsDescriptor, IndexExistsRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexExistsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}
}
