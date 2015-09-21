using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public partial interface IUpgradeRequest : IRequest<UpgradeRequestParameters> { }

	public partial class UpgradeRequest { }

	[DescriptorFor("IndicesUpgrade")]
	public partial class UpgradeDescriptor { }
}
