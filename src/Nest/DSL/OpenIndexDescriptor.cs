using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesOpen")]
	public partial class OpenIndexDescriptor : IndexPathDescriptorBase<OpenIndexDescriptor, OpenIndexRequestParameters>
		, IPathInfo<OpenIndexRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<OpenIndexRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
}
