using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class ClusterStateDescriptor : 
		IndicesOptionalPathDescriptor<ClusterStateDescriptor, ClusterStateQueryString>
		, IPathInfo<ClusterStateQueryString>
	{
		ElasticSearchPathInfo<ClusterStateQueryString> IPathInfo<ClusterStateQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<ClusterStateQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			
			return pathInfo;
		}
	}
}
