// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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

		[DataMember(Name = "metadata")]
		IDictionary<string, object> Metadata { get; set; }
	}

	public partial class SnapshotRequest
	{
		public bool? IgnoreUnavailable { get; set; }

		public bool? IncludeGlobalState { get; set; }
		public Indices Indices { get; set; }

		public bool? Partial { get; set; }

		public IDictionary<string, object> Metadata { get; set; }
	}

	public partial class SnapshotDescriptor
	{
		bool? ISnapshotRequest.IgnoreUnavailable { get; set; }

		bool? ISnapshotRequest.IncludeGlobalState { get; set; }
		Indices ISnapshotRequest.Indices { get; set; }

		bool? ISnapshotRequest.Partial { get; set; }

		IDictionary<string, object> ISnapshotRequest.Metadata { get; set; }

		public SnapshotDescriptor Index(IndexName index) => Indices(index);

		public SnapshotDescriptor Index<T>() where T : class => Indices(typeof(T));

		public SnapshotDescriptor Indices(Indices indices) => Assign(indices, (a, v) => a.Indices = v);

		public SnapshotDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);

		public SnapshotDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(includeGlobalState, (a, v) => a.IncludeGlobalState = v);

		public SnapshotDescriptor Partial(bool? partial = true) => Assign(partial, (a, v) => a.Partial = v);

		public SnapshotDescriptor Metadata(IDictionary<string, object> metadata) => Assign(metadata, (a, v) => a.Metadata = v);

		public SnapshotDescriptor Metadata(Func<FluentDictionary<string, object>, IDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Metadata = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
