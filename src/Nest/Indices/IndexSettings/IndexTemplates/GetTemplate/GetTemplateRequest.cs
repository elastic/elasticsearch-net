using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetTemplateRequest { }

	public partial class GetTemplateRequest { }

	[DescriptorFor("IndicesGetTemplate")]
	public partial class GetTemplateDescriptor { }
}
