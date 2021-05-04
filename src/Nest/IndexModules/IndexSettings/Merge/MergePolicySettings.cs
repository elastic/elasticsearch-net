// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
		string MaxMergedSegment { get; set; }

		/// <summary>
		/// Controls how aggressively merges that reclaim more deletions are favored. Higher values
		/// favor selecting merges that reclaim deletions. A value of 0.0 means deletions don’t
		/// impact merge selection. Defaults to 2.0
		/// </summary>
		double? ReclaimDeletesWeight { get; set; }

		/// <summary>
		/// Sets the allowed number of segments per tier. Smaller values mean more merging but fewer segments.
		/// Default is 10. Note, this value needs to be >= than the max_merge_at_once otherwise you’ll force too
		/// many merges to occur.
		/// </summary>
		int? SegmentsPerTier { get; set; }
	}

	public class MergePolicySettings : IMergePolicySettings
	{
		/// <inheritdoc />
		public int? ExpungeDeletesAllowed { get; set; }

		/// <inheritdoc />
		public string FloorSegment { get; set; }

		/// <inheritdoc />
		public int? MaxMergeAtOnce { get; set; }

		/// <inheritdoc />
		public int? MaxMergeAtOnceExplicit { get; set; }

		/// <inheritdoc />
		public string MaxMergedSegment { get; set; }

		/// <inheritdoc />
		public double? ReclaimDeletesWeight { get; set; }

		/// <inheritdoc />
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

		/// <inheritdoc />
		public MergePolicySettingsDescriptor ExpungeDeletesAllowed(int? allowed) =>
			Assign(allowed, (a, v) => a.ExpungeDeletesAllowed = v);

		/// <inheritdoc />
		public MergePolicySettingsDescriptor FloorSegment(string floorSegment) =>
			Assign(floorSegment, (a, v) => a.FloorSegment = v);

		/// <inheritdoc />
		public MergePolicySettingsDescriptor MaxMergeAtOnce(int? maxMergeAtOnce) =>
			Assign(maxMergeAtOnce, (a, v) => a.MaxMergeAtOnce = v);

		/// <inheritdoc />
		public MergePolicySettingsDescriptor MaxMergeAtOnceExplicit(int? maxMergeOnceAtOnceExplicit) =>
			Assign(maxMergeOnceAtOnceExplicit, (a, v) => a.MaxMergeAtOnceExplicit = v);

		/// <inheritdoc />
		public MergePolicySettingsDescriptor MaxMergedSegement(string maxMergedSegment) =>
			Assign(maxMergedSegment, (a, v) => a.MaxMergedSegment = v);

		/// <inheritdoc />
		public MergePolicySettingsDescriptor ReclaimDeletesWeight(double? weight) =>
			Assign(weight, (a, v) => a.ReclaimDeletesWeight = v);

		/// <inheritdoc />
		public MergePolicySettingsDescriptor SegmentsPerTier(int? segmentsPerTier) =>
			Assign(segmentsPerTier, (a, v) => a.SegmentsPerTier = v);
	}
}
