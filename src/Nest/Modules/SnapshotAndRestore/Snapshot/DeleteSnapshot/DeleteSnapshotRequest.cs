using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteSnapshotRequest : IRequest<DeleteSnapshotRequestParameters> { }

	public partial class DeleteSnapshotRequest : RequestBase<DeleteSnapshotRequestParameters>, IDeleteSnapshotRequest
	{
	}

	[DescriptorFor("SnapshotDelete")]
	public partial class DeleteSnapshotDescriptor : RequestDescriptorBase<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters>, IDeleteSnapshotRequest
	{
	}
}
