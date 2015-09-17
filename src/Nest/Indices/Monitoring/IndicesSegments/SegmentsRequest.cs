using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface ISegmentsRequest : IRequest<SegmentsRequestParameters> { }

	public partial class SegmentsRequest : RequestBase<SegmentsRequestParameters>, ISegmentsRequest
	{
	}
	
	[DescriptorFor("IndicesSegments")]
	public partial class SegmentsDescriptor
		: RequestDescriptorBase<SegmentsDescriptor, SegmentsRequestParameters>, ISegmentsRequest
	{
	}
}
