using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor : IndicesOptionalPathDescriptor<AnalyzeDescriptor, AnalyzeRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AnalyzeRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
}
