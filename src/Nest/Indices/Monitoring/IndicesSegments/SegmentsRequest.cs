using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface ISegmentsRequest : IIndicesOptionalPath<SegmentsRequestParameters> { }

	public partial class SegmentsRequest : IndicesOptionalPathBase<SegmentsRequestParameters>, ISegmentsRequest
	{
	}
	
	[DescriptorFor("IndicesSegments")]
	public partial class SegmentsDescriptor 
		: IndicesOptionalPathDescriptor<SegmentsDescriptor, SegmentsRequestParameters>, ISegmentsRequest
	{
	}
}
