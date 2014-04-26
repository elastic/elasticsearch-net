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
	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor : 
		IndexTypePathDescriptor<GetMappingDescriptor, GetMappingRequestParameters>
		, IPathInfo<GetMappingRequestParameters>
	{
		ElasticsearchPathInfo<GetMappingRequestParameters> IPathInfo<GetMappingRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<GetMappingRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
