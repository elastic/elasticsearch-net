using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteWarmerRequest { }

	public partial class DeleteWarmerRequest { }

	[DescriptorFor("IndicesDeleteWarmer")]
	public partial class DeleteWarmerDescriptor { }
}
