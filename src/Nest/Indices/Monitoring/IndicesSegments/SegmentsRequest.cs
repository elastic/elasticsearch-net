using System;

namespace Nest
{
	public partial interface ISegmentsRequest { }

	public partial class SegmentsRequest { }

	[DescriptorFor("IndicesSegments")]
	public partial class SegmentsDescriptor
	{
		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public SegmentsDescriptor OperationThreading(string operationThreading) => this;
	}
}
