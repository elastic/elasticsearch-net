using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface ITemplateExistsRequest { }

	public partial class TemplateExistsRequest { }

	[DescriptorFor("IndicesExistsTemplate")]
	public partial class TemplateExistsDescriptor { }
}
