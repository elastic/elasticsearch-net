using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetRepositoryRequest : IRepositoryOptionalPath<GetRepositoryRequestParameters>
	{
	}

	internal static class GetRepositoryPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetRepositoryRequestParameters> pathInfo, IGetRepositoryRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetRepositoryRequest : RepositoryOptionalPathBase<GetRepositoryRequestParameters>, IGetRepositoryRequest
	{
		public GetRepositoryRequest() { }
		public GetRepositoryRequest(string repositoryName) : base(repositoryName) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetRepositoryRequestParameters> pathInfo)
		{
			GetRepositoryPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("SnapshotGetRepository")]
	public partial class GetRepositoryDescriptor : RepositoryOptionalPathDescriptor<GetRepositoryDescriptor, GetRepositoryRequestParameters>, IGetRepositoryRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetRepositoryRequestParameters> pathInfo)
		{
			GetRepositoryPathInfo.Update(pathInfo, this);
		}
	}
}
