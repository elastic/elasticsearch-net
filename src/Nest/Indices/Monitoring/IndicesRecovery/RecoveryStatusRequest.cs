using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IRecoveryStatusRequest { }

	public partial class RecoveryStatusRequest { }

	[DescriptorFor("IndicesRecovery")]
	public partial class RecoveryStatusDescriptor { }
}
