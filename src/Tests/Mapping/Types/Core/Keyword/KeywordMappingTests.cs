using System;
using Nest;

namespace Tests.Mapping.Types.Core.Keyword
{
	public class KeywordTest
	{
		[Keyword(
			Boost = 1.2,
			EagerGlobalOrdinals = true,
			IgnoreAbove = 50,
			IncludeInAll = false,
			Index = false,
			IndexOptions = IndexOptions.Offsets,
			NullValue = "null",
			SearchAnalyzer = "searchanalyzer"
		)]
		public string Full { get; set; }

		[Keyword]
		public string Minimal { get; set; }

        public char Char { get; set; }

        public Guid Guid { get; set; }
	}

	public class KeywordMappingTest : TypeMappingTestBase<KeywordTest>
	{
		protected override bool AutoMap => true;

		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "keyword",
					boost = 1.2,
					eager_global_ordinals = true,
					ignore_above = 50,
					include_in_all = false,
					index = false,
					index_options = "offsets",
					null_value = "null",
					search_analyzer = "searchanalyzer"
				},
				minimal = new
				{
					type = "keyword"
				},
				@char = new
				{
					type = "keyword"
				},
				@guid = new
				{
					type = "keyword"
				}
			}
		};

		protected override Func<PropertiesDescriptor<KeywordTest>, IPromise<IProperties>> FluentProperties => p => p
			.Keyword(t => t
				.Name(o => o.Full)
				.Boost(1.2)
				.EagerGlobalOrdinals()
				.IgnoreAbove(50)
				.IncludeInAll(false)
				.Index(false)
				.IndexOptions(IndexOptions.Offsets)
				.NullValue("null")
				.SearchAnalyzer("searchanalyzer")
			)
			.Keyword(s => s
				.Name(o => o.Minimal)
			);

		protected override object ExpectJsonFluentOnly => new
		{
			properties = new
			{
				full = new
				{
					type = "keyword",
					norms = new
					{
						enabled = true,
						loading = "lazy"
					}
				}
			}
		};
		protected override Func<PropertiesDescriptor<KeywordTest>, IPromise<IProperties>> FluentOnlyProperties => p => p
			.Keyword(s => s
				.Name(o => o.Full)
				.Norms(n => n
					.Enabled()
					.Loading(NormsLoading.Lazy)
				)
			);
	}
}
