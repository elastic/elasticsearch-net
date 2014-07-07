using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("Delete")]
	public partial class DeleteDescriptor<T> : DocumentPathDescriptorBase<DeleteDescriptor<T>, T, DeleteRequestParameters>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
}
