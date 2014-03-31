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
	[DescriptorFor("IndicesStatus")]
	public partial class IndicesStatusDescriptor : 
		IndicesOptionalPathDescriptor<IndicesStatusDescriptor, IndicesStatsRequestParameters>
		, IPathInfo<IndicesStatusRequestParameters>
	{
		
		ElasticsearchPathInfo<IndicesStatusRequestParameters> IPathInfo<IndicesStatusRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<IndicesStatusRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
