using System;
using Nest;
using Tests.Framework;

namespace Tests.Analysis.Analyzers
{
	/**
	 */

	public class AnalyzerUsageTests : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
	{
		protected override object ExpectJson => new
		{
			analysis = new
			{
				analyzer = new
				{
					myCustom = new
					{
						type = "custom",
						tokenizer = "ng",
						filter = new[] { "myAscii", "kstem" },
						char_filter = new[] { "stripMe", "patterned" }
					},
					myKeyword = new
					{
						type = "keyword"
					},
					myPattern = new
					{
						type = "pattern",
						pattern = "\\w"
					},
					mySimple = new
					{
						type = "simple"
					},
					myLanguage = new { type = "dutch" },
					mySnow = new
					{
						type = "snowball",
						language = "Dutch"
					},
					myStandard = new
					{
						type = "standard",
						max_token_length = 2
					},
					myStop = new
					{
						type = "stop",
						stopwords_path = "analysis/stopwords.txt"
					},
					myWhiteSpace = new
					{
						type = "whitespace"
					},
					myWhiteSpace2 = new
					{
						type = "whitespace"
					}

				}
			}
		};


		/**
		 * 
		 */
		protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => FluentExample;
		public static Func<IndexSettingsDescriptor, IPromise<IndexSettings>> FluentExample => s => s
			.Analysis(analysis => analysis
				.Analyzers(analyzers => analyzers
					.Custom("myCustom", a => a
						.Filters("myAscii", "kstem")
						.CharFilters("stripMe", "patterned")
						.Tokenizer("ng")
					)
					.Keyword("myKeyword")
					.Pattern("myPattern", a => a.Pattern(@"\w"))
					.Language("myLanguage", a => a.Language(Language.Dutch))
					.Simple("mySimple")
					.Snowball("mySnow", a => a.Language(SnowballLanguage.Dutch))
					.Standard("myStandard", a => a.MaxTokenLength(2))
					.Stop("myStop", a => a.StopwordsPath("analysis/stopwords.txt"))
					.Whitespace("myWhiteSpace")
					.Whitespace("myWhiteSpace2")
				)
			);

		/**
		 */
		protected override IndexSettings Initializer => InitializerExample;
		public static IndexSettings InitializerExample =>
			new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Analyzers = new Nest.Analyzers
					{
							{ "myCustom", new CustomAnalyzer
							{
								CharFilter = new [] { "stripMe", "patterned"},
								Filter = new [] { "myAscii", "kstem"},
								Tokenizer = "ng"
							} },
							{ "myKeyword", new KeywordAnalyzer() },
							{ "myPattern", new PatternAnalyzer { Pattern = @"\w" } },
							{ "myLanguage", new LanguageAnalyzer { Language = Language.Dutch } },
							{ "mySimple", new SimpleAnalyzer() },
							{ "mySnow", new SnowballAnalyzer { Language = SnowballLanguage.Dutch } },
							{ "myStandard", new StandardAnalyzer { MaxTokenLength = 2 } },
							{ "myStop", new StopAnalyzer { StopwordsPath = "analysis/stopwords.txt" } },
							{ "myWhiteSpace", new WhitespaceAnalyzer() },
							{ "myWhiteSpace2", new WhitespaceAnalyzer() }
					}
				}
			};
	}
}