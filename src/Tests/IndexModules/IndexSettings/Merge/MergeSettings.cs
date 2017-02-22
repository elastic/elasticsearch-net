using System;
using System.Collections.Generic;
using Nest_5_2_0;
using Tests.Framework;

namespace Tests.IndexModules.IndexSettings.Merge
{
	public class IndexMergeSettings
	{
		/**
		 */

		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest_5_2_0.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{"index.merge.policy.expunge_deletes_allowed", 10},
				{"index.merge.policy.floor_segment", "2mb"},
				{"index.merge.policy.max_merge_at_once", 10},
				{"index.merge.policy.max_merge_at_once_explicit", 30},
				{"index.merge.policy.max_merged_segment", "5gb"},
				{"index.merge.policy.segments_per_tier", 10},
				{"index.merge.policy.reclaim_deletes_weight", 2.0},
				{"index.merge.scheduler.max_thread_count", 1},
				{"index.merge.scheduler.auto_throttle", true},
			};
			/**
			 * 
			 */
			protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
				.Merge(merge => merge
					.Policy(p => p
						.ExpungeDeletesAllowed(10)
						.FloorSegment("2mb")
						.MaxMergeAtOnce(10)
						.MaxMergeAtOnceExplicit(30)
						.MaxMergedSegement("5gb")
						.SegmentsPerTier(10)
						.ReclaimDeletesWeight(2.0)
					)
					.Scheduler(schedule => schedule
						.AutoThrottle()
						.MaxThreadCount(1)
					)
				);

			/**
			 */
			protected override Nest_5_2_0.IndexSettings Initializer =>
				new Nest_5_2_0.IndexSettings
				{
					Merge = new MergeSettings
					{
						Policy = new MergePolicySettings
						{
							ExpungeDeletesAllowed = 10,
							FloorSegment = "2mb",
							MaxMergeAtOnce = 10,
							MaxMergeAtOnceExplicit = 30,
							MaxMergedSegment = "5gb",
							SegmentsPerTier = 10,
							ReclaimDeletesWeight = 2.0
						},
						Scheduler = new MergeSchedulerSettings
						{
							AutoThrottle = true,
							MaxThreadCount = 1
						}
					}
				};
		}
	}
}
