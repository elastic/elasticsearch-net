using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IRefreshRequest { }

	public partial class RefreshRequest { }

	[DescriptorFor("IndicesRefresh")]
	public partial class RefreshDescriptor { }
}
