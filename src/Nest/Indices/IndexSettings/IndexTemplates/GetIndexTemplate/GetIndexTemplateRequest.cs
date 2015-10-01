using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetIndexTemplateRequest { }

	public partial class GetIndexTemplateRequest { }

	[DescriptorFor("IndicesGetTemplate")]
	public partial class GetIndexTemplateDescriptor { }
}
