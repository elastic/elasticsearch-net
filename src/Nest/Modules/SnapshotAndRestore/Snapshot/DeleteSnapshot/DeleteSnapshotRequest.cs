using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteSnapshotRequest : IRepositorySnapshotPath<DeleteSnapshotRequestParameters> { }

	public partial class DeleteSnapshotRequest : RepositorySnapshotPathBase<DeleteSnapshotRequestParameters>, IDeleteSnapshotRequest
	{
		public DeleteSnapshotRequest(string repository, string snapshot) : base(repository, snapshot) { }
	}
	[DescriptorFor("SnapshotDelete")]
	public partial class DeleteSnapshotDescriptor : RepositorySnapshotPathDescriptor<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters>, IDeleteSnapshotRequest
	{
	}
}
