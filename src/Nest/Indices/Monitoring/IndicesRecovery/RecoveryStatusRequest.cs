using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRecoveryStatusRequest : IIndicesOptionalPath<RecoveryStatusRequestParameters> { }

	public partial class RecoveryStatusRequest : IndicesOptionalPathBase<RecoveryStatusRequestParameters>, IRecoveryStatusRequest
	{
	}

	[DescriptorFor("IndicesRecovery")]
	public partial class RecoveryStatusDescriptor : IndicesOptionalPathDescriptor<RecoveryStatusDescriptor, RecoveryStatusRequestParameters>, IRecoveryStatusRequest
	{
	}
}
