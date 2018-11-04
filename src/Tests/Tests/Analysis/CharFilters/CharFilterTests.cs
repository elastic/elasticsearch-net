using System;
using Nest;

namespace Tests.Analysis.CharFilters
{
	using FuncTokenizer = Func<string, CharFiltersDescriptor, IPromise<ICharFilters>>;

	public class CharFilterTests
	{
		public class MappingTests : CharFilterAssertionBase<MappingTests>
		{
			public override FuncTokenizer Fluent => (n, cf) => cf.Mapping(n, c => c.Mappings("a=>b"));
			public override ICharFilter Initializer => new MappingCharFilter { Mappings = new[] { "a=>b" } };
			public override object Json => new { mappings = new[] { "a=>b" }, type = "mapping" };
			public override string Name => "mapping";
		}

		public class PatternReplaceTests : CharFilterAssertionBase<PatternReplaceTests>
		{
			public override FuncTokenizer Fluent => (n, cf) => cf.PatternReplace(n, c => c.Pattern("x").Replacement("y"));
			public override ICharFilter Initializer => new PatternReplaceCharFilter { Pattern = "x", Replacement = "y" };
			public override object Json => new { pattern = "x", replacement = "y", type = "pattern_replace" };
			public override string Name => "pr";
		}

		public class IcuNormalizerTests : CharFilterAssertionBase<IcuNormalizerTests>
		{
			public override FuncTokenizer Fluent => (n, cf) => cf
				.IcuNormalization(n, c => c
					.Mode(IcuNormalizationMode.Compose)
					.Name(IcuNormalizationType.CompatibilityCaseFold)
				);

			public override ICharFilter Initializer =>
				new IcuNormalizationCharFilter
				{
					Mode = IcuNormalizationMode.Compose,
					Name = IcuNormalizationType.CompatibilityCaseFold
				};

			public override object Json => new { mode = "compose", name = "nfkc_cf", type = "icu_normalizer" };
			public override string Name => "icunorm";
		}

		public class KuromojiIterationMarkTests : CharFilterAssertionBase<KuromojiIterationMarkTests>
		{
			public override FuncTokenizer Fluent =>
				(n, cf) => cf.KuromojiIterationMark("kmark", c => c.NormalizeKana().NormalizeKanji());

			public override ICharFilter Initializer =>
				new KuromojiIterationMarkCharFilter { NormalizeKana = true, NormalizeKanji = true };

			public override object Json => new
			{
				normalize_kanji = true,
				normalize_kana = true,
				type = "kuromoji_iteration_mark"
			};

			public override string Name => "kmark";
		}

		public class HtmlStripTests : CharFilterAssertionBase<HtmlStripTests>
		{
			public override FuncTokenizer Fluent => (n, cf) => cf.HtmlStrip(n);
			public override ICharFilter Initializer => new HtmlStripCharFilter { };
			public override object Json => new { type = "html_strip" };
			public override string Name => "htmls";
		}
	}
}
