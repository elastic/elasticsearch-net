using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("SnapshotGet")]
	public partial class GetSnapshotDescriptor : RepositorySnapshotPathDescriptor<GetSnapshotDescriptor, GetSnapshotRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetSnapshotRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}

	}
}
