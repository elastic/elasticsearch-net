using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetIndexSettingsRequest { }

	public partial class GetIndexSettingsRequest { }

	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor { }
}
