using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("Info")]
	public partial class InfoDescriptor : BasePathDescriptor<InfoDescriptor, InfoRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<InfoRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			pathInfo.RequestParameters = this.Request.RequestParameters;
		}
	}
}
