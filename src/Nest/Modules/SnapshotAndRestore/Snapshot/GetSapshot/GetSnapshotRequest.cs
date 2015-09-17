using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetSnapshotRequest : IRequest<GetSnapshotRequestParameters> { }

	public partial class GetSnapshotRequest : RequestBase<GetSnapshotRequestParameters>, IGetSnapshotRequest
	{
	}

	[DescriptorFor("SnapshotGet")]
	public partial class GetSnapshotDescriptor : RequestDescriptorBase<GetSnapshotDescriptor, GetSnapshotRequestParameters>, IGetSnapshotRequest
	{
	}
}
