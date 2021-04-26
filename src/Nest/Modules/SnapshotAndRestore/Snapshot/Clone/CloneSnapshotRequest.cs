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

namespace Nest
{
	[MapsApi("snapshot.clone.json")]
	[ReadAs(typeof(CloneSnapshotRequest))]
	public partial interface ICloneSnapshotRequest
	{
		/// <summary>
		/// The indices to clone.
		/// </summary>
		[DataMember(Name = "indices")]
		Indices Indices { get; set; }
	}

	public partial class CloneSnapshotRequest
	{
		/// <inheritdoc />
		public Indices Indices { get; set; }
	}

	public partial class CloneSnapshotDescriptor
	{
		Indices ICloneSnapshotRequest.Indices { get; set; }

		/// <inheritdoc cref="IRestoreRequest.Indices" />
		public CloneSnapshotDescriptor Index(IndexName index) => Indices(index);

		/// <inheritdoc cref="IRestoreRequest.Indices" />
		public CloneSnapshotDescriptor Index<T>() where T : class => Indices(typeof(T));

		/// <inheritdoc cref="IRestoreRequest.Indices" />
		public CloneSnapshotDescriptor Indices(Indices indices) => Assign(indices, (a, v) => a.Indices = v);
	}
}
