using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.IndexModules.IndexSettings.Settings
{
	[SkipVersion("<=5.2.0", "setting introduced in 5.3.0")]
	public class RoutingPartitionSizeIndexSettingsUsage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
	{
		protected override object ExpectJson => new Dictionary<string, object>
		{
			{FixedIndexSettings.RoutingPartitionSize, 6},
		};

		/**
		 *
		 */
		protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
			.RoutingPartitionSize(6);

		/**
		 */
		protected override Nest.IndexSettings Initializer =>
			new Nest.IndexSettings
			{
				RoutingPartitionSize = 6,
			};
	}
}
