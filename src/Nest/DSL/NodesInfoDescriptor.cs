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
	[DescriptorFor("NodesInfo")]
	public partial class NodesInfoDescriptor : NodeIdOptionalDescriptor<NodesInfoDescriptor, NodesInfoQueryString>
		, IPathInfo<NodesInfoQueryString>
	{
		ElasticsearchPathInfo<NodesInfoQueryString> IPathInfo<NodesInfoQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<NodesInfoQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			return pathInfo;
		}

	}
}
