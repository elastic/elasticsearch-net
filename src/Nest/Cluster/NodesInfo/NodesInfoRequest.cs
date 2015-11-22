using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface INodesInfoRequest { }

	public partial class NodesInfoRequest { }

	[DescriptorFor("NodesInfo")]
	public partial class NodesInfoDescriptor { }
}
