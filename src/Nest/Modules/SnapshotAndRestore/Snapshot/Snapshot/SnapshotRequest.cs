using Newtonsoft.Json;

namespace Nest
{
	public partial interface ISnapshotRequest 
	{
		[JsonProperty("indices")]
		[JsonConverter(typeof(IndicesMultiSyntaxJsonConverter))]
		Indices Indices { get; set; }

		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		[JsonProperty("include_global_state")]
		bool? IncludeGlobalState { get; set; }

		[JsonProperty("partial")]
		bool? Partial { get; set; }
	}

	public partial class SnapshotRequest 
	{
		public Indices Indices { get; set; }

		public bool? IgnoreUnavailable { get; set; }

		public bool? IncludeGlobalState { get; set; }

		public bool? Partial { get; set; }

	}

	[DescriptorFor("SnapshotCreate")]
	public partial class SnapshotDescriptor 
	{
		Indices ISnapshotRequest.Indices { get; set; }
		bool? ISnapshotRequest.IgnoreUnavailable { get; set; }

		bool? ISnapshotRequest.IncludeGlobalState { get; set; }

		bool? ISnapshotRequest.Partial { get; set; }

		public SnapshotDescriptor Index(IndexName index) => this.Indices(index);

		public SnapshotDescriptor Index<T>() where T : class => this.Indices(typeof(T));

		public SnapshotDescriptor Indices(Indices indices) => Assign(a => a.Indices = indices);

		public SnapshotDescriptor IgnoreUnavailable(bool ignoreUnavailable = true) => Assign(a => a.IgnoreUnavailable = ignoreUnavailable);

		public SnapshotDescriptor IncludeGlobalState(bool includeGlobalState = true) => Assign(a => a.IncludeGlobalState = includeGlobalState);

		public SnapshotDescriptor Partial(bool partial = true) => Assign(a => a.Partial = partial);
	}
}
