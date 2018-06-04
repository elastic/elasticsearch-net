using System;
using Elasticsearch.Net;

namespace Nest
{
	// Patched parameters - Update REST API json specifications to 6.2 #3268
	public partial class ForceMergeRequest : PlainRequestBase<ForceMergeRequestParameters>, IForceMergeRequest
	{
		[Obsolete("Removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading { get; set; }

		[Obsolete("Removed in NEST 7.x. Calling this is a no-op.")]
		public bool? WaitForMerge { get; set; }
	}

	// Patched parameters - Update REST API json specifications to 6.2 #3268
	public partial class IndicesShardStoresRequest : PlainRequestBase<IndicesShardStoresRequestParameters>, IIndicesShardStoresRequest
	{
		[Obsolete("Removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading{ get; set; }
	}

	// Patched parameters - Update REST API json specifications to 6.2 #3268
	public partial class SegmentsRequest : PlainRequestBase<SegmentsRequestParameters>, ISegmentsRequest
	{
		[Obsolete("Removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading{ get; set; }
	}

	// Patched parameters - Update REST API json specifications to 6.2 #3268
	public partial class ValidateQueryRequest<T> : PlainRequestBase<ValidateQueryRequestParameters>, IValidateQueryRequest
	{
		[Obsolete("Removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading{ get; set; }
	}

	// Patched parameters - Update REST API json specifications to 6.2 #3268
	public partial class ValidateQueryRequest : PlainRequestBase<ValidateQueryRequestParameters>, IValidateQueryRequest
	{
		[Obsolete("Removed in NEST 7.x. Calling this is a no-op.")]
		public string OperationThreading{ get; set; }
	}
}
