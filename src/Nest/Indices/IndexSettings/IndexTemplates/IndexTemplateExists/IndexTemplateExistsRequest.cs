using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IIndexTemplateExistsRequest { }

	public partial class IndexTemplateExistsRequest { }

	[DescriptorFor("IndicesExistsTemplate")]
	public partial class IndexTemplateExistsDescriptor { }
}
