using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IUpgradeRequest : IRequest<UpgradeRequestParameters>
	{
	}

	public partial class UpgradeRequest : RequestBase<UpgradeRequestParameters>, IUpgradeRequest
	{
	}

	[DescriptorFor("IndicesUpgrade")]
	public partial class UpgradeDescriptor 
		: RequestDescriptorBase<UpgradeDescriptor, UpgradeRequestParameters>, IUpgradeRequest
	{
	}
}
