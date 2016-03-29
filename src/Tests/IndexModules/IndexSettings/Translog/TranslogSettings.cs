using System;
using System.Collections.Generic;
using Nest;
using Tests.Framework;

namespace Tests.IndexModules.IndexSettings.Translog
{
	public class TranlogSettings
	{
		/**
		 */

		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{"index.translog.sync_interval", "5s" },
				{"index.translog.durability", "request" },
				{"index.translog.fs.type", "buffered" },
				{"index.translog.flush_threshold_size", "10mb" },
				{"index.translog.flush_threshold_ops", 2 },
				{"index.translog.flush_threshold_period", "30m" },
				{"index.translog.interval", "5s" },
			};

			/**
			 * 
			 */
			protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
				.Translog(slowlog => slowlog
					.Flush(f => f
						.ThresholdOps(2)
						.ThresholdSize("10mb")
						.ThresholdPeriod(TimeSpan.FromMinutes(30))
						.Interval(TimeSpan.FromSeconds(5))
					)
					.SyncInterval("5s")
					.Durability(TranslogDurability.Request)
					.FileSystemType(TranslogWriteMode.Buffered)
				);

			/**
			 */
			protected override Nest.IndexSettings Initializer =>
				new Nest.IndexSettings
				{
					Translog = new TranslogSettings
					{
						SyncInterval = TimeSpan.FromSeconds(5),
						FileSystemType = TranslogWriteMode.Buffered,
						Durability = TranslogDurability.Request,
						Flush = new TranslogFlushSettings
						{
							ThresholdOps = 2,
							ThresholdPeriod = "30m",
							Interval = TimeSpan.FromSeconds(5),
							ThresholdSize = "10mb"
						}
					}
				};
		}
	}
}
