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
	[DescriptorFor("IndicesOpen")]
	public partial class OpenIndexDescriptor : IndexPathDescriptorBase<OpenIndexDescriptor, OpenIndexQueryString>
		, IPathInfo<OpenIndexQueryString>
	{
		ElasticsearchPathInfo<OpenIndexQueryString> IPathInfo<OpenIndexQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<OpenIndexQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			
			return pathInfo;
		}
	}
}
