using System;
using System.Collections.Generic;
using Nest_5_2_0;
using Tests.Framework;

namespace Tests.IndexModules.IndexSettings.Translog
{
	public class TranlogSettings
	{
		/**
		 */

		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest_5_2_0.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{"index.translog.sync_interval", "5s" },
				{"index.translog.durability", "request" },
				{"index.translog.flush_threshold_size", "10mb" },
				{"index.translog.flush_threshold_period", "30m" }
			};

			/**
			 *
			 */
			protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
				.Translog(slowlog => slowlog
					.Flush(f => f
						.ThresholdSize("10mb")
						.ThresholdPeriod(TimeSpan.FromMinutes(30))
						.Interval(TimeSpan.FromSeconds(5))
					)
					.SyncInterval("5s")
					.Durability(TranslogDurability.Request)
				);

			/**
			 */
			protected override Nest_5_2_0.IndexSettings Initializer =>
				new Nest_5_2_0.IndexSettings
				{
					Translog = new TranslogSettings
					{
						SyncInterval = TimeSpan.FromSeconds(5),
						Durability = TranslogDurability.Request,
						Flush = new TranslogFlushSettings
						{
							ThresholdPeriod = "30m",
							Interval = TimeSpan.FromSeconds(5),
							ThresholdSize = "10mb"
						}
					}
				};
		}
	}
}
