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
	[DescriptorFor("IndicesFlush")]
	public partial class FlushDescriptor : 
		IndicesOptionalExplicitAllPathDescriptor<FlushDescriptor, FlushQueryString>
		, IPathInfo<FlushQueryString>
	{
		ElasticSearchPathInfo<FlushQueryString> IPathInfo<FlushQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<FlushQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
