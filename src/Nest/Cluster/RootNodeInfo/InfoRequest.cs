using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IInfoRequest { }

	public partial class InfoRequest { }

	[DescriptorFor("Info")]
	public partial class InfoDescriptor { }
}
