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
	[DescriptorFor("IndicesGetWarmer")]
	public partial class GetWarmerDescriptor : 
		IndicesOptionalTypesNamePathDecriptor<GetWarmerDescriptor, GetWarmerQueryString>
		, IPathInfo<GetWarmerQueryString>
	{
		ElasticSearchPathInfo<GetWarmerQueryString> IPathInfo<GetWarmerQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<GetWarmerQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
