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

namespace Nest
{
	public interface IShardAllocationSettings
	{
		/// <summary> Enable or disable allocation for specific kinds of shards, defaults to all</summary>
		AllocationEnable? AllocationEnable { get; set; }

		/// <summary>How many concurrent shard recoveries are allowed to happen on a node. Defaults to 2.</summary>
		int? NodeConcurrentRecoveries { get; set; }

		/// <summary>
		/// While the recovery of replicas happens over the network, the recovery of an unassigned primary after node restart uses
		/// data from the local disk. These should be fast so more initial primary recoveries can happen in
		/// parallel on the same node. Defaults to 4.
		/// </summary>
		int? NodeInitialPrimariesRecoveries { get; set; }

		/// <summary>
		/// Allows to perform a check to prevent allocation of multiple instances of
		/// the same shard on a single host, based on host name and host address.
		/// Defaults to false, meaning that no check is performed by default. This setting only
		/// applies if multiple nodes are started on the same machine.
		/// </summary>
		bool? SameShardHost { get; set; }
	}

	public class ShardAllocationSettings : IShardAllocationSettings
	{
		/// <inheritdoc />
		public AllocationEnable? AllocationEnable { get; set; }

		/// <inheritdoc />
		public int? NodeConcurrentRecoveries { get; set; }

		/// <inheritdoc />
		public int? NodeInitialPrimariesRecoveries { get; set; }

		/// <inheritdoc />
		public bool? SameShardHost { get; set; }
	}

	public class ShardAllocationSettingsDescriptor
		: DescriptorBase<ShardAllocationSettingsDescriptor, IShardAllocationSettings>, IShardAllocationSettings
	{
		AllocationEnable? IShardAllocationSettings.AllocationEnable { get; set; }

		int? IShardAllocationSettings.NodeConcurrentRecoveries { get; set; }

		int? IShardAllocationSettings.NodeInitialPrimariesRecoveries { get; set; }

		bool? IShardAllocationSettings.SameShardHost { get; set; }

		/// <inheritdoc />
		public ShardAllocationSettingsDescriptor AllocationEnable(AllocationEnable? enable) => Assign(enable, (a, v) => a.AllocationEnable = v);

		/// <inheritdoc />
		public ShardAllocationSettingsDescriptor NodeConcurrentRecoveries(int? concurrent) => Assign(concurrent, (a, v) => a.NodeConcurrentRecoveries = v);

		/// <inheritdoc />
		public ShardAllocationSettingsDescriptor NodeInitialPrimariesRecoveries(int? initial) =>
			Assign(initial, (a, v) => a.NodeInitialPrimariesRecoveries = v);

		/// <inheritdoc />
		public ShardAllocationSettingsDescriptor SameShardHost(bool? same = true) => Assign(same, (a, v) => a.SameShardHost = v);
	}
}
