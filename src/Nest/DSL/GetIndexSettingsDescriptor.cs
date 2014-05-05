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
	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor : 
		IndexPathDescriptorBase<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters>
		, IPathInfo<GetIndexSettingsRequestParameters>
	{
		ElasticsearchPathInfo<GetIndexSettingsRequestParameters> IPathInfo<GetIndexSettingsRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
