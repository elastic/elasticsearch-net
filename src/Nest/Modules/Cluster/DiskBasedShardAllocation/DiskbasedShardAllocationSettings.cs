// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface IDiskBasedShardAllocationSettings
	{
		/// <summary>
		/// Controls the high watermark. It defaults to 90%, meaning ES will attempt to relocate shards to another node if the node disk usage rises
		/// above 90%.
		/// It can also be set to an absolute byte value (similar to the low watermark) to relocate shards once less than the
		///  configured amount of space is available on the node.
		/// </summary>
		string HighWatermark { get; set; }

		/// <summary>
		/// Defaults to true, which means that Elasticsearch will take into account shards that are currently being relocated to the target node when
		/// computing
		/// a node’s disk usage. Taking relocating shards' sizes into account may, however, mean that the disk usage for a node is incorrectly
		/// estimated on the high side,
		/// since the relocation could be 90% complete and a recently retrieved disk usage would include the total size of the
		/// relocating shard as well as the space already used by the running relocation.
		/// </summary>
		bool? IncludeRelocations { get; set; }

		/// <summary>
		/// Controls the low watermark for disk usage. It defaults to 85%, meaning ES will not allocate new shards to nodes once they have more than
		/// 85% disk used. It can also be set
		/// to an absolute byte value (like 500mb) to prevent ES from allocating shards if less than the configured amount of space is available.
		/// </summary>
		string LowWatermark { get; set; }

		/// <summary>Defaults to true. Set to false to disable the disk allocation decider.</summary>
		bool? ThresholdEnabled { get; set; }

		/// <summary>How often Elasticsearch should check on disk usage for each node in the cluster. Defaults to 30s.</summary>
		Time UpdateInterval { get; set; }
	}

	public class DiskBasedShardAllocationSettings : IDiskBasedShardAllocationSettings
	{
		/// <inheritdoc />
		public string HighWatermark { get; set; }

		/// <inheritdoc />
		public bool? IncludeRelocations { get; set; }

		/// <inheritdoc />
		public string LowWatermark { get; set; }

		/// <inheritdoc />
		public bool? ThresholdEnabled { get; set; }

		/// <inheritdoc />
		public Time UpdateInterval { get; set; }
	}

	public class DiskBasedShardAllocationSettingsDescriptor
		: DescriptorBase<DiskBasedShardAllocationSettingsDescriptor, IDiskBasedShardAllocationSettings>, IDiskBasedShardAllocationSettings
	{
		string IDiskBasedShardAllocationSettings.HighWatermark { get; set; }

		bool? IDiskBasedShardAllocationSettings.IncludeRelocations { get; set; }

		string IDiskBasedShardAllocationSettings.LowWatermark { get; set; }
		bool? IDiskBasedShardAllocationSettings.ThresholdEnabled { get; set; }

		Time IDiskBasedShardAllocationSettings.UpdateInterval { get; set; }

		/// <inheritdoc />
		public DiskBasedShardAllocationSettingsDescriptor ThresholdEnabled(bool? enable = true) => Assign(enable, (a, v) => a.ThresholdEnabled = v);

		/// <inheritdoc />
		public DiskBasedShardAllocationSettingsDescriptor LowWatermark(string low) => Assign(low, (a, v) => a.LowWatermark = v);

		/// <inheritdoc />
		public DiskBasedShardAllocationSettingsDescriptor HighWatermark(string high) => Assign(high, (a, v) => a.HighWatermark = v);

		/// <inheritdoc />
		public DiskBasedShardAllocationSettingsDescriptor UpdateInterval(Time time) => Assign(time, (a, v) => a.UpdateInterval = v);

		/// <inheritdoc />
		public DiskBasedShardAllocationSettingsDescriptor IncludeRelocations(bool? include = true) => Assign(include, (a, v) => a.IncludeRelocations = v);
	}
}
