using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesClose")]
	public partial class CloseIndexDescriptor : IndexPathDescriptorBase<CloseIndexDescriptor, CloseIndexRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CloseIndexRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
}
