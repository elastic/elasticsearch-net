// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Revert a specific snapshot for a machine learning job
	/// </summary>
	[MapsApi("ml.revert_model_snapshot.json")]
	public partial interface IRevertModelSnapshotRequest
	{
		/// <summary>
		/// If true, deletes the results in the time period between the latest results and the time of
		/// the reverted snapshot. It also resets the model to accept records for this time period.
		/// The default value is false.
		/// </summary>
		[DataMember(Name ="delete_intervening_results")]
		bool? DeleteInterveningResults { get; set; }
	}

	/// <inheritdoc />
	public partial class RevertModelSnapshotRequest
	{
		/// <inheritdoc />
		public bool? DeleteInterveningResults { get; set; }
	}

	/// <inheritdoc />
	public partial class RevertModelSnapshotDescriptor
	{
		bool? IRevertModelSnapshotRequest.DeleteInterveningResults { get; set; }

		/// <inheritdoc />
		public RevertModelSnapshotDescriptor DeleteInterveningResults(bool? deleteInterveningResults = true) =>
			Assign(deleteInterveningResults, (a, v) => a.DeleteInterveningResults = v);
	}
}
