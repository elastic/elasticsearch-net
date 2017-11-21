using System;
using System.Collections.Generic;
using Nest;
using Tests.Framework;

namespace Tests.IndexModules.IndexSettings.Sorting
{
	public class SortingSettingsSingleItem
	{
		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{"index.sort.field", "field1" },
				{"index.sort.order", "asc" },
				{"index.sort.mode", "min" },
				{"index.sort.missing", "_first" },
			};

			protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
				.Sorting(sl => sl
					.Fields("field1")
					.Order(IndexSortOrder.Ascending)
					.Mode(IndexSortMode.Minimum)
					.Missing(IndexSortMissing.First)
				);

			protected override Nest.IndexSettings Initializer =>
				new Nest.IndexSettings
				{
					Sorting = new Nest.SortingSettings
					{
						Fields = new[] { "field1" },
						Order = new [] { IndexSortOrder.Ascending },
						Mode = new [] { IndexSortMode.Minimum },
						Missing = new [] { IndexSortMissing.First }
					}
				};
		}
	}

	public class SortingSettingsArray
	{
		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{"index.sort.field", new[] { "field1", "field2" } },
				{"index.sort.order", new[] { "asc", "desc" } },
				{"index.sort.mode", new[] { "min", "max" } },
				{"index.sort.missing", new[] { "_first", "_last" } },
			};

			protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
				.Sorting(sl => sl
					.Fields("field1", "field2")
					.Order(IndexSortOrder.Ascending, IndexSortOrder.Descending)
					.Mode(IndexSortMode.Minimum, IndexSortMode.Maximum)
					.Missing(IndexSortMissing.First,IndexSortMissing.Last)
				);

			protected override Nest.IndexSettings Initializer =>
				new Nest.IndexSettings
				{
					Sorting = new Nest.SortingSettings
					{
						Fields = new[] { "field1", "field2" },
						Order = new [] { IndexSortOrder.Ascending, IndexSortOrder.Descending },
						Mode = new [] { IndexSortMode.Minimum, IndexSortMode.Maximum },
						Missing = new [] { IndexSortMissing.First, IndexSortMissing.Last }
					}
				};
		}
	}
}
