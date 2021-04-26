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

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A snapshot lifecycle configuration
	/// </summary>
	[ReadAs(typeof(SnapshotLifecycleConfig))]
	[InterfaceDataContract]
	public interface ISnapshotLifecycleConfig
	{
		[DataMember(Name = "ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		[DataMember(Name = "include_global_state")]
		bool? IncludeGlobalState { get; set; }

		/// <summary>
		/// The indices to snapshot
		/// </summary>
		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(IndicesMultiSyntaxFormatter))]
		Indices Indices { get; set; }
	}

	public class SnapshotLifecycleConfig : ISnapshotLifecycleConfig
	{
		/// <inheritdoc />
		public bool? IgnoreUnavailable { get; set; }

		/// <inheritdoc />
		public bool? IncludeGlobalState { get; set; }

		/// <inheritdoc />
		public Indices Indices { get; set; }
	}

	public class SnapshotLifecycleConfigDescriptor : DescriptorBase<SnapshotLifecycleConfigDescriptor, ISnapshotLifecycleConfig>, ISnapshotLifecycleConfig
	{
		bool? ISnapshotLifecycleConfig.IgnoreUnavailable { get; set; }
		bool? ISnapshotLifecycleConfig.IncludeGlobalState { get; set; }
		Indices ISnapshotLifecycleConfig.Indices { get; set; }

		public SnapshotLifecycleConfigDescriptor Indices(IndexName index) => Indices((Indices)index);

		public SnapshotLifecycleConfigDescriptor Indices<T>() where T : class => Indices(typeof(T));

		public SnapshotLifecycleConfigDescriptor Indices(Indices indices) => Assign(indices, (a, v) => a.Indices = v);

		public SnapshotLifecycleConfigDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);

		public SnapshotLifecycleConfigDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(includeGlobalState, (a, v) => a.IncludeGlobalState = v);
	}
}
