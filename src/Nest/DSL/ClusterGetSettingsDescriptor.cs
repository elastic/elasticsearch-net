using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	internal static class ClusterGetSettingsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<ClusterGetSettingsRequestParameters> pathInfo, IClusterGetSettingsRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClusterGetSettingsRequest : IRequest<ClusterGetSettingsRequestParameters>
	{

	}
	
	public partial class ClusterGetSettingsRequest : BaseRequest<ClusterGetSettingsRequestParameters>, IClusterGetSettingsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterGetSettingsRequestParameters> pathInfo)
		{
			ClusterGetSettingsPathInfo.Update(pathInfo, this);
		}
	}

	public partial class ClusterGetSettingsDescriptor : BasePathDescriptor<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters>
		, IClusterGetSettingsRequest
	{
		protected IClusterGetSettingsRequest Self { get { return this; } }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterGetSettingsRequestParameters> pathInfo)
		{
			ClusterGetSettingsPathInfo.Update(pathInfo, this);
		}
	}
}
