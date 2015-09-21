using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface ICloseIndexRequest { }

	public partial class CloseIndexRequest { }

	[DescriptorFor("IndicesClose")]
	public partial class CloseIndexDescriptor { }
}
