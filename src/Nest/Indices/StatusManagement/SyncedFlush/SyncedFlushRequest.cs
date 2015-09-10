using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ISyncedFlushRequest : IIndicesOptionalExplicitAllPath<SyncedFlushRequestParameters>
	{
	}

	public partial class SyncedFlushRequest : IndicesOptionalExplicitAllPathBase<SyncedFlushRequestParameters>, ISyncedFlushRequest
	{
		public SyncedFlushRequest(Indices indices) : base(indices) { }
	}
	
	[DescriptorFor("IndicesFlushSynced")]
	public partial class SyncedFlushDescriptor 
		: IndicesOptionalExplicitAllPathDescriptor<SyncedFlushDescriptor, SyncedFlushRequestParameters>
		, ISyncedFlushRequest
	{
		public SyncedFlushDescriptor(Indices indices) : base(indices) { }
	}
}
