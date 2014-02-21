using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesClearCache")]
	public partial class ClearCacheDescriptor : 
		IndicesOptionalPathDescriptor<ClearCacheDescriptor, ClearCacheQueryString>
		, IPathInfo<ClearCacheQueryString>
	{
		ElasticsearchPathInfo<ClearCacheQueryString> IPathInfo<ClearCacheQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<ClearCacheQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
