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
