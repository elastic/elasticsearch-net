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
	public partial class RefreshDescriptor : IndicesOptionalPathDescriptor<RefreshDescriptor, RefreshRequestParameters>
		, IPathInfo<RefreshRequestParameters>
	{
		ElasticsearchPathInfo<RefreshRequestParameters> IPathInfo<RefreshRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<RefreshRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			
			return pathInfo;
		}
	}
}
