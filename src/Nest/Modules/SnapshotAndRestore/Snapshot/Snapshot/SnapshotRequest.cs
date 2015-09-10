using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISnapshotRequest : IRepositorySnapshotPath<SnapshotRequestParameters>
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

	public partial class SnapshotRequest : RepositorySnapshotPathBase<SnapshotRequestParameters>, ISnapshotRequest
	{
		public SnapshotRequest(string repository, string snapshot) : base(repository, snapshot) { }

		public IEnumerable<IndexName> Indices { get; set; }

		public bool? IgnoreUnavailable { get; set; }

		public bool? IncludeGlobalState { get; set; }

		public bool? Partial { get; set; }

	}


	[DescriptorFor("SnapshotCreate")]
	public partial class SnapshotDescriptor
		: RepositorySnapshotPathDescriptor<SnapshotDescriptor, SnapshotRequestParameters>, ISnapshotRequest
	{
		private ISnapshotRequest Self => this;

		IEnumerable<IndexName> ISnapshotRequest.Indices { get; set; }
		bool? ISnapshotRequest.IgnoreUnavailable { get; set; }

		bool? ISnapshotRequest.IncludeGlobalState { get; set; }

		bool? ISnapshotRequest.Partial { get; set; }

		string IRepositorySnapshotPath<SnapshotRequestParameters>.Repository { get; set; }

		string IRepositorySnapshotPath<SnapshotRequestParameters>.Snapshot { get; set; }

		public SnapshotDescriptor Index(string index)
		{
			return this.Indices(index);
		}

		public SnapshotDescriptor Index<T>() where T : class
		{
			return this.Indices(typeof(T));
		}

		public SnapshotDescriptor Indices(params string[] indices)
		{
			this.Self.Indices = indices.Select(s => (IndexName)s);
			return this;
		}

		public SnapshotDescriptor Indices(params Type[] indicesTypes)
		{
			this.Self.Indices = indicesTypes.Select(s => (IndexName)s);
			return this;
		}
		public SnapshotDescriptor IgnoreUnavailable(bool ignoreUnavailable = true)
		{
			this.Self.IgnoreUnavailable = ignoreUnavailable;
			return this;
		}
		public SnapshotDescriptor IncludeGlobalState(bool includeGlobalState = true)
		{
			this.Self.IncludeGlobalState = includeGlobalState;
			return this;
		}
		public SnapshotDescriptor Partial(bool partial = true)
		{
			this.Self.Partial = partial;
			return this;
		}
	}
}
