using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public partial interface ISyncedFlushRequest { }

	public partial class SyncedFlushRequest { }
	
	[DescriptorFor("IndicesFlushSynced")]
	public partial class SyncedFlushDescriptor { }
}
