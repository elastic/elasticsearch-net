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
	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor : 
		IndexPathDescriptorBase<GetIndexSettingsDescriptor, GetIndexSettingsQueryString>
		, IPathInfo<GetIndexSettingsQueryString>
	{
		ElasticSearchPathInfo<GetIndexSettingsQueryString> IPathInfo<GetIndexSettingsQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<GetIndexSettingsQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
