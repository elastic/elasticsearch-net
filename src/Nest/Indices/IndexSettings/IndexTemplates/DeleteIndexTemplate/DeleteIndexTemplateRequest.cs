using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteIndexTemplateRequest { }

	public partial class DeleteIndexTemplateRequest { }

	[DescriptorFor("IndicesDeleteTemplate")]
	public partial class DeleteIndexTemplateDescriptor { }
}
