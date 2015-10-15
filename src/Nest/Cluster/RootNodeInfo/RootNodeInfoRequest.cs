using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IRootNodeInfoRequest { }

	public partial class RootNodeInfoRequest { }

	[DescriptorFor("Info")]
	public partial class RootNodeInfoDescriptor { }
}
