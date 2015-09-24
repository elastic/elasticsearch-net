using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IOptimizeRequest { }

	public partial class OptimizeRequest { }

	[DescriptorFor("IndicesOptimize")]
	public partial class OptimizeDescriptor { }
}
