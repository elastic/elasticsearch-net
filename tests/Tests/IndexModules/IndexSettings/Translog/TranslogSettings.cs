// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest;

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
				{ "index.translog.sync_interval", "5s" },
				{ "index.translog.durability", "request" },
				{ "index.translog.flush_threshold_size", "10mb" },
				{ "index.translog.flush_threshold_period", "30m" }
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
			protected override Nest.IndexSettings Initializer =>
				new Nest.IndexSettings
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
