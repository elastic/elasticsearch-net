using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRecoveryStatusRequest : IRequest<RecoveryStatusRequestParameters> { }

	public partial class RecoveryStatusRequest : RequestBase<RecoveryStatusRequestParameters>, IRecoveryStatusRequest
	{
	}

	[DescriptorFor("IndicesRecovery")]
	public partial class RecoveryStatusDescriptor : RequestDescriptorBase<RecoveryStatusDescriptor, RecoveryStatusRequestParameters>, IRecoveryStatusRequest
	{
	}
}
