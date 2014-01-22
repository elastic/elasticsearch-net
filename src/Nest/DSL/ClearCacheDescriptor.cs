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
	[DescriptorFor("IndicesClearCache")]
	public partial class ClearCacheDescriptor : 
		IndicesOptionalPathDescriptorBase<ClearCacheDescriptor, ClearCacheQueryString>
		, IPathInfo<ClearCacheQueryString>
	{
		ElasticSearchPathInfo<ClearCacheQueryString> IPathInfo<ClearCacheQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<ClearCacheQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
