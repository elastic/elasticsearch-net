using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IClearScrollRequest { }

	public partial class ClearScrollRequest { }

	[DescriptorFor("ClearScroll")]
	public partial class ClearScrollDescriptor { }
}
