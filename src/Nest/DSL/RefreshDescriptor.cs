using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[DescriptorFor("IndicesRefresh")]
	public partial class RefreshDescriptor : IndicesOptionalPathDescriptor<RefreshDescriptor, RefreshQueryString>
		, IPathInfo<RefreshQueryString>
	{
		ElasticsearchPathInfo<RefreshQueryString> IPathInfo<RefreshQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<RefreshQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			
			return pathInfo;
		}
	}
}
