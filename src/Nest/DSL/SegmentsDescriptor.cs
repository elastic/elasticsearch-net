using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[DescriptorFor("IndicesSegments")]
	public partial class SegmentsDescriptor : IndicesOptionalPathDescriptor<SegmentsDescriptor, SegmentsQueryString>
		, IPathInfo<SegmentsQueryString>
	{
		ElasticsearchPathInfo<SegmentsQueryString> IPathInfo<SegmentsQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<SegmentsQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			
			return pathInfo;
		}
	}
}
