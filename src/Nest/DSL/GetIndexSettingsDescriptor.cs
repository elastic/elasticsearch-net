using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor : IndexPathDescriptorBase<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
