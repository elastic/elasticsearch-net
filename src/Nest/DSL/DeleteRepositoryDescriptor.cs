using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteRepositoryRequest : IRepositoryPath<DeleteRepositoryRequestParameters> { }

	internal static class DeleteRepositoryPathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteRepositoryRequestParameters> pathInfo, IDeleteRepositoryRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
	
	public partial class DeleteRepositoryRequest : RepositoryPathBase<DeleteRepositoryRequestParameters>, IDeleteRepositoryRequest
	{
		public DeleteRepositoryRequest(string repositoryName) : base(repositoryName) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRepositoryRequestParameters> pathInfo)
		{
			DeleteRepositoryPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("SnapshotDeleteRepository")]
	public partial class DeleteRepositoryDescriptor : RepositoryPathDescriptor<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters>, IDeleteRepositoryRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRepositoryRequestParameters> pathInfo)
		{
			DeleteRepositoryPathInfo.Update(pathInfo, this);
		}

	}
}
