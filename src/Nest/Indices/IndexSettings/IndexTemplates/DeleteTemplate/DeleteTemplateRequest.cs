using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteTemplateRequest { }

	public partial class DeleteTemplateRequest { }

	[DescriptorFor("IndicesDeleteTemplate")]
	public partial class DeleteTemplateDescriptor { }
}
