using System;
using Nest;
using Tests.Framework;

namespace Tests.Analysis.TokenFilters
{
	/**
	 */

	public class TokenFilterUsageTests : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
	{
		protected override object ExpectJson => new
		{
			analysis = new
			{
				filter = new
				{
					myAscii = new
					{
						type = "asciifolding",
						preserveOriginal = true
					},
					myCommonGrams = new
					{
						type = "common_grams",
						common_words = new[] { "x", "y", "z" },
						ignore_case = true,
						query_mode = true
					},
					mydp = new
					{
						type = "delimited_payload_filter",
						delimiter = "-",
						encoding = "identity"
					},
					dcc = new
					{
						type = "dictionary_decompounder",
						word_list = new[] { "x", "y", "z" },
						min_word_size = 2,
						min_subword_size = 2,
						max_subword_size = 2,
						only_longest_match = true
					},
					etf = new
					{
						type = "edge_ngram",
						min_gram = 1,
						max_gram = 2
					},
					elision = new
					{
						type = "elision",
						articles = new[] { "a", "b", "c" }
					},
					hunspell = new
					{
						type = "hunspell",
						ignore_case = true,
						locale = "en_US",
						dictionary = "path_to_dict",
						dedup = true,
						longest_only = true
					},
					hypdecomp = new
					{
						type = "hyphenation_decompounder",
						word_list = new[] { "x", "y", "z" },
						min_word_size = 2,
						min_subword_size = 2,
						max_subword_size = 2,
						only_longest_match = true,
						hyphenation_patterns_path = "analysis/fop.xml"
					},
					keeptypes = new
					{
						type = "keep_types",
						types = new[] { "<NUM>", "<SOMETHINGELSE>" }
					},
					keepwords = new
					{
						type = "keep",
						keep_words = new[] { "a", "b", "c" },
						keep_words_case = true
					},
					marker = new
					{
						type = "keyword_marker",
						keywords = new[] { "a", "b" },
						ignore_case = true
					},
					kstem = new
					{
						type = "kstem"
					},
					length = new
					{
						type = "length",
						min = 10,
						max = 200
					},
					limit = new
					{
						type = "limit",
						max_token_count = 12,
						consume_all_tokens = true
					},
					lc = new
					{
						type = "lowercase"
					},
					ngram = new
					{
						type = "ngram",
						min_gram = 3,
						max_gram = 30
					},
					pc = new
					{
						type = "pattern_capture",
						patterns = new[] { "\\d", "\\w" },
						preserve_original = true
					},
					pr = new
					{
						type = "pattern_replace",
						pattern = "(\\d|\\w)",
						replacement = "replacement"
					},
					porter = new
					{
						type = "porter_stem"
					},
					rev = new
					{
						type = "reverse"
					},
					shing = new
					{
						type = "shingle",
						min_shingle_size = 8,
						max_shingle_size = 12,
						output_unigrams = true,
						output_unigrams_if_no_shingles = true,
						token_separator = "|",
						filler_token = "x"
					},
					snow = new
					{
						type = "snowball",
						language = "Dutch"
					},
					standard = new
					{
						type = "standard"
					},
					stem = new
					{
						type = "stemmer",
						language = "arabic"
					},
					stemo = new
					{
						type = "stemmer_override",
						rules_path = "analysis/custom_stems.txt"
					},
					stop = new
					{
						type = "stop",
						stopwords = new[] { "x", "y", "z" },
						ignore_case = true,
						remove_trailing = true
					},
					syn = new
					{
						type = "synonym",
						synonyms_path = "analysis/stopwords.txt",
						format = "wordnet",
						synonyms = new[] { "x=>y", "z=>s" },
						ignore_case = true,
						expand = true,
						tokenizer = "whitespace"
					},
					trimmer = new
					{
						type = "trim"
					},
					truncer = new
					{
						type = "truncate",
						length = 100
					},
					uq = new
					{
						type = "unique",
						only_on_same_position = true
					},
					upper = new
					{
						type = "uppercase"
					},
					wd = new
					{
						type = "word_delimiter",
						generate_word_parts = true,
						generate_number_parts = true,
						catenate_words = true,
						catenate_numbers = true,
						catenate_all = true,
						split_on_case_change = true,
						preserve_original = true,
						split_on_numerics = true,
						stem_english_possessive = true,
						protected_words = new[] { "x", "y", "z" }
					}
				}
			}
		};

