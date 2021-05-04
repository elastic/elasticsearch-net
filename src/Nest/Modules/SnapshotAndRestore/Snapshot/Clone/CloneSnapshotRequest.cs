// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
