using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("ClusterNodeInfo")]
	public partial class ClusterNodeStatsDescriptor : NodeIdOptionalDescriptor<ClusterNodeStatsDescriptor, ClusterNodeStatsQueryString>
		, IPathInfo<ClusterNodeStatsQueryString>
	{
		ElasticSearchPathInfo<ClusterNodeStatsQueryString> IPathInfo<ClusterNodeStatsQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<ClusterNodeStatsQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			return pathInfo;
		}

	}
}
