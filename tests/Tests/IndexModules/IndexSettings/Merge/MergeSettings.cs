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

using System;
using System.Collections.Generic;
using Nest;

namespace Tests.IndexModules.IndexSettings.Merge
{
	public class IndexMergeSettings
	{
		/**
		 */

		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{ "index.merge.policy.expunge_deletes_allowed", 10 },
				{ "index.merge.policy.floor_segment", "2mb" },
				{ "index.merge.policy.max_merge_at_once", 10 },
				{ "index.merge.policy.max_merge_at_once_explicit", 30 },
				{ "index.merge.policy.max_merged_segment", "5gb" },
				{ "index.merge.policy.segments_per_tier", 10 },
				{ "index.merge.policy.reclaim_deletes_weight", 2.0 },
				{ "index.merge.scheduler.max_thread_count", 1 },
				{ "index.merge.scheduler.auto_throttle", true },
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
			protected override Nest.IndexSettings Initializer =>
				new Nest.IndexSettings
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
