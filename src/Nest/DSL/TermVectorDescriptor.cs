using Elasticsearch.Net;

namespace Nest
{
	public partial class TermvectorDescriptor<T> : DocumentPathDescriptorBase<TermvectorDescriptor<T>, T, TermvectorRequestParameters>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
