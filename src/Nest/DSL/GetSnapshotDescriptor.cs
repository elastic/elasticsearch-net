using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetSnapshotRequest : IRequest<GetSnapshotRequestParameters> { }

	internal static class GetSnapshotPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetSnapshotRequestParameters> pathInfo, IGetSnapshotRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetSnapshotRequest : RepositorySnapshotPathBase<GetSnapshotRequestParameters>, IGetSnapshotRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetSnapshotRequestParameters> pathInfo)
		{
			GetSnapshotPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("SnapshotGet")]
	public partial class GetSnapshotDescriptor : RepositorySnapshotPathDescriptor<GetSnapshotDescriptor, GetSnapshotRequestParameters>, IGetSnapshotRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetSnapshotRequestParameters> pathInfo)
		{
			GetSnapshotPathInfo.Update(pathInfo, this);
		}

	}
}
