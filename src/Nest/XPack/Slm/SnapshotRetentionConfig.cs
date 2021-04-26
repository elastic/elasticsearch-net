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
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A snapshot lifecycle retention configuration
	/// </summary>
	[ReadAs(typeof(SnapshotRetentionConfig))]
	[InterfaceDataContract]
	public interface ISnapshotRetentionConfiguration
	{
		/// <summary>
		/// How old a snapshot must be in order to be eligible for deletion.
		/// </summary>
		[DataMember(Name = "expire_after")]
		Time ExpireAfter { get; set; }

		/// <summary>
		/// A minimum number of snapshots to keep, regardless of age.
		/// </summary>
		[DataMember(Name = "min_count")]
		int? MinimumCount { get; set; }

		/// <summary>
		/// The maximum number of snapshots to keep, regardless of age. If the number of snapshots in the repository exceeds this limit, the
		/// policy retains the most recent snapshots and deletes older snapshots.
		/// </summary>
		[DataMember(Name = "max_count")]
		int? MaximumCount { get; set; }
	}

	public class SnapshotRetentionConfig : ISnapshotRetentionConfiguration
	{
		/// <inheritdoc />
		public Time ExpireAfter { get; set; }

		/// <inheritdoc />
		public int? MinimumCount { get; set; }

		/// <inheritdoc />
		public int? MaximumCount { get; set; }
	}

	public class SnapshotRetentionConfigurationDescriptor : DescriptorBase<SnapshotRetentionConfigurationDescriptor, ISnapshotRetentionConfiguration>, ISnapshotRetentionConfiguration
	{
		Time ISnapshotRetentionConfiguration.ExpireAfter { get; set; }

		int? ISnapshotRetentionConfiguration.MinimumCount { get; set; }

		int? ISnapshotRetentionConfiguration.MaximumCount { get; set; }

		/// <inheritdoc cref="ISnapshotRetentionConfiguration.ExpireAfter" />
		public SnapshotRetentionConfigurationDescriptor ExpireAfter(Time time) => Assign(time, (a, v) => a.ExpireAfter = v);

		/// <inheritdoc cref="ISnapshotRetentionConfiguration.MinimumCount" />
		public SnapshotRetentionConfigurationDescriptor MinimumCount(int? count) => Assign(count, (a, v) => a.MinimumCount = v);

		/// <inheritdoc cref="ISnapshotRetentionConfiguration.MaximumCount" />
		public SnapshotRetentionConfigurationDescriptor MaximumCount(int? count) => Assign(count, (a, v) => a.MaximumCount = v);
	}
}
