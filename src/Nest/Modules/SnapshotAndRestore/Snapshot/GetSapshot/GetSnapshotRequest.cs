using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetSnapshotRequest { }

	public partial class GetSnapshotRequest 
	{
	}

	[DescriptorFor("SnapshotGet")]
	public partial class GetSnapshotDescriptor 
	{
	}
}
