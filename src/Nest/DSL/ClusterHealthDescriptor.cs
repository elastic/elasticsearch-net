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
	public partial class ClusterHealthDescriptor : 
		IndicesOptionalPathDescriptor<ClusterHealthDescriptor, ClusterHealthRequestParameters>
		, IPathInfo<ClusterHealthRequestParameters>
	{
		ElasticsearchPathInfo<ClusterHealthRequestParameters> IPathInfo<ClusterHealthRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			
			return pathInfo;
		}
	}
}
