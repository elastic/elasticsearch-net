namespace Nest
{
	public interface IDiskBasedShardAllocationSettings
	{
		/// <summary>Defaults to true. Set to false to disable the disk allocation decider.</summary>
		bool? ThresholdEnabled { get; set; }

		/// <summary>Controls the low watermark for disk usage. It defaults to 85%, meaning ES will not allocate new shards to nodes once they have more than 85% disk used. It can also be set 
		/// to an absolute byte value (like 500mb) to prevent ES from allocating shards if less than the configured amount of space is available.</summary>
		string LowWatermark { get; set; }

		/// <summary> 
		/// Controls the high watermark. It defaults to 90%, meaning ES will attempt to relocate shards to another node if the node disk usage rises above 90%. 
		/// It can also be set to an absolute byte value (similar to the low watermark) to relocate shards once less than the
		///  configured amount of space is available on the node.</summary>
		string HighWatermark { get; set; }

		/// <summary>How often Elasticsearch should check on disk usage for each node in the cluster. Defaults to 30s.</summary>
		Time UpdateInterval { get; set; }

		/// <summary>
		/// Defaults to true, which means that Elasticsearch will take into account shards that are currently being relocated to the target node when computing 
		/// a node’s disk usage. Taking relocating shards' sizes into account may, however, mean that the disk usage for a node is incorrectly estimated on the high side,
		/// since the relocation could be 90% complete and a recently retrieved disk usage would include the total size of the 
		/// relocating shard as well as the space already used by the running relocation.
		/// </summary>
		bool? IncludeRelocations { get; set; }
	}

	public class DiskBasedShardAllocationSettings : IDiskBasedShardAllocationSettings
	{
		///<inheritdoc/>
		public bool? ThresholdEnabled { get; set; }

		///<inheritdoc/>
		public string LowWatermark { get; set; }

		///<inheritdoc/>
		public string HighWatermark { get; set; }

		///<inheritdoc/>
		public Time UpdateInterval { get; set; }

		///<inheritdoc/>
		public bool? IncludeRelocations { get; set; }
	}

	public class DiskBasedShardAllocationSettingsDescriptor 
		: DescriptorBase<DiskBasedShardAllocationSettingsDescriptor, IDiskBasedShardAllocationSettings>, IDiskBasedShardAllocationSettings
	{
		bool? IDiskBasedShardAllocationSettings.ThresholdEnabled { get; set; }

		string IDiskBasedShardAllocationSettings.LowWatermark { get; set; }

		string IDiskBasedShardAllocationSettings.HighWatermark { get; set; }

		Time IDiskBasedShardAllocationSettings.UpdateInterval { get; set; }

		bool? IDiskBasedShardAllocationSettings.IncludeRelocations { get; set; }

		///<inheritdoc/>
		public DiskBasedShardAllocationSettingsDescriptor ThresholdEnabled(bool? enable) => Assign(a => a.ThresholdEnabled = enable);

		///<inheritdoc/>
		public DiskBasedShardAllocationSettingsDescriptor LowWatermark(string low) => Assign(a => a.LowWatermark = low);

		///<inheritdoc/>
		public DiskBasedShardAllocationSettingsDescriptor HighWatermark(string high) => Assign(a => a.HighWatermark = high);

		///<inheritdoc/>
		public DiskBasedShardAllocationSettingsDescriptor UpdateInterval(Time time) => Assign(a => a.UpdateInterval = time);

		///<inheritdoc/>
		public DiskBasedShardAllocationSettingsDescriptor IncludeRelocations(bool? include) => Assign(a => a.IncludeRelocations = include);


	}
}