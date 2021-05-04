// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A snapshot lifecycle policy
	/// </summary>
	public class SnapshotLifecyclePolicy
		: ISnapshotLifecyclePolicy
	{
		/// <inheritdoc />
		public ISnapshotLifecycleConfig Config { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public string Repository { get; set; }

		/// <inheritdoc />
		public CronExpression Schedule { get; set; }

		/// <inheritdoc />
		public ISnapshotRetentionConfiguration Retention { get; set; }
	}

	/// <inheritdoc />
	[InterfaceDataContract]
	[ReadAs(typeof(SnapshotLifecyclePolicy))]
	public interface ISnapshotLifecyclePolicy
	{
		/// <summary>
		/// Configuration for each snapshot that will be created by this policy.
		/// </summary>
		[DataMember(Name = "config")]
		ISnapshotLifecycleConfig Config { get; set; }

		/// <summary>
		/// A name automatically given to each snapshot performed by this policy.
		/// Supports the same date math supported in index names. A UUID is automatically appended to the
		/// end of the name to prevent conflicting snapshot names.
		/// </summary>
		[DataMember(Name = "name")]
		string Name { get; set; }

		/// <summary>
		/// The snapshot repository that will contain snapshots created by this policy.
		/// The repository must exist prior to the policy’s creation and can be created
		/// with the Snapshot CreateRespository API.
		/// </summary>
		[DataMember(Name = "repository")]
		string Repository { get; set; }

		/// <summary>
		/// A periodic or absolute time schedule specified as a cron expression.
		/// </summary>
		[DataMember(Name = "schedule")]
		CronExpression Schedule { get; set; }

		/// <summary>
		/// Automatic deletion of older snapshots is an optional feature of snapshot lifecycle management. Retention is run as a cluster level task
		/// that is not associated with a particular policy’s schedule (though the configuration of which snapshots to keep is done on a
		/// per-policy basis).
		/// </summary>
		[DataMember(Name = "retention")]
		ISnapshotRetentionConfiguration Retention { get; set; }
	}
}
