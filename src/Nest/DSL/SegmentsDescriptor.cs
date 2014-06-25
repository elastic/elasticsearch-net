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
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SegmentsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
