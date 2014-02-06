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
	public partial class ClusterNodeInfoDescriptor : NodeIdOptionalDescriptor<ClusterNodeInfoDescriptor, ClusterNodeInfoQueryString>
		, IPathInfo<ClusterNodeInfoQueryString>
	{
		ElasticSearchPathInfo<ClusterNodeInfoQueryString> IPathInfo<ClusterNodeInfoQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<ClusterNodeInfoQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			return pathInfo;
		}

	}
}
