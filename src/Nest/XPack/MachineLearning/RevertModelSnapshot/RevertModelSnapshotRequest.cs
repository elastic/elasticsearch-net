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
