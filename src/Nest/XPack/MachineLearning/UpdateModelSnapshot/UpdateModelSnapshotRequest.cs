// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Update a machine learning model snapshot.
	/// </summary>
	[MapsApi("ml.update_model_snapshot.json")]
	public partial interface IUpdateModelSnapshotRequest
	{
		/// <summary>
		/// An optional description of the model snapshot. For example, "Before black friday".
		/// </summary>
		[DataMember(Name ="description")]
		string Description { get; set; }

		/// <summary>
		/// If true, this snapshot will not be deleted during automatic cleanup of snapshots older than model_snapshot_retention_days.
		/// Note that this snapshot will still be deleted when the job is deleted.
		/// </summary>
		[DataMember(Name ="retain")]
		bool? Retain { get; set; }
	}

	/// <inheritdoc />
	public partial class UpdateModelSnapshotRequest
	{
		/// <inheritdoc />
		public string Description { get; set; }

		/// <inheritdoc />
		public bool? Retain { get; set; }
	}

	/// <inheritdoc />
	public partial class UpdateModelSnapshotDescriptor
	{
		string IUpdateModelSnapshotRequest.Description { get; set; }
		bool? IUpdateModelSnapshotRequest.Retain { get; set; }

		/// <inheritdoc />
		public UpdateModelSnapshotDescriptor Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc />
		public UpdateModelSnapshotDescriptor Retain(bool? retain = true) => Assign(retain, (a, v) => a.Retain = v);
	}
}
