using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISnapshotStatusRequest : IRequest<SnapshotStatusRequestParameters> { }

	public partial class SnapshotStatusRequest : RequestBase<SnapshotStatusRequestParameters>, ISnapshotStatusRequest
	{
	}

	[DescriptorFor("SnapshotGet")]
	public partial class SnapshotStatusDescriptor : RequestDescriptorBase<SnapshotStatusDescriptor, SnapshotStatusRequestParameters>, ISnapshotStatusRequest
	{
	}
}
