using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IUpgradeRequest : IIndicesOptionalPath<UpgradeRequestParameters>
	{
	}

	public partial class UpgradeRequest : IndicesOptionalPathBase<UpgradeRequestParameters>, IUpgradeRequest
	{
	}

	[DescriptorFor("IndicesUpgrade")]
	public partial class UpgradeDescriptor 
		: IndicesOptionalPathDescriptor<UpgradeDescriptor, UpgradeRequestParameters>, IUpgradeRequest
	{
	}
}
