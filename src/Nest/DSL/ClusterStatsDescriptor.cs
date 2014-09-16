using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal static class ClusterStatsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<ClusterStatsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}

	public interface IClusterStatsRequest : IRequest<ClusterStatsRequestParameters>
	{
	}

	public partial class ClusterStatsRequest : BasePathRequest<ClusterStatsRequestParameters>, IClusterStatsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterStatsRequestParameters> pathInfo)
		{
			ClusterStatsPathInfo.Update(pathInfo);
		}
	}

	public partial class ClusterStatsDescriptor 
		: BasePathDescriptor<ClusterStatsDescriptor, ClusterStatsRequestParameters>, IClusterStatsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterStatsRequestParameters> pathInfo)
		{
			ClusterStatsPathInfo.Update(pathInfo);
		}
	}
}
