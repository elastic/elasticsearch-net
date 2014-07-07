using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("Exists")]
	public partial class DocumentExistsDescriptor<T> : DocumentPathDescriptorBase<DocumentExistsDescriptor<T>, T, DocumentExistsRequestParameters>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DocumentExistsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}
}
