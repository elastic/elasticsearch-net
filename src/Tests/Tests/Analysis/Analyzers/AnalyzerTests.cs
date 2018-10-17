using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Analysis.TokenFilters;

namespace Tests.Analysis.Analyzers
{
	using FuncTokenizer = Func<string, AnalyzersDescriptor, IPromise<IAnalyzers>>;

	public class AnalyzerTests
	{
		public class KeywordTests : AnalyzerAssertionBase<KeywordTests>
		{
			public override string Name => "myKeyword";

			public override IAnalyzer Initializer =>
				new KeywordAnalyzer();

			public override FuncTokenizer Fluent => (n, an) => an.Keyword("myKeyword");

			public override object Json => new
			{
				type = "keyword"
			};

		}

		public class CustomTests : AnalyzerAssertionBase<CustomTests>
		{
			public override string Name => "myCustom";

			public override IAnalyzer Initializer => new CustomAnalyzer
			{
				CharFilter = new[] {"html_strip"},
				Tokenizer = "standard",
				Filter = new []{"lowercase", "asciifolding" }
			};

			public override FuncTokenizer Fluent => (n, an) => an
				.Custom("myCustom", a => a
					.Filters("lowercase", "asciifolding")
					.CharFilters("html_strip")
					.Tokenizer("standard")
				);

			public override object Json => new
			{
				type = "custom",
				tokenizer = "standard",
				filter = new[] {"lowercase", "asciifolding"},
				char_filter = new[] {"html_strip"}
			};

		}
		public class PatternTests : AnalyzerAssertionBase<PatternTests>
		{
			public override string Name => "myPattern ";

			public override IAnalyzer Initializer => new PatternAnalyzer {Pattern = @"\w"};

			public override FuncTokenizer Fluent => (n, an) => an.Pattern(n, a => a.Pattern(@"\w"));

			public override object Json => new { type = "pattern", pattern = "\\w" };

		}
		public class SimpleTests : AnalyzerAssertionBase<SimpleTests>
		{
			public override string Name => "mySimple";

			public override IAnalyzer Initializer => new SimpleAnalyzer();

			public override FuncTokenizer Fluent => (n, an) => an.Simple("mySimple");
			public override object Json => new {type = "simple"};

		}
		public class LanguageTests : AnalyzerAssertionBase<SimpleTests>
		{
			public override string Name => "myLanguage";

			public override IAnalyzer Initializer => new LanguageAnalyzer {Language = Language.Dutch};

			public override FuncTokenizer Fluent => (n, an) => an
				.Language("myLanguage", a => a.Language(Language.Dutch));

			public override object Json => new {type = "dutch"};

		}
		public class SnowballTests : AnalyzerAssertionBase<SnowballTests>
		{
			public override string Name => "mySnow";

			public override IAnalyzer Initializer => new SnowballAnalyzer {Language = SnowballLanguage.Dutch};

			public override FuncTokenizer Fluent => (n, an) => an
				.Snowball("mySnow", a => a.Language(SnowballLanguage.Dutch));

			public override object Json => new
			{
				type = "snowball",
				language = "Dutch"
			};

		}
		public class StandardTests : AnalyzerAssertionBase<StandardTests>
		{
			public override string Name => "myStandard";

			public override IAnalyzer Initializer => new StandardAnalyzer {MaxTokenLength = 2};

			public override FuncTokenizer Fluent => (n, an) => an
				.Standard("myStandard", a => a.MaxTokenLength(2));

			public override object Json => new
			{
				type = "standard",
				max_token_length = 2
			};

		}
		public class StopTests : AnalyzerAssertionBase<StopTests>
		{
			public override string Name => "myStop";

			public override IAnalyzer Initializer => new StopAnalyzer {StopwordsPath = "analysis/stopwords.txt"};

			public override FuncTokenizer Fluent => (n, an) => an
				.Stop("myStop", a => a.StopwordsPath("analysis/stopwords.txt"));

			public override object Json => new
			{
				type = "stop",
				stopwords_path = "analysis/stopwords.txt"
			};

		}
		public class WhitespaceTests : AnalyzerAssertionBase<WhitespaceTests>
		{
			public override string Name => "myWhiteSpace";

			public override IAnalyzer Initializer => new WhitespaceAnalyzer();

			public override FuncTokenizer Fluent => (n, an) => an.Whitespace(n);
			public override object Json => new {type = "whitespace"};

		}

		public class FingerprintTests : AnalyzerAssertionBase<FingerprintTests>
		{
			public override string Name => "myFingerprint";

			public override IAnalyzer Initializer =>
				new FingerprintAnalyzer
				{
					PreserveOriginal = true,
					Separator = ",",
					MaxOutputSize = 100,
					StopWords = new[] {"a", "he", "the"}
				};

			public override FuncTokenizer Fluent => (n, an) => an
				.Fingerprint("myFingerprint", a => a
					.PreserveOriginal()
					.Separator(",")
					.MaxOutputSize(100)
					.StopWords("a", "he", "the")
				);

			public override object Json => new
			{
				type = "fingerprint",
				preserve_original = true,
				separator = ",",
				max_output_size = 100,
				stopwords = new[] {"a", "he", "the"}
			};

		}


		public class KuromojuTests : AnalyzerAssertionBase<KuromojuTests>
		{
			public override string Name => "kuro";

			public override IAnalyzer Initializer =>
				new KuromojiAnalyzer
				{
					Mode = KuromojiTokenizationMode.Search
				};

			public override FuncTokenizer Fluent => (n, an) => an
				.Kuromoji("kuro", a => a
					.Mode(KuromojiTokenizationMode.Search)
				);

			public override object Json => new
			{
				type = "kuromoji",
				mode = "search"
			};
		}

		[SkipVersion("<6.4.0", "analysis-nori plugin introduced in 6.4.0")]
		public class NoriTests : AnalyzerAssertionBase<NoriTests>
		{
			public override string Name => "nori";
			private readonly string[] _stopTags = {"NR", "SP"};
			public override IAnalyzer Initializer => new NoriAnalyzer
			{
				StopTags = _stopTags,
				DecompoundMode = NoriDecompoundMode.Mixed
			};

			public override FuncTokenizer Fluent => (n, t) => t.Nori(n, e => e
				.StopTags(_stopTags)
				.DecompoundMode(NoriDecompoundMode.Mixed)
			);

			public override object Json => new
			{
				type = "nori",
				decompound_mode = "mixed",
				stoptags =_stopTags
			};
		}
	}
}
