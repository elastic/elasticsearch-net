using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesGetTemplate")]
	public partial class GetTemplateDescriptor :
		NamePathDescriptor<GetTemplateDescriptor, GetTemplateRequestParameters>
		, IPathInfo<GetTemplateRequestParameters>
	{
		ElasticsearchPathInfo<GetTemplateRequestParameters> IPathInfo<GetTemplateRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<GetTemplateRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
		
	}
}
