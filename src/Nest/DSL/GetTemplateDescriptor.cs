using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesGetTemplate")]
	[JsonConverter(typeof(ActAsQueryConverter))]
	public partial class GetTemplateDescriptor :
		NamePathDescriptor<GetTemplateDescriptor, GetTemplateQueryString>
		, IPathInfo<GetTemplateQueryString>
	{
		ElasticsearchPathInfo<GetTemplateQueryString> IPathInfo<GetTemplateQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<GetTemplateQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}

	}
}
