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
	[DescriptorFor("IndicesClose")]
	public partial class CloseIndexDescriptor : IndexPathDescriptorBase<CloseIndexDescriptor, CloseIndexRequestParameters>
		, IPathInfo<CloseIndexRequestParameters>
	{
		ElasticsearchPathInfo<CloseIndexRequestParameters> IPathInfo<CloseIndexRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<CloseIndexRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
