using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IAliasExistsRequest { }

	public partial class AliasExistsRequest { }

	[DescriptorFor("IndicesExistsAlias")]
	public partial class AliasExistsDescriptor { }
}
