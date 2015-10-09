using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface INodesStatsRequest { }

	public partial class NodesStatsRequest { }
	[DescriptorFor("NodesStats")]
	public partial class NodesStatsDescriptor { }
}
