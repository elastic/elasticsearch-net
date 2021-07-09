// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The response from a migrate to data tiers request.
	/// </summary>
	public class MigrateToDataTiersResponse : ResponseBase
	{
		/// <summary>
		/// Whether the request was a dry run.
		/// </summary>
		[DataMember(Name = "dry_run")]
		public bool DryRun { get; internal set; }

		/// <summary>
		/// The ILM policies that were updated.
		/// </summary>
		[DataMember(Name = "migrated_ilm_policies")]
		public IEnumerable<string> MigratedIlmPolicies { get; internal set; }

		/// <summary>
		/// The indices that were migrated to tier preference routing.
		/// </summary>
		[DataMember(Name = "migrated_indices")]
		public IEnumerable<string> MigratedIndices { get; internal set; }

		/// <summary>
		/// Shows the name of the legacy index template that was deleted. This will be missing if no legacy index template was
		/// deleted.
		/// </summary>
		[DataMember(Name = "removed_legacy_template")]
		public string RemovedLegacyTemplate { get; internal set; }
	}
}
