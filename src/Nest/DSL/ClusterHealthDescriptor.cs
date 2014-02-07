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
	public partial class ClusterHealthDescriptor : 
		IndicesOptionalPathDescriptor<ClusterHealthDescriptor, ClusterHealthQueryString>
		, IPathInfo<ClusterHealthQueryString>
	{
		ElasticsearchPathInfo<ClusterHealthQueryString> IPathInfo<ClusterHealthQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<ClusterHealthQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			
			return pathInfo;
		}
	}
}
