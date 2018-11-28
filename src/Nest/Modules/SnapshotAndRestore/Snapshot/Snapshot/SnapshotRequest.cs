using System.Runtime.Serialization;
using Utf8Json;

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

		public SnapshotDescriptor Indices(Indices indices) => Assign(a => a.Indices = indices);

		public SnapshotDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(a => a.IgnoreUnavailable = ignoreUnavailable);

		public SnapshotDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(a => a.IncludeGlobalState = includeGlobalState);

		public SnapshotDescriptor Partial(bool? partial = true) => Assign(a => a.Partial = partial);
	}
}
