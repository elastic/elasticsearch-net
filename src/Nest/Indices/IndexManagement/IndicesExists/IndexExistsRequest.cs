using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IIndexExistsRequest { }

	public partial class IndexExistsRequest { }

	[DescriptorFor("IndicesExists")]
	public partial class IndexExistsDescriptor { }
}
