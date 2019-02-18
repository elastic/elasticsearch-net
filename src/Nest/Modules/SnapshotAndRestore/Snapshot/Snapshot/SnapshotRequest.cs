using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("snapshot.create.json")]
	public partial interface ISnapshotRequest
	{
		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		[JsonProperty("include_global_state")]
		bool? IncludeGlobalState { get; set; }

		[JsonProperty("indices")]
		[JsonConverter(typeof(IndicesMultiSyntaxJsonConverter))]
		Indices Indices { get; set; }

		[JsonProperty("partial")]
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
