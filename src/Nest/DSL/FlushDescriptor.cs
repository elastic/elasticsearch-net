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
	[DescriptorFor("IndicesFlush")]
	public partial class FlushDescriptor : 
		IndicesOptionalExplicitAllPathDescriptor<FlushDescriptor, FlushQueryString>
		, IPathInfo<FlushQueryString>
	{
		ElasticsearchPathInfo<FlushQueryString> IPathInfo<FlushQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<FlushQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
