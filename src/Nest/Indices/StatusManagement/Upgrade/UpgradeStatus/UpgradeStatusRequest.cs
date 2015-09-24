using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public partial interface IUpgradeStatusRequest { }

	public partial class UpgradeStatusRequest { }

	[DescriptorFor("IndicesGetUpgrade")]
	public partial class UpgradeStatusDescriptor { }
}
