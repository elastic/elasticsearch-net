using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesStats")]
	public partial class IndicesStatsDescriptor : 
		IndicesOptionalPathDescriptorBase<IndicesStatsDescriptor, IndicesStatsQueryString>
		, IPathInfo<IndicesStatsQueryString>
	{
		ElasticSearchPathInfo<IndicesStatsQueryString> IPathInfo<IndicesStatsQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<IndicesStatsQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
