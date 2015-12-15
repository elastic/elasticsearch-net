namespace Nest
{
	public interface IMergePolicySettings
	{
		/// <summary>
		/// When expungeDeletes is called, we only merge away a segment if its delete percentage 
		/// is over this threshold. Default is 10. 
		/// </summary>
		int? ExpungeDeletesAllowed { get; set; }

		/// <summary>
		/// Segments smaller than this are "rounded up" to this size, i.e. treated as equal (floor) size for merge selection. 
		/// This is to prevent frequent flushing of tiny segments, thus preventing a long tail in the index. Default is 2mb.
		/// </summary>
		//TODO map special type for sizes (e.g 5gb, 16mb)
		string FloorSegment { get; set; }

		/// <summary>
		/// Maximum number of segments to be merged at a time during "normal" merging. Default is 10.
		/// </summary>
		int? MaxMergeAtOnce { get; set; }

		/// <summary>
		/// Maximum number of segments to be merged at a time, during optimize or expungeDeletes. Default is 30.
		/// </summary>
		int? MaxMergeAtOnceExplicit { get; set; }

		/// <summary>
		/// Maximum sized segment to produce during normal merging (not explicit optimize).
		/// This setting is approximate: the estimate of the merged segment size is made by summing 
		/// sizes of to-be-merged segments (compensating for percent deleted docs). Default is 5gb.
		/// </summary>
		//TODO map special type for sizes (e.g 5gb, 16mb)
		string MaxMergedSegment { get; set; }

		/// <summary>
		/// Sets the allowed number of segments per tier. Smaller values mean more merging but fewer segments. 
		/// Default is 10. Note, this value needs to be >= than the max_merge_at_once otherwise you’ll force too 
		/// many merges to occur.
		/// </summary>
		int? SegmentsPerTier { get; set; }

		/// <summary>
		/// Controls how aggressively merges that reclaim more deletions are favored. Higher values 
		/// favor selecting merges that reclaim deletions. A value of 0.0 means deletions don’t 
		/// impact merge selection. Defaults to 2.0
		/// </summary>
		double? ReclaimDeletesWeight { get; set; }
	}

	public class MergePolicySettings : IMergePolicySettings
	{
		///<inheritdoc/>
		public int? ExpungeDeletesAllowed { get; set; }
		///<inheritdoc/>
		public string FloorSegment { get; set; }
		///<inheritdoc/>
		public int? MaxMergeAtOnce { get; set; }
		///<inheritdoc/>
		public int? MaxMergeAtOnceExplicit { get; set; }
		///<inheritdoc/>
		public string MaxMergedSegment { get; set; }
		///<inheritdoc/>
		public double? ReclaimDeletesWeight { get; set; }
		///<inheritdoc/>
		public int? SegmentsPerTier { get; set; }
	}

	public class MergePolicySettingsDescriptor 
		: DescriptorBase<MergePolicySettingsDescriptor, IMergePolicySettings>, IMergePolicySettings
	{
		int? IMergePolicySettings.ExpungeDeletesAllowed { get; set; }
		string IMergePolicySettings.FloorSegment { get; set; }
		int? IMergePolicySettings.MaxMergeAtOnce { get; set; }
		int? IMergePolicySettings.MaxMergeAtOnceExplicit { get; set; }
		string IMergePolicySettings.MaxMergedSegment { get; set; }
		double? IMergePolicySettings.ReclaimDeletesWeight { get; set; }
		int? IMergePolicySettings.SegmentsPerTier { get; set; }

		///<inheritdoc/>
		public MergePolicySettingsDescriptor ExpungeDeletesAllowed(int? allowed) => 
			Assign(a => a.ExpungeDeletesAllowed = allowed);

		///<inheritdoc/>
		public MergePolicySettingsDescriptor FloorSegment(string floorSegment) => 
			Assign(a => a.FloorSegment = floorSegment);

		///<inheritdoc/>
		public MergePolicySettingsDescriptor MaxMergeAtOnce(int? maxMergeAtOnce) => 
			Assign(a => a.MaxMergeAtOnce = maxMergeAtOnce);

		///<inheritdoc/>
		public MergePolicySettingsDescriptor MaxMergeAtOnceExplicit(int? maxMergeOnceAtOnceExplicit) => 
			Assign(a => a.MaxMergeAtOnceExplicit = maxMergeOnceAtOnceExplicit);

		///<inheritdoc/>
		public MergePolicySettingsDescriptor MaxMergedSegement(string maxMergedSegment) => 
			Assign(a => a.MaxMergedSegment = maxMergedSegment);

		///<inheritdoc/>
		public MergePolicySettingsDescriptor ReclaimDeletesWeight(double? weight) => 
			Assign(a => a.ReclaimDeletesWeight = weight);

		///<inheritdoc/>
		public MergePolicySettingsDescriptor SegmentsPerTier(int? segmentsPerTier) => 
			Assign(a => a.SegmentsPerTier = segmentsPerTier);

	}
}