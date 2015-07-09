using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal static class ClusterPendingTasksPathInfo
	{
		public static void Update(ElasticsearchPathInfo<ClusterPendingTasksRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}

	public interface IClusterPendingTasksRequest : IRequest<ClusterPendingTasksRequestParameters>
	{
	}

	public partial class ClusterPendingTasksRequest 
		: BasePathRequest<ClusterPendingTasksRequestParameters>, IClusterPendingTasksRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterPendingTasksRequestParameters> pathInfo)
		{
			ClusterPendingTasksPathInfo.Update(pathInfo);
		}
	}

	public partial class ClusterPendingTasksDescriptor 
		: BasePathDescriptor<ClusterPendingTasksDescriptor, ClusterPendingTasksRequestParameters>, IClusterPendingTasksRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterPendingTasksRequestParameters> pathInfo)
		{
			ClusterPendingTasksPathInfo.Update(pathInfo);
		}
	}
}
