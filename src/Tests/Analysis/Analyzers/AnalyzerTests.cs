using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Property;

namespace Tests
{
	public class AnalyzersTests
	{
		/**
		 */

		public class Usage : GeneralUsageBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
		{
			protected override object ExpectJson => new
			{
				analysis = new
				{
					analyzer = new
					{
						myCustom = new
						{
							type = "typex",
							char_filter = "tokeniza",
							filter = new[] {
								"x",
								"y"
							},
							tokenizer = new[] {
								"a",
								"b"
							}
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
							language = "dutch"
						},
						myStandard = new
						{
							type = "standard",
							max_token_length = 2
						},
						myStop = new
						{
							type = "stop",
							stopwords_path = "somewhere"
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
			protected override Func<IndexSettingsDescriptor, IIndexSettings> Fluent => s => s
				.Analysis(analysis => analysis
					.Analyzers(analyzers => analyzers
						.Custom("myCustom", a => a
							.CustomType("typex")
							.Filters("x", "y")
							.CharFilters("a", "b")
							.Tokenizer("tokeniza")
						)
						.Keyword("myKeyword")
						.Pattern("myPattern", a => a.Pattern(@"\w"))
						.Language("myLanguage", a => a.Language(Language.Dutch))
						.Simple("mySimple")
						.Snowball("mySnow", a => a.Language(SnowballLanguage.Dutch))
						.Standard("myStandard", a => a.MaxTokenLength(2))
						.Stop("myStop", a => a.StopwordsPath("somewhere"))
						.Whitespace("myWhiteSpace")
						.Whitespace("myWhiteSpace2")
					)
				);

			/**
			 */
			protected override IndexSettings Initializer =>
				new IndexSettings
				{
					Analysis = new Analysis
					{
						Analyzers = new Analyzers
						{
							{ "myCustom", new CustomAnalyzer("typex")
							{
								CharFilter = new [] { "a", "b"},
								Filter = new [] { "x", "y"},
								Tokenizer = "tokeniza"
							} },
							{ "myKeyword", new KeywordAnalyzer() },
							{ "myPattern", new PatternAnalyzer { Pattern = @"\w" } },
							{ "myLanguage", new LanguageAnalyzer { Language = Language.Dutch } },
							{ "mySimple", new SimpleAnalyzer() },
							{ "mySnow", new SnowballAnalyzer { Language = SnowballLanguage.Dutch } },
							{ "myStandard", new StandardAnalyzer { MaxTokenLength = 2 } },
							{ "myStop", new StopAnalyzer { StopwordsPath = "somewhere" } },
							{ "myWhiteSpace", new WhitespaceAnalyzer() },
							{ "myWhiteSpace2", new WhitespaceAnalyzer() }
						}
					}
				};
		}
	}
}
