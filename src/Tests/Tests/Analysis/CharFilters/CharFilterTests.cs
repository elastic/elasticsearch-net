using System;
using Nest;

namespace Tests.Analysis.CharFilters
{
	using FuncTokenizer = Func<string, CharFiltersDescriptor, IPromise<ICharFilters>>;

	public class CharFilterTests
	{
		public class MappingTests : CharFilterAssertionBase<MappingTests>
		{
			protected override string Name => "mapping";
			protected override ICharFilter Initializer => new MappingCharFilter {Mappings = new[] {"a=>b"}};
			protected override FuncTokenizer Fluent => (n, cf) => cf.Mapping("mapped", c => c.Mappings("a=>b"));
			protected override object Json => new { mappings = new[] {"a=>b"}, type = "mapping" };
		}

		public class PatternReplaceTests : CharFilterAssertionBase<PatternReplaceTests>
		{
			protected override string Name => "pr";
			protected override ICharFilter Initializer => new PatternReplaceCharFilter {Pattern = "x", Replacement = "y"};
			protected override FuncTokenizer Fluent => (n, cf) => cf.PatternReplace("patterned", c => c.Pattern("x").Replacement("y"));
			protected override object Json => new {pattern = "x", replacement = "y", type = "pattern_replace"};
		}

		public class IcuNormalizerTests : CharFilterAssertionBase<IcuNormalizerTests>
		{
			protected override string Name => "icunorm";
			protected override ICharFilter Initializer =>
				new IcuNormalizationCharFilter
				{
					Mode = IcuNormalizationMode.Compose,
					Name = IcuNormalizationType.CompatibilityCaseFold
				};

			protected override FuncTokenizer Fluent => (n, cf) => cf
				.IcuNormalization("icun", c => c
					.Mode(IcuNormalizationMode.Compose)
					.Name(IcuNormalizationType.CompatibilityCaseFold)
				);

			protected override object Json => new {mode = "compose", name = "nfkc_cf", type = "icu_normalizer"};

		}

		public class KuromojiIterationMarkTests : CharFilterAssertionBase<KuromojiIterationMarkTests>
		{
			protected override string Name => "kmark";

			protected override ICharFilter Initializer =>
				new KuromojiIterationMarkCharFilter { NormalizeKana = true, NormalizeKanji = true };

			protected override FuncTokenizer Fluent =>
				(n, cf) => cf.KuromojiIterationMark("kmark", c => c.NormalizeKana().NormalizeKanji());

			protected override object Json => new
			{
				normalize_kanji = true,
				normalize_kana = true,
				type = "kuromoji_iteration_mark"
			};
		}

		public class HtmlStripTests : CharFilterAssertionBase<HtmlStripTests>
		{
			protected override string Name => "htmls";
			protected override ICharFilter Initializer => new HtmlStripCharFilter { };
			protected override FuncTokenizer Fluent => (n, cf) => cf.HtmlStrip("stripMe");
			protected override object Json => new {type = "html_strip"};
		}

	}
}
