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
	[DescriptorFor("IndicesStatus")]
	public partial class IndicesStatusDescriptor : 
		IndicesOptionalPathDescriptor<IndicesStatusDescriptor, IndicesStatsQueryString>
		, IPathInfo<IndicesStatusQueryString>
	{
		
		ElasticsearchPathInfo<IndicesStatusQueryString> IPathInfo<IndicesStatusQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<IndicesStatusQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
