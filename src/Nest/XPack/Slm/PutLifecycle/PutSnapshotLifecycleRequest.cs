// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("slm.put_lifecycle")]
	[ReadAs(typeof(PutSnapshotLifecycleRequest))]
	public partial interface IPutSnapshotLifecycleRequest
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

	public partial class PutSnapshotLifecycleRequest
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

	public partial class PutSnapshotLifecycleDescriptor
	{
		ISnapshotLifecycleConfig IPutSnapshotLifecycleRequest.Config { get; set; }
		string IPutSnapshotLifecycleRequest.Name { get; set; }
		string IPutSnapshotLifecycleRequest.Repository { get; set; }
		CronExpression IPutSnapshotLifecycleRequest.Schedule { get; set; }

		ISnapshotRetentionConfiguration IPutSnapshotLifecycleRequest.Retention { get; set; }

		/// <inheritdoc cref="IPutSnapshotLifecycleRequest.Name" />
		public PutSnapshotLifecycleDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="IPutSnapshotLifecycleRequest.Repository" />
		public PutSnapshotLifecycleDescriptor Repository(string repository) => Assign(repository, (a, v) => a.Repository = v);

		/// <inheritdoc cref="IPutSnapshotLifecycleRequest.Schedule" />
		public PutSnapshotLifecycleDescriptor Schedule(CronExpression schedule) => Assign(schedule, (a, v) => a.Schedule = v);

		/// <inheritdoc cref="IPutSnapshotLifecycleRequest.Config" />
		public PutSnapshotLifecycleDescriptor Config(Func<SnapshotLifecycleConfigDescriptor, ISnapshotLifecycleConfig> selector) =>
			Assign(selector, (a, v) => a.Config = v.InvokeOrDefault(new SnapshotLifecycleConfigDescriptor()));

		/// <inheritdoc cref="IPutSnapshotLifecycleRequest.Retention" />
		public PutSnapshotLifecycleDescriptor Retention(Func<SnapshotRetentionConfigurationDescriptor, ISnapshotRetentionConfiguration> selector) =>
			Assign(selector, (a, v) => a.Retention = v.InvokeOrDefault(new SnapshotRetentionConfigurationDescriptor()));
	}
}
