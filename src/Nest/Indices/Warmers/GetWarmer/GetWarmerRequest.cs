using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetWarmerRequest { }

	public partial class GetWarmerRequest { }

	[DescriptorFor("IndicesGetWarmer")]
	public partial class GetWarmerDescriptor { }
}
