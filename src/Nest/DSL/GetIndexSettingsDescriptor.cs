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
		IndexPathDescriptorBase<GetIndexSettingsDescriptor, GetIndexSettingsQueryString>
		, IPathInfo<GetIndexSettingsQueryString>
	{
		ElasticsearchPathInfo<GetIndexSettingsQueryString> IPathInfo<GetIndexSettingsQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<GetIndexSettingsQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
