using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("SnapshotDelete")]
	public partial class DeleteSnapshotDescriptor : RepositorySnapshotPathDescriptor<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteSnapshotRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}

	}
}
