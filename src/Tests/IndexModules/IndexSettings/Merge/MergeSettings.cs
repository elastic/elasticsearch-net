using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.Aggregations
{
	public class IndexMergeSettings
	{
		/**
		 */

		public class Usage : UsageTestBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
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
			protected override Func<IndexSettingsDescriptor, IIndexSettings> Fluent => s => s
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
			protected override IndexSettings Initializer =>
				new IndexSettings
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
