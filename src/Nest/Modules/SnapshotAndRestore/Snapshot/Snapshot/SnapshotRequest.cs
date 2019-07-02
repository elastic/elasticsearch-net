using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("snapshot.create.json")]
	public partial interface ISnapshotRequest
	{
		[DataMember(Name ="ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		[DataMember(Name ="include_global_state")]
		bool? IncludeGlobalState { get; set; }

		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(IndicesMultiSyntaxFormatter))]
		Indices Indices { get; set; }

		[DataMember(Name ="partial")]
		bool? Partial { get; set; }
	}

	public partial class SnapshotRequest
	{
		public bool? IgnoreUnavailable { get; set; }

		public bool? IncludeGlobalState { get; set; }
		public Indices Indices { get; set; }

		public bool? Partial { get; set; }
	}

	public partial class SnapshotDescriptor
	{
		bool? ISnapshotRequest.IgnoreUnavailable { get; set; }

		bool? ISnapshotRequest.IncludeGlobalState { get; set; }
		Indices ISnapshotRequest.Indices { get; set; }

		bool? ISnapshotRequest.Partial { get; set; }

		public SnapshotDescriptor Index(IndexName index) => Indices(index);

		public SnapshotDescriptor Index<T>() where T : class => Indices(typeof(T));

		public SnapshotDescriptor Indices(Indices indices) => Assign(indices, (a, v) => a.Indices = v);

		public SnapshotDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);

		public SnapshotDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(includeGlobalState, (a, v) => a.IncludeGlobalState = v);

		public SnapshotDescriptor Partial(bool? partial = true) => Assign(partial, (a, v) => a.Partial = v);
	}
}
