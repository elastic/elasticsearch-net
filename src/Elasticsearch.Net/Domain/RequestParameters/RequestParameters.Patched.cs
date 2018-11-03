using System;

namespace Elasticsearch.Net
{
	public partial class ForceMergeRequestParameters
	{
		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading { get; set; }

		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public bool? WaitForMerge { get; set; }
	}

	public partial class SegmentsRequestParameters
	{
		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading { get; set; }
	}

	public partial class IndicesShardStoresRequestParameters
	{
		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading { get; set; }
	}

	public partial class ValidateQueryRequestParameters
	{
		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading { get; set; }
	}
}