		/**
		 * 
		 */
		protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => FluentExample;
		public static Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> FluentExample => s => s
			.Analysis(analysis => analysis
				.TokenFilters(tf => tf
					.AsciiFolding("myAscii", t => t.PreserveOriginal())
					.CommonGrams("myCommonGrams", t => t
						.CommonWords("x", "y", "z")
						.IgnoreCase()
						.QueryMode()
					)
					.DelimitedPayload("mydp", t => t
						.Delimiter('-')
						.Encoding(DelimitedPayloadEncoding.Identity)
					)
					.DictionaryDecompounder("dcc", t => t
						.MaxSubwordSize(2)
						.MinSubwordSize(2)
						.MinWordSize(2)
						.OnlyLongestMatch()
						.WordList("x", "y", "z")
					)
					.EdgeNGram("etf", t => t
						.MaxGram(2)
						.MinGram(1)
					)
					.Elision("elision", t => t
						.Articles("a", "b", "c")
					)
					.Hunspell("hunspell", t => t
						.Dedup()
						.Dictionary("path_to_dict")
						.IgnoreCase()
						.Locale("en_US")
						.LongestOnly()
					)
					.HyphenationDecompounder("hypdecomp", t => t
						.MaxSubwordSize(2)
						.MinSubwordSize(2)
						.MinWordSize(2)
						.OnlyLongestMatch()
						.WordList("x", "y", "z")
						.HyphenationPatternsPath("analysis/fop.xml")
					)
					.KeepTypes("keeptypes", t => t
						.Types("<NUM>", "<SOMETHINGELSE>")
					)
					.KeepWords("keepwords", t => t
						.KeepWords("a", "b", "c")
						.KeepWordsCase()
					)
					.KeywordMarker("marker", t => t
						.IgnoreCase()
						.Keywords("a", "b")
					)
					.KStem("kstem")
					.Length("length", t => t
						.Max(200)
						.Min(10)
					)
					.LimitTokenCount("limit", t => t
						.ConsumeAllToken()
						.MaxTokenCount(12)
					)
					.Lowercase("lc")
					.NGram("ngram", t => t
						.MinGram(3)
						.MaxGram(30)
					)
					.PatternCapture("pc", t => t
						.Patterns(@"\d", @"\w")
						.PreserveOriginal()
					)
					.PatternReplace("pr", t => t
						.Pattern(@"(\d|\w)")
						.Replacement("replacement")
					)
					.PorterStem("porter")
					.Reverse("rev")
					.Shingle("shing", t => t
						.FillerToken("x")
						.MaxShingleSize(12)
						.MinShingleSize(8)
						.OutputUnigrams()
						.OutputUnigramsIfNoShingles()
						.TokenSeparator("|")
					)
					.Snowball("snow", t => t.Language(SnowballLanguage.Dutch))
					.Standard("standard")
					.Stemmer("stem", t => t.Language("arabic"))
					.StemmerOverride("stemo", t => t.RulesPath("analysis/custom_stems.txt"))
					.Stop("stop", t => t
						.IgnoreCase()
						.RemoveTrailing()
						.StopWords("x", "y", "z")
					)
					.Synonym("syn", t => t
						.Expand()
						.Format(SynonymFormat.WordNet)
						.IgnoreCase()
						.SynonymsPath("analysis/stopwords.txt")
						.Synonyms("x=>y", "z=>s")
						.Tokenizer("whitespace")
					)
					.Trim("trimmer")
					.Truncate("truncer", t => t.Length(100))
					.Unique("uq", t => t.OnlyOnSamePosition())
					.Uppercase("upper")
					.WordDelimiter("wd", t => t
						.CatenateAll()
						.CatenateNumbers()
						.CatenateWords()
						.GenerateNumberParts()
						.GenerateWordParts()
						.PreserveOriginal()
						.ProtectedWords("x", "y", "z")
						.SplitOnCaseChange()
						.SplitOnNumerics()
						.StemEnglishPossessive()
					)
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
					TokenFilters = new Nest.TokenFilters
					{
							{ "myAscii", new AsciiFoldingTokenFilter { PreserveOriginal = true } },
							{ "myCommonGrams", new CommonGramsTokenFilter { QueryMode = true, IgnoreCase = true, CommonWords = new [] {"x", "y", "z"} } },
							{ "mydp", new DelimitedPayloadTokenFilter { Delimiter = '-', Encoding = DelimitedPayloadEncoding.Identity } },
							{ "dcc", new DictionaryDecompounderTokenFilter
							{
								MinWordSize = 2,
								MinSubwordSize = 2,
								MaxSubwordSize = 2,
								OnlyLongestMatch = true,
								WordList = new [] { "x", "y", "z"}
							} },
							{ "etf", new EdgeNGramTokenFilter { MaxGram = 2, MinGram = 1 } },
							{ "elision", new ElisionTokenFilter { Articles = new [] { "a", "b", "c" } } },
							{ "hunspell", new HunspellTokenFilter
							{
								Dedup = true,
								Dictionary = "path_to_dict",
								IgnoreCase = true,
								Locale = "en_US",
								LongestOnly = true
							} },
							{ "hypdecomp", new HyphenationDecompounderTokenFilter
							{
								MaxSubwordSize = 2,
								MinSubwordSize = 2,
								MinWordSize = 2,
								OnlyLongestMatch = true,
								WordList = new [] { "x", "y", "z"},
								HyphenationPatternsPath = "analysis/fop.xml"
							} },
							{ "keeptypes", new KeepTypesTokenFilter { Types = new [] { "<NUM>", "<SOMETHINGELSE>"} } },
							{ "keepwords", new KeepWordsTokenFilter { KeepWordsCase = true, KeepWords = new [] { "a", "b", "c" } } },
							{ "marker", new KeywordMarkerTokenFilter { IgnoreCase = true, Keywords = new [] { "a", "b" } } },
							{ "kstem", new KStemTokenFilter { } },
							{ "length", new LengthTokenFilter { Min = 10, Max = 200 } },
							{ "limit", new LimitTokenCountTokenFilter { ConsumeAllTokens = true, MaxTokenCount = 12  } },
							{ "lc", new LowercaseTokenFilter() },
							{ "ngram", new NGramTokenFilter { MinGram = 3, MaxGram = 30 } },
							{ "pc", new PatternCaptureTokenFilter { Patterns = new [] { @"\d", @"\w"}, PreserveOriginal = true } },
							{ "pr", new PatternReplaceTokenFilter { Pattern = @"(\d|\w)", Replacement = "replacement" } },
							{ "porter", new PorterStemTokenFilter() },
							{ "rev", new ReverseTokenFilter() },
							{ "shing", new ShingleTokenFilter
							{
								FillerToken = "x",
								MaxShingleSize = 12,
								MinShingleSize = 8,
								OutputUnigrams = true,
								OutputUnigramsIfNoShingles = true,
								TokenSeparator = "|"
							} },
							{ "snow", new SnowballTokenFilter { Language = SnowballLanguage.Dutch } },
							{ "standard", new StandardTokenFilter () },
							{ "stem", new StemmerTokenFilter { Language = "arabic" } },
							{ "stemo", new StemmerOverrideTokenFilter { RulesPath = "analysis/custom_stems.txt" } },
							{ "stop", new StopTokenFilter { IgnoreCase = true, RemoveTrailing = true, StopWords = new [] { "x", "y", "z"}  } },
							{ "syn", new SynonymTokenFilter
							{
								Expand = true,
								Format = SynonymFormat.WordNet,
								IgnoreCase = true,
								SynonymsPath = "analysis/stopwords.txt",
								Synonyms = new [] { "x=>y", "z=>s"},
								Tokenizer = "whitespace"
							} },
							{ "trimmer", new TrimTokenFilter() },
							{ "truncer", new TruncateTokenFilter { Length = 100 } },
							{ "uq", new UniqueTokenFilter { OnlyOnSamePosition = true, } },
							{ "upper", new UppercaseTokenFilter() },
							{ "wd", new WordDelimiterTokenFilter
							{
								CatenateAll = true,
								CatenateNumbers = true,
								CatenateWords = true,
								GenerateNumberParts = true,
								GenerateWordParts = true,
								PreserveOriginal = true,
								ProtectedWords = new [] { "x", "y", "z"},
								SplitOnCaseChange = true,
								SplitOnNumerics = true,
								StemEnglishPossessive = true
							} }
					}
				}
			};
	}
}
