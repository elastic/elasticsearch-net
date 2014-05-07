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
	[DescriptorFor("IndicesOptimize")]
	public partial class OptimizeDescriptor : IndicesOptionalPathDescriptor<OptimizeDescriptor, OptimizeRequestParameters>
		, IPathInfo<OptimizeRequestParameters>
	{
		ElasticsearchPathInfo<OptimizeRequestParameters> IPathInfo<OptimizeRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			
			return pathInfo;
		}
	}
}
