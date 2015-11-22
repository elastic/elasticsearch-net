using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IClearCacheRequest { }
	
	public partial class ClearCacheRequest { }

	[DescriptorFor("IndicesClearCache")]
	public partial class ClearCacheDescriptor { }
}
