/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
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
