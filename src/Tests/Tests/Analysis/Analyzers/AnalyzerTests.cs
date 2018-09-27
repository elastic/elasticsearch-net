using System;
using Nest;

namespace Tests.Analysis.Analyzers
{
	using FuncTokenizer = Func<string, AnalyzersDescriptor, IPromise<IAnalyzers>>;

	public class AnalyzerTests
	{
		public class KeywordTests : AnalyzerAssertionBase<KeywordTests>
		{
			protected override string Name => "myKeyword ";

			protected override IAnalyzer Initializer =>
				new KeywordAnalyzer();

			protected override FuncTokenizer Fluent => (n, an) => an.Keyword("myKeyword");

			protected override object Json => new
			{
				type = "keyword"
			};

		}

		public class CustomTests : AnalyzerAssertionBase<CustomTests>
		{
			protected override string Name => "myCustom";

			protected override IAnalyzer Initializer => new CustomAnalyzer
			{
				CharFilter = new[] {"stripMe", "patterned"},
				Tokenizer = "ng",
				Filter = new []{"myAscii", "kstem" }
			};


			protected override FuncTokenizer Fluent => (n, an) => an
				.Custom("myCustom", a => a
					.Filters("myAscii", "kstem")
					.CharFilters("stripMe", "patterned")
					.Tokenizer("ng")
				);

			protected override object Json => new
			{
				type = "custom",
				tokenizer = "ng",
				filter = new[] {"myAscii", "kstem"},
				char_filter = new[] {"stripMe", "patterned"}
			};

		}
		public class PatternTests : AnalyzerAssertionBase<PatternTests>
		{
			protected override string Name => "myPattern ";

			protected override IAnalyzer Initializer => new PatternAnalyzer {Pattern = @"\w"};

			protected override FuncTokenizer Fluent => (n, an) => an
				.Pattern("myPattern", a => a.Pattern(@"\w"));

			protected override object Json => new { type = "pattern", pattern = "\\w" };

		}
		public class SimpleTests : AnalyzerAssertionBase<SimpleTests>
		{
			protected override string Name => "mySimple";

			protected override IAnalyzer Initializer => new SimpleAnalyzer();

			protected override FuncTokenizer Fluent => (n, an) => an.Simple("mySimple");
			protected override object Json => new {type = "simple"};

		}
		public class LanguageTests : AnalyzerAssertionBase<SimpleTests>
		{
			protected override string Name => "myLanguage";

			protected override IAnalyzer Initializer => new LanguageAnalyzer {Language = Language.Dutch};

			protected override FuncTokenizer Fluent => (n, an) => an
				.Language("myLanguage", a => a.Language(Language.Dutch));

			protected override object Json => new {type = "dutch"};

		}
		public class SnowballTests : AnalyzerAssertionBase<SnowballTests>
		{
			protected override string Name => "mySnow ";

			protected override IAnalyzer Initializer => new SnowballAnalyzer {Language = SnowballLanguage.Dutch};

			protected override FuncTokenizer Fluent => (n, an) => an
				.Snowball("mySnow", a => a.Language(SnowballLanguage.Dutch));

			protected override object Json => new
			{
				type = "snowball",
				language = "Dutch"
			};

		}
		public class StandardTests : AnalyzerAssertionBase<StandardTests>
		{
			protected override string Name => "myStandard";

			protected override IAnalyzer Initializer => new StandardAnalyzer {MaxTokenLength = 2};

			protected override FuncTokenizer Fluent => (n, an) => an
				.Standard("myStandard", a => a.MaxTokenLength(2));

			protected override object Json => new
			{
				type = "standard",
				max_token_length = 2
			};

		}
		public class StopTests : AnalyzerAssertionBase<StopTests>
		{
			protected override string Name => "myStop ";

			protected override IAnalyzer Initializer => new StopAnalyzer {StopwordsPath = "analysis/stopwords.txt"};

			protected override FuncTokenizer Fluent => (n, an) => an
				.Stop("myStop", a => a.StopwordsPath("analysis/stopwords.txt"));

			protected override object Json => new
			{
				type = "stop",
				stopwords_path = "analysis/stopwords.txt"
			};

		}
		public class WhitespaceTests : AnalyzerAssertionBase<WhitespaceTests>
		{
			protected override string Name => "myWhiteSpace ";

			protected override IAnalyzer Initializer => new WhitespaceAnalyzer();

			protected override FuncTokenizer Fluent => (n, an) => an.Whitespace("myWhiteSpace");
			protected override object Json => new {type = "whitespace"};

		}

		public class FingerprintTests : AnalyzerAssertionBase<FingerprintTests>
		{
			protected override string Name => "myFingerprint";

			protected override IAnalyzer Initializer =>
				new FingerprintAnalyzer
				{
					PreserveOriginal = true,
					Separator = ",",
					MaxOutputSize = 100,
					StopWords = new[] {"a", "he", "the"}
				};

			protected override FuncTokenizer Fluent => (n, an) => an
				.Fingerprint("myFingerprint", a => a
					.PreserveOriginal()
					.Separator(",")
					.MaxOutputSize(100)
					.StopWords("a", "he", "the")
				);

			protected override object Json => new
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
			protected override string Name => "kuro ";

			protected override IAnalyzer Initializer =>
				new KuromojiAnalyzer
				{
					Mode = KuromojiTokenizationMode.Search
				};

			protected override FuncTokenizer Fluent => (n, an) => an
				.Kuromoji("kuro", a => a
					.Mode(KuromojiTokenizationMode.Search)
				);

			protected override object Json => new
			{
				type = "kuromoji",
				mode = "search"
			};
		}

	}
}
