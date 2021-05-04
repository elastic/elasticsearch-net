// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.IndexModules;

namespace Tests.Analysis.TokenFilters
{
	/**
	 */

	public class TokenFilterUsageTests : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
	{
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
						.MaxGram(4)
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
						.MaxShingleSize(10)
						.MinShingleSize(8)
						.OutputUnigrams()
						.OutputUnigramsIfNoShingles()
						.TokenSeparator("|")
					)
					.Snowball("snow", t => t.Language(SnowballLanguage.Dutch))
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
						.SynonymsPath("analysis/stopwords.txt")
						.Synonyms("x=>y", "z=>s")
						.Tokenizer("whitespace")
					)
					.SynonymGraph("syn_graph", t => t
						.Expand()
						.Format(SynonymFormat.WordNet)
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
					.WordDelimiterGraph("wdg", t => t
						.CatenateAll()
						.CatenateNumbers()
						.CatenateWords()
						.GenerateNumberParts()
						.GenerateWordParts()
						.IgnoreKeywords()
						.PreserveOriginal()
						.ProtectedWords("x", "y", "z")
						.SplitOnCaseChange()
						.SplitOnNumerics()
						.StemEnglishPossessive()
					)
					.KuromojiPartOfSpeech("kpos", t => t
						.StopTags("#  verb-main:", "動詞-自立")
					)
					.KuromojiReadingForm("kfr", t => t
						.UseRomaji()
					)
					.KuromojiStemmer("ks", t => t
						.MinimumLength(4)
					)
					.IcuCollation("icuc", t => t
						.Alternate(IcuCollationAlternate.NonIgnorable)
						.CaseFirst(IcuCollationCaseFirst.Lower)
						.HiraganaQuaternaryMode()
						.Decomposition(IcuCollationDecomposition.No)
						.Numeric()
						.CaseLevel()
						.Country("DE")
						.Language("de")
						.Strength(IcuCollationStrength.Tertiary)
						.Variant("@collation=phonebook")
					)
					.IcuFolding("icuf", t => t.UnicodeSetFilter("[^åäöÅÄÖ]"))
					.IcuNormalization("icun", t => t.Name(IcuNormalizationType.Canonical))
					.IcuTransform("icut", t => t
						.Direction(IcuTransformDirection.Forward)
						.Id("Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC")
					)
					.Phonetic("phonetic", t => t
						.Encoder(PhoneticEncoder.Beidermorse)
						.RuleType(PhoneticRuleType.Exact)
						.NameType(PhoneticNameType.Sephardic)
						.LanguageSet(PhoneticLanguage.Cyrillic, PhoneticLanguage.English, PhoneticLanguage.Hebrew)
					)
				)
			);

		public static IndexSettings InitializerExample =>
			new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					TokenFilters = new Nest.TokenFilters
					{
						{ "myAscii", new AsciiFoldingTokenFilter { PreserveOriginal = true } },
						{
							"myCommonGrams", new CommonGramsTokenFilter { QueryMode = true, IgnoreCase = true, CommonWords = new[] { "x", "y", "z" } }
						},
						{ "mydp", new DelimitedPayloadTokenFilter { Delimiter = '-', Encoding = DelimitedPayloadEncoding.Identity } },
						{
							"dcc", new DictionaryDecompounderTokenFilter
							{
								MinWordSize = 2,
								MinSubwordSize = 2,
								MaxSubwordSize = 2,
								OnlyLongestMatch = true,
								WordList = new[] { "x", "y", "z" }
							}
						},
						{ "etf", new EdgeNGramTokenFilter { MaxGram = 2, MinGram = 1 } },
						{ "elision", new ElisionTokenFilter { Articles = new[] { "a", "b", "c" } } },
						{
							"hunspell", new HunspellTokenFilter
							{
								Dedup = true,
								Dictionary = "path_to_dict",
								Locale = "en_US",
								LongestOnly = true
							}
						},
						{
							"hypdecomp", new HyphenationDecompounderTokenFilter
							{
								MaxSubwordSize = 2,
								MinSubwordSize = 2,
								MinWordSize = 2,
								OnlyLongestMatch = true,
								WordList = new[] { "x", "y", "z" },
								HyphenationPatternsPath = "analysis/fop.xml"
							}
						},
						{ "keeptypes", new KeepTypesTokenFilter { Types = new[] { "<NUM>", "<SOMETHINGELSE>" } } },
						{ "keepwords", new KeepWordsTokenFilter { KeepWordsCase = true, KeepWords = new[] { "a", "b", "c" } } },
						{ "marker", new KeywordMarkerTokenFilter { IgnoreCase = true, Keywords = new[] { "a", "b" } } },
						{ "kstem", new KStemTokenFilter() },
						{ "length", new LengthTokenFilter { Min = 10, Max = 200 } },
						{ "limit", new LimitTokenCountTokenFilter { ConsumeAllTokens = true, MaxTokenCount = 12 } },
						{ "lc", new LowercaseTokenFilter() },
						{ "ngram", new NGramTokenFilter { MinGram = 3, MaxGram = 4 } },
						{ "pc", new PatternCaptureTokenFilter { Patterns = new[] { @"\d", @"\w" }, PreserveOriginal = true } },
						{ "pr", new PatternReplaceTokenFilter { Pattern = @"(\d|\w)", Replacement = "replacement" } },
						{ "porter", new PorterStemTokenFilter() },
						{ "rev", new ReverseTokenFilter() },
						{
							"shing", new ShingleTokenFilter
							{
								FillerToken = "x",
								MaxShingleSize = 10,
								MinShingleSize = 8,
								OutputUnigrams = true,
								OutputUnigramsIfNoShingles = true,
								TokenSeparator = "|"
							}
						},
						{ "snow", new SnowballTokenFilter { Language = SnowballLanguage.Dutch } },
						{ "stem", new StemmerTokenFilter { Language = "arabic" } },
						{ "stemo", new StemmerOverrideTokenFilter { RulesPath = "analysis/custom_stems.txt" } },
						{ "stop", new StopTokenFilter { IgnoreCase = true, RemoveTrailing = true, StopWords = new[] { "x", "y", "z" } } },
						{
							"syn", new SynonymTokenFilter
							{
								Expand = true,
								Format = SynonymFormat.WordNet,
								SynonymsPath = "analysis/stopwords.txt",
								Synonyms = new[] { "x=>y", "z=>s" },
								Tokenizer = "whitespace"
							}
						},
						{
							"syn_graph", new SynonymGraphTokenFilter
							{
								Expand = true,
								Format = SynonymFormat.WordNet,
								SynonymsPath = "analysis/stopwords.txt",
								Synonyms = new[] { "x=>y", "z=>s" },
								Tokenizer = "whitespace"
							}
						},
						{ "trimmer", new TrimTokenFilter() },
						{ "truncer", new TruncateTokenFilter { Length = 100 } },
						{ "uq", new UniqueTokenFilter { OnlyOnSamePosition = true, } },
						{ "upper", new UppercaseTokenFilter() },
						{
							"wd", new WordDelimiterTokenFilter
							{
								CatenateAll = true,
								CatenateNumbers = true,
								CatenateWords = true,
								GenerateNumberParts = true,
								GenerateWordParts = true,
								PreserveOriginal = true,
								ProtectedWords = new[] { "x", "y", "z" },
								SplitOnCaseChange = true,
								SplitOnNumerics = true,
								StemEnglishPossessive = true
							}
						},
						{
							"wdg", new WordDelimiterGraphTokenFilter
							{
								CatenateAll = true,
								CatenateNumbers = true,
								CatenateWords = true,
								GenerateNumberParts = true,
								GenerateWordParts = true,
								IgnoreKeywords = true,
								PreserveOriginal = true,
								ProtectedWords = new[] { "x", "y", "z" },
								SplitOnCaseChange = true,
								SplitOnNumerics = true,
								StemEnglishPossessive = true
							}
						},
						{ "kpos", new KuromojiPartOfSpeechTokenFilter { StopTags = new[] { "#  verb-main:", "動詞-自立" } } },
						{ "kfr", new KuromojiReadingFormTokenFilter { UseRomaji = true } },
						{ "ks", new KuromojiStemmerTokenFilter { MinimumLength = 4 } },

						{
							"icuc", new IcuCollationTokenFilter
							{
								Alternate = IcuCollationAlternate.NonIgnorable,
								CaseFirst = IcuCollationCaseFirst.Lower,
								HiraganaQuaternaryMode = true,
								Decomposition = IcuCollationDecomposition.No,
								Numeric = true,
								CaseLevel = true,
								Country = "DE",
								Language = "de",
								Strength = IcuCollationStrength.Tertiary,
								Variant = "@collation=phonebook"
							}
						},
						{
							"icuf", new IcuFoldingTokenFilter
							{
								UnicodeSetFilter = "[^åäöÅÄÖ]"
							}
						},
						{
							"icun", new IcuNormalizationTokenFilter
							{
								Name = IcuNormalizationType.Canonical
							}
						},
						{
							"icut", new IcuTransformTokenFilter
							{
								Direction = IcuTransformDirection.Forward,
								Id = "Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC"
							}
						},
						{
							"phonetic", new PhoneticTokenFilter
							{
								Encoder = PhoneticEncoder.Beidermorse,
								RuleType = PhoneticRuleType.Exact,
								NameType = PhoneticNameType.Sephardic,
								LanguageSet = new[] { PhoneticLanguage.Cyrillic, PhoneticLanguage.English, PhoneticLanguage.Hebrew }
							}
						},
					}
				}
			};

		protected override object ExpectJson => new
		{
			analysis = new
			{
				filter = new
				{
					myAscii = new
					{
						type = "asciifolding",
						preserve_original = true
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
						type = "delimited_payload",
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
					icuc = new
					{
						alternate = "non-ignorable",
						caseFirst = "lower",
						caseLevel = true,
						country = "DE",
						decomposition = "no",
						hiraganaQuaternaryMode = true,
						language = "de",
						numeric = true,
						strength = "tertiary",
						type = "icu_collation",
						variant = "@collation=phonebook"
					},
					icuf = new
					{
						type = "icu_folding",
						unicodeSetFilter = "[^åäöÅÄÖ]"
					},
					icun = new
					{
						name = "nfc",
						type = "icu_normalizer"
					},
					icut = new
					{
						dir = "forward",
						id = "Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC",
						type = "icu_transform"
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
					kfr = new
					{
						type = "kuromoji_readingform",
						use_romaji = true
					},
					kpos = new
					{
						stoptags = new[]
						{
							"#  verb-main:",
							"動詞-自立"
						},
						type = "kuromoji_part_of_speech"
					},
					ks = new
					{
						minimum_length = 4,
						type = "kuromoji_stemmer"
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
						max_gram = 4
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
						max_shingle_size = 10,
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
						expand = true,
						tokenizer = "whitespace"
					},
					syn_graph = new
					{
						type = "synonym_graph",
						synonyms_path = "analysis/stopwords.txt",
						format = "wordnet",
						synonyms = new[] { "x=>y", "z=>s" },
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
					},
					wdg = new
					{
						type = "word_delimiter_graph",
						generate_word_parts = true,
						generate_number_parts = true,
						ignore_keywords = true,
						catenate_words = true,
						catenate_numbers = true,
						catenate_all = true,
						split_on_case_change = true,
						preserve_original = true,
						split_on_numerics = true,
						stem_english_possessive = true,
						protected_words = new[] { "x", "y", "z" }
					},
					phonetic = new
					{
						type = "phonetic",
						encoder = "beider_morse",
						rule_type = "exact",
						name_type = "sephardic",
						languageset = new[] { "cyrillic", "english", "hebrew" }
					}
				}
			}
		};

		/**
		 *
		 */
		protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => FluentExample;

		/**
		 */
		protected override IndexSettings Initializer => InitializerExample;
	}
}
