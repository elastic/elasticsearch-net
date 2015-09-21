using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteSnapshotRequest { }

	public partial class DeleteSnapshotRequest { }

	[DescriptorFor("SnapshotDelete")]
	public partial class DeleteSnapshotDescriptor { }
}
