using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IOpenIndexRequest { }

	public partial class OpenIndexRequest { }

	[DescriptorFor("IndicesOpen")]
	public partial class OpenIndexDescriptor { }
}
