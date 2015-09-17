using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IUpgradeStatusRequest : IRequest<UpgradeStatusRequestParameters>
	{
	}

	public partial class UpgradeStatusRequest : RequestBase<UpgradeStatusRequestParameters>, IUpgradeStatusRequest
	{
	}

	[DescriptorFor("IndicesGetUpgrade")]
	public partial class UpgradeStatusDescriptor
		: RequestDescriptorBase<UpgradeStatusDescriptor, UpgradeStatusRequestParameters>, IUpgradeStatusRequest
	{
	}
}
