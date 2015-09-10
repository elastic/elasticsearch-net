using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISnapshotStatusRequest : IRepositorySnapshotOptionalPath<SnapshotStatusRequestParameters> { }

	public partial class SnapshotStatusRequest : RepositorySnapshotOptionalPathBase<SnapshotStatusRequestParameters>, ISnapshotStatusRequest
	{
		public SnapshotStatusRequest() : base() {}
		public SnapshotStatusRequest(string repository, params string[] snapshots) : base(repository, snapshots) { }
	}

	[DescriptorFor("SnapshotGet")]
	public partial class SnapshotStatusDescriptor : RepositorySnapshotOptionalPathDescriptor<SnapshotStatusDescriptor, SnapshotStatusRequestParameters>, ISnapshotStatusRequest
	{
	}
}
