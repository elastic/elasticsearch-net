using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ISyncedFlushRequest : IRequest<SyncedFlushRequestParameters>
	{
	}

	public partial class SyncedFlushRequest : RequestBase<SyncedFlushRequestParameters>, ISyncedFlushRequest
	{
	}
	
	[DescriptorFor("IndicesFlushSynced")]
	public partial class SyncedFlushDescriptor 
		: RequestDescriptorBase<SyncedFlushDescriptor, SyncedFlushRequestParameters>
		, ISyncedFlushRequest
	{
	}
}
