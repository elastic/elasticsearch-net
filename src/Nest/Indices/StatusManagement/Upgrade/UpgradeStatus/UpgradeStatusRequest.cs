using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IUpgradeStatusRequest : IIndicesOptionalPath<UpgradeStatusRequestParameters>
	{
	}

	public partial class UpgradeStatusRequest : IndicesOptionalPathBase<UpgradeStatusRequestParameters>, IUpgradeStatusRequest
	{
	}

	[DescriptorFor("IndicesGetUpgrade")]
	public partial class UpgradeStatusDescriptor
		: IndicesOptionalPathDescriptor<UpgradeStatusDescriptor, UpgradeStatusRequestParameters>, IUpgradeStatusRequest
	{
	}
}
