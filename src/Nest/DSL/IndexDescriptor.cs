using Elasticsearch.Net;

namespace Nest
{
	public partial class IndexDescriptor<T> : DocumentOptionalPathDescriptorBase<IndexDescriptor<T>, T, IndexRequestParameters>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			pathInfo.Index.ThrowIfNull("index");
			pathInfo.Type.ThrowIfNull("type");
			var id = pathInfo.Id;
			pathInfo.HttpMethod = id == null || id.IsNullOrEmpty() ? PathInfoHttpMethod.POST : PathInfoHttpMethod.PUT;
		}
	}
}
