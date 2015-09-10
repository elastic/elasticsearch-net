using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetSnapshotRequest : IRepositorySnapshotPath<GetSnapshotRequestParameters> { }

	public partial class GetSnapshotRequest : RepositorySnapshotPathBase<GetSnapshotRequestParameters>, IGetSnapshotRequest
	{
		public GetSnapshotRequest(string repository, string snapshot) : base(repository, snapshot) { }
	}

	[DescriptorFor("SnapshotGet")]
	public partial class GetSnapshotDescriptor : RepositorySnapshotPathDescriptor<GetSnapshotDescriptor, GetSnapshotRequestParameters>, IGetSnapshotRequest
	{
	}
}
