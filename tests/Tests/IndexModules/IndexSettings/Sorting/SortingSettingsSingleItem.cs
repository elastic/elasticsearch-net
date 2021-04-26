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

namespace Tests.IndexModules.IndexSettings.Sorting
{
	public class SortingSettingsSingleItem
	{
		private class TestClass
		{
			// ReSharper disable once InconsistentNaming
			// ReSharper disable once UnusedMember.Local
			public string field1 { get; set; }
		}

		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{ "index.sort.field", "field1" },
				{ "index.sort.order", "asc" },
				{ "index.sort.mode", "min" },
				{ "index.sort.missing", "_first" },
			};

			protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
				.Sorting<TestClass>(sl => sl
					.Fields("field1")
					.Order(IndexSortOrder.Ascending)
					.Mode(IndexSortMode.Minimum)
					.Missing(IndexSortMissing.First)
				);

			protected override Nest.IndexSettings Initializer =>
				new Nest.IndexSettings
				{
					Sorting = new SortingSettings
					{
						Fields = new[] { "field1" },
						Order = new[] { IndexSortOrder.Ascending },
						Mode = new[] { IndexSortMode.Minimum },
						Missing = new[] { IndexSortMissing.First }
					}
				};
		}
	}

	public class SortingSettingsArray
	{
		private class TestClass
		{
			// ReSharper disable once InconsistentNaming
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public string field1 { get; set; }
			// ReSharper disable once InconsistentNaming
			// ReSharper disable once UnusedMember.Local
			public string field2 { get; set; }
		}

		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{ "index.sort.field", new[] { "field1", "field2" } },
				{ "index.sort.order", new[] { "asc", "desc" } },
				{ "index.sort.mode", new[] { "min", "max" } },
				{ "index.sort.missing", new[] { "_first", "_last" } },
			};

			protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
				.Sorting<TestClass>(sl => sl
					.Fields(f => f.Field(p => p.field1).Field("field2"))
					.Order(IndexSortOrder.Ascending, IndexSortOrder.Descending)
					.Mode(IndexSortMode.Minimum, IndexSortMode.Maximum)
					.Missing(IndexSortMissing.First, IndexSortMissing.Last)
				);

			protected override Nest.IndexSettings Initializer =>
				new Nest.IndexSettings
				{
					Sorting = new SortingSettings
					{
						Fields = new[] { "field1", "field2" },
						Order = new[] { IndexSortOrder.Ascending, IndexSortOrder.Descending },
						Mode = new[] { IndexSortMode.Minimum, IndexSortMode.Maximum },
						Missing = new[] { IndexSortMissing.First, IndexSortMissing.Last }
					}
				};
		}
	}
}
