using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IFlushRequest { }

	public partial class FlushRequest { }

	[DescriptorFor("IndicesFlush")]
	public partial class FlushDescriptor { }
}
