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
	[DescriptorFor("IndicesGetWarmer")]
	public partial class GetWarmerDescriptor : 
		IndicesOptionalTypesNamePathDecriptor<GetWarmerDescriptor, GetWarmerRequestParameters>
		, IPathInfo<GetWarmerRequestParameters>
	{
		ElasticsearchPathInfo<GetWarmerRequestParameters> IPathInfo<GetWarmerRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
