using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface ISegmentsRequest { }

	public partial class SegmentsRequest { }
	
	[DescriptorFor("IndicesSegments")]
	public partial class SegmentsDescriptor { }
}
