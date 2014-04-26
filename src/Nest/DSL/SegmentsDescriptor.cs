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
	[DescriptorFor("IndicesSegments")]
	public partial class SegmentsDescriptor : IndicesOptionalPathDescriptor<SegmentsDescriptor, SegmentsRequestParameters>
		, IPathInfo<SegmentsRequestParameters>
	{
		ElasticsearchPathInfo<SegmentsRequestParameters> IPathInfo<SegmentsRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<SegmentsRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			
			return pathInfo;
		}
	}
}
