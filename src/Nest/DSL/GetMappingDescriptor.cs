using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor : 
		IndexTypePathDescriptor<GetMappingDescriptor, GetMappingQueryString>
		, IPathInfo<GetMappingQueryString>
	{
		ElasticSearchPathInfo<GetMappingQueryString> IPathInfo<GetMappingQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<GetMappingQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
