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
	[DescriptorFor("NodesStats")]
	public partial class NodesStatsDescriptor : NodeIdOptionalDescriptor<NodesStatsDescriptor, NodesStatsQueryString>
		, IPathInfo<NodesStatsQueryString>
	{
		ElasticsearchPathInfo<NodesStatsQueryString> IPathInfo<NodesStatsQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<NodesStatsQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			return pathInfo;
		}

	}
}
