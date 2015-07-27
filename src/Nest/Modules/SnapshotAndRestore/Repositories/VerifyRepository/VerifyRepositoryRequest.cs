using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IVerifyRepositoryRequest : IRepositoryPath<VerifyRepositoryRequestParameters> { }

	internal static class VerifyRepositoryPathInfo
	{
		public static void Update(ElasticsearchPathInfo<VerifyRepositoryRequestParameters> pathInfo, IVerifyRepositoryRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}
	
	public partial class VerifyRepositoryRequest : RepositoryPathBase<VerifyRepositoryRequestParameters>, IVerifyRepositoryRequest
	{
		public VerifyRepositoryRequest(string repositoryName) : base(repositoryName) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<VerifyRepositoryRequestParameters> pathInfo)
		{
			VerifyRepositoryPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("SnapshotVerifyRepository")]
	public partial class VerifyRepositoryDescriptor : RepositoryPathDescriptor<VerifyRepositoryDescriptor, VerifyRepositoryRequestParameters>, IVerifyRepositoryRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<VerifyRepositoryRequestParameters> pathInfo)
		{
			VerifyRepositoryPathInfo.Update(pathInfo, this);
		}

	}
}
