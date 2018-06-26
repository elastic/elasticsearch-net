using System;

namespace Nest
{
	public partial interface IForceMergeRequest { }

	public partial class ForceMergeRequest { }

	[DescriptorFor("IndicesForcemerge")]
	public partial class ForceMergeDescriptor
	{
		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public ForceMergeDescriptor OperationThreading(string operationThreading) => this;

		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public ForceMergeDescriptor WaitForMerge(bool? waitForMerge = true) => this;
	}
}
