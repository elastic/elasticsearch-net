// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.IndexModules.IndexSettings.Settings
{
	[SkipVersion("<=5.2.0", "setting introduced in 5.3.0")]
	public class RoutingPartitionSizeIndexSettingsUsage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
	{
		protected override object ExpectJson => new Dictionary<string, object>
		{
			{ FixedIndexSettings.RoutingPartitionSize, 6 },
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
