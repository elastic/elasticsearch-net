using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface ISnapshotRequest 
	{
		[JsonProperty("indices")]
		IEnumerable<IndexName> Indices { get; set; }

		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		[JsonProperty("include_global_state")]
		bool? IncludeGlobalState { get; set; }

		[JsonProperty("partial")]
		bool? Partial { get; set; }
	}

	public partial class SnapshotRequest 
	{
		public IEnumerable<IndexName> Indices { get; set; }

		public bool? IgnoreUnavailable { get; set; }

		public bool? IncludeGlobalState { get; set; }

		public bool? Partial { get; set; }

	}

	[DescriptorFor("SnapshotCreate")]
	public partial class SnapshotDescriptor 
	{
		IEnumerable<IndexName> ISnapshotRequest.Indices { get; set; }
		bool? ISnapshotRequest.IgnoreUnavailable { get; set; }

		bool? ISnapshotRequest.IncludeGlobalState { get; set; }

		bool? ISnapshotRequest.Partial { get; set; }

		// TODO: Replace these
		//string IRepositorySnapshotPath.<SnapshotRequestParameters>.Repository { get; set; }

		//string IRepositorySnapshotPath<SnapshotRequestParameters>.Snapshot { get; set; }

		public SnapshotDescriptor Index(string index)
		{
			return this.Indices(index);
		}

		public SnapshotDescriptor Index<T>() where T : class
		{
			return this.Indices(typeof(T));
		}

		public SnapshotDescriptor Indices(params string[] indices) => Assign(a => a.Indices = indices.Select(s => (IndexName) s));

		public SnapshotDescriptor Indices(params Type[] indices) =>Assign(a => a.Indices = indices.Select(s => (IndexName) s));

		public SnapshotDescriptor IgnoreUnavailable(bool ignoreUnavailable = true) => Assign(a => a.IgnoreUnavailable = ignoreUnavailable);

		public SnapshotDescriptor IncludeGlobalState(bool includeGlobalState = true) => Assign(a => a.IncludeGlobalState = includeGlobalState);

		public SnapshotDescriptor Partial(bool partial = true) => Assign(a => a.Partial = partial);
	}
}
