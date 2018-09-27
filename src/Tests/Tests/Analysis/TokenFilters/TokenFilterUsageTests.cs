using System;
using Nest;
using Tests.Framework;

namespace Tests.Analysis.TokenFilters
{
	using FuncTokenFilters = Func<string, TokenFiltersDescriptor, IPromise<ITokenFilters>>;

	public static class TokenFilterTests
	{
		public class AsciiFoldingTests : TokenFilterAssertionBase<AsciiFoldingTests>
		{
			protected override string Name => "ascii";
			protected override ITokenFilter Initializer => new AsciiFoldingTokenFilter {PreserveOriginal = true};
			protected override FuncTokenFilters Fluent => (n, tf) => tf.AsciiFolding(n, t => t.PreserveOriginal());
			protected override object Json => new {type = "asciifolding", preserve_original = true};
		}

		public class CommonGramsTests : TokenFilterAssertionBase<CommonGramsTests>
		{
			protected override string Name => "mycomgram";

			protected override ITokenFilter Initializer =>
				new CommonGramsTokenFilter {QueryMode = true, IgnoreCase = true, CommonWords = new[] {"x", "y", "z"}};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.CommonGrams(n, t => t.CommonWords("x", "y", "z").IgnoreCase().QueryMode());

			protected override object Json => new
			{
				type = "common_grams",
				common_words = new[] {"x", "y", "z"},
				ignore_case = true,
				query_mode = true
			};
		}

		public class DelimitedPayloadFilterTests : TokenFilterAssertionBase<DelimitedPayloadFilterTests>
		{
			protected override string Name => "mydp";

			protected override ITokenFilter Initializer =>
				new DelimitedPayloadTokenFilter {Delimiter = '-', Encoding = DelimitedPayloadEncoding.Identity};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.DelimitedPayload(n, t => t.Delimiter('-').Encoding(DelimitedPayloadEncoding.Identity));

			protected override object Json => new { type = "delimited_payload_filter", delimiter = "-", encoding = "identity" };
		}

		public class DictionaryDecompounderTests : TokenFilterAssertionBase<DictionaryDecompounderTests>
		{
			protected override string Name => "dcc";

			protected override ITokenFilter Initializer =>
				new DictionaryDecompounderTokenFilter
				{
					MinWordSize = 2,
					MinSubwordSize = 2,
					MaxSubwordSize = 2,
					OnlyLongestMatch = true,
					WordList = new[] {"x", "y", "z"}
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.DictionaryDecompounder(n, t => t
					.MaxSubwordSize(2)
					.MinSubwordSize(2)
					.MinWordSize(2)
					.OnlyLongestMatch()
					.WordList("x", "y", "z")
				);

			protected override object Json => new
			{
				type = "dictionary_decompounder",
				word_list = new[] {"x", "y", "z"},
				min_word_size = 2,
				min_subword_size = 2,
				max_subword_size = 2,
				only_longest_match = true
			};

		}

		public class EdgeNgramTests : TokenFilterAssertionBase<EdgeNgramTests>
		{
			protected override string Name => "etf";

			protected override ITokenFilter Initializer => new EdgeNGramTokenFilter {MaxGram = 2, MinGram = 1};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.EdgeNGram(n, t => t.MaxGram(2).MinGram(1));
			protected override object Json => new { type = "edge_ngram", min_gram = 1, max_gram = 2 };

		}

		public class ElisionTests : TokenFilterAssertionBase<ElisionTests>
		{
			protected override string Name => "el";

			protected override ITokenFilter Initializer => new ElisionTokenFilter {Articles = new[] {"a", "b", "c"}};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.Elision(n, t => t.Articles("a", "b", "c"));

			protected override object Json => new { type = "elision", articles = new[] {"a", "b", "c"} };
		}

		public class HunspellTests : TokenFilterAssertionBase<HunspellTests>
		{
			protected override string Name => "huns";

			protected override ITokenFilter Initializer =>
				new HunspellTokenFilter
				{
					Dedup = true,
					Dictionary = "path_to_dict",
					Locale = "en_US",
					LongestOnly = true
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.Hunspell(n, t => t
					.Dedup()
					.Dictionary("path_to_dict")
					.Locale("en_US")
					.LongestOnly()
				);

			protected override object Json => new
			{
				type = "hunspell",
				locale = "en_US",
				dictionary = "path_to_dict",
				dedup = true,
				longest_only = true
			};

		}

		public class HyphenationDecompounderTests : TokenFilterAssertionBase<HyphenationDecompounderTests>
		{
			protected override string Name => "hyphdecomp";

			protected override ITokenFilter Initializer =>
				new HyphenationDecompounderTokenFilter
				{
					MaxSubwordSize = 2,
					MinSubwordSize = 2,
					MinWordSize = 2,
					OnlyLongestMatch = true,
					WordList = new[] {"x", "y", "z"},
					HyphenationPatternsPath = "analysis/fop.xml"
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.HyphenationDecompounder(n, t => t
					.MaxSubwordSize(2)
					.MinSubwordSize(2)
					.MinWordSize(2)
					.OnlyLongestMatch()
					.WordList("x", "y", "z")
					.HyphenationPatternsPath("analysis/fop.xml")
				);

			protected override object Json => new
			{
				type = "hyphenation_decompounder",
				word_list = new[] {"x", "y", "z"},
				min_word_size = 2,
				min_subword_size = 2,
				max_subword_size = 2,
				only_longest_match = true,
				hyphenation_patterns_path = "analysis/fop.xml"
			};

		}

		public class KeepTypesTests : TokenFilterAssertionBase<KeepTypesTests>
		{
			protected override string Name => "keeptypes";

			protected override ITokenFilter Initializer =>
				new KeepTypesTokenFilter {Types = new[] {"<NUM>", "<SOMETHINGELSE>"}};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.KeepTypes(n, t => t
					.Types("<NUM>", "<SOMETHINGELSE>")
				);

			protected override object Json => new
			{
				type = "keep_types",
				types = new[] {"<NUM>", "<SOMETHINGELSE>"}
			};

		}

		public class IcuCollationTests : TokenFilterAssertionBase<IcuCollationTests>
		{
			protected override string Name => "icuc";

			protected override ITokenFilter Initializer =>
				new IcuCollationTokenFilter
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
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.IcuCollation(n, t => t
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
				);

			protected override object Json => new
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
			};

		}

		public class IcuFoldingTests : TokenFilterAssertionBase<IcuFoldingTests>
		{
			protected override string Name => "icuf";

			protected override ITokenFilter Initializer =>
				new IcuFoldingTokenFilter { UnicodeSetFilter = "[^åäöÅÄÖ]" };

			protected override FuncTokenFilters Fluent => (n, tf) => tf.IcuFolding(n, t => t.UnicodeSetFilter("[^åäöÅÄÖ]"));

			protected override object Json => new
			{
				type = "icu_folding",
				unicodeSetFilter = "[^åäöÅÄÖ]"
			};

		}

		public class IcuNormalizerTests : TokenFilterAssertionBase<IcuNormalizerTests>
		{
			protected override string Name => "icun";

			protected override ITokenFilter Initializer => new IcuNormalizationTokenFilter { Name = IcuNormalizationType.Canonical };

			protected override FuncTokenFilters Fluent => (n, tf) => tf .IcuNormalization(n, t => t.Name(IcuNormalizationType.Canonical));

			protected override object Json => new
			{
				name = "nfc",
				type = "icu_normalizer"
			};

		}

		public class IcuTransformTests : TokenFilterAssertionBase<IcuTransformTests>
		{
			protected override string Name => "icut";

			protected override ITokenFilter Initializer =>
				new IcuTransformTokenFilter
				{
					Direction = IcuTransformDirection.Forward,
					Id = "Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC"
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.IcuTransform(n, t => t
					.Direction(IcuTransformDirection.Forward)
					.Id("Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC")
				);

			protected override object Json => new
			{
				dir = "forward",
				id = "Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC",
				type = "icu_transform"
			};

		}

		public class KeepwordsTests : TokenFilterAssertionBase<KeepwordsTests>
		{
			protected override string Name => "keepwords";

			protected override ITokenFilter Initializer =>
				new KeepWordsTokenFilter {KeepWordsCase = true, KeepWords = new[] {"a", "b", "c"}};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.KeepWords(n, t => t
					.KeepWords("a", "b", "c")
					.KeepWordsCase()
				);

			protected override object Json => new
			{
				type = "keep",
				keep_words = new[] {"a", "b", "c"},
				keep_words_case = true
			};

		}

		public class MarkerTests : TokenFilterAssertionBase<MarkerTests>
		{
			protected override string Name => "marker";

			protected override ITokenFilter Initializer => new KeywordMarkerTokenFilter {IgnoreCase = true, Keywords = new[] {"a", "b"}};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.KeywordMarker("marker", t => t
					.IgnoreCase()
					.Keywords("a", "b")
				);

			protected override object Json => new
			{
				type = "keyword_marker",
				keywords = new[] {"a", "b"},
				ignore_case = true
			};


		}

		public class KuromojiReadingFormTests : TokenFilterAssertionBase<KuromojiReadingFormTests>
		{
			protected override string Name => "kfr";

			protected override ITokenFilter Initializer => new KuromojiReadingFormTokenFilter {UseRomaji = true};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.KuromojiReadingForm(n, t => t.UseRomaji());

			protected override object Json => new
			{
				type = "kuromoji_readingform",
				use_romaji = true
			};

		}

		public class KuromojiPartOfSpeechTests : TokenFilterAssertionBase<KuromojiPartOfSpeechTests>
		{
			protected override string Name => "kpos";

			protected override ITokenFilter Initializer =>
				new KuromojiPartOfSpeechTokenFilter {StopTags = new[] {"#  verb-main:", "動詞-自立"}};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.KuromojiPartOfSpeech(n, t => t.StopTags("#  verb-main:", "動詞-自立"));

			protected override object Json => new
			{
				stoptags = new[]
				{
					"#  verb-main:",
					"動詞-自立"
				},
				type = "kuromoji_part_of_speech"
			};

		}

		public class KuromojiStemmerTests : TokenFilterAssertionBase<KuromojiStemmerTests>
		{
			protected override string Name => "ks";

			protected override ITokenFilter Initializer => new KuromojiStemmerTokenFilter {MinimumLength = 4};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.KuromojiStemmer(n, t => t.MinimumLength(4));

			protected override object Json => new
			{
				minimum_length = 4,
				type = "kuromoji_stemmer"
			};

		}

		public class KStemTests : TokenFilterAssertionBase<KStemTests>
		{
			protected override string Name => "kstem";
			protected override ITokenFilter Initializer => new KStemTokenFilter { };
			protected override FuncTokenFilters Fluent => (n, tf) => tf.KStem(n);
			protected override object Json => new {type = "kstem"};
		}

		public class LengthTests : TokenFilterAssertionBase<LengthTests>
		{
			protected override string Name => "length";
			protected override ITokenFilter Initializer => new LengthTokenFilter {Min = 10, Max = 200};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.Length(n, t => t.Max(200).Min(10));
			protected override object Json => new {type = "length", min = 10, max = 200};

		}

		public class LimitTests : TokenFilterAssertionBase<LimitTests>
		{
			protected override string Name => "limit";

			protected override ITokenFilter Initializer => new LimitTokenCountTokenFilter {ConsumeAllTokens = true, MaxTokenCount = 12};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.LimitTokenCount(n, t => t.ConsumeAllToken().MaxTokenCount(12));

			protected override object Json => new
			{
				type = "limit",
				max_token_count = 12,
				consume_all_tokens = true
			};

		}

		public class LowercaseTests : TokenFilterAssertionBase<LowercaseTests>
		{
			protected override string Name => "lc";

			protected override ITokenFilter Initializer => new LowercaseTokenFilter();

			protected override FuncTokenFilters Fluent => (n, tf) => tf.Lowercase(n);

			protected override object Json => new {type = "lowercase"};

		}

		public class NGramTests : TokenFilterAssertionBase<NGramTests>
		{
			protected override string Name => "ngram";

			protected override ITokenFilter Initializer => new NGramTokenFilter {MinGram = 3, MaxGram = 4};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.NGram(n, t => t.MinGram(3).MaxGram(4));

			protected override object Json => new {type = "ngram", min_gram = 3, max_gram = 4};

		}

		public class PatternCaptureTests : TokenFilterAssertionBase<PatternCaptureTests>
		{
			protected override string Name => "pc";

			protected override ITokenFilter Initializer =>
				new PatternCaptureTokenFilter {Patterns = new[] {@"\d", @"\w"}, PreserveOriginal = true};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.PatternCapture(n, t => t.Patterns(@"\d", @"\w").PreserveOriginal());

			protected override object Json => new
			{
				type = "pattern_capture",
				patterns = new[] {"\\d", "\\w"},
				preserve_original = true
			};
		}

		public class PatternReplaceTests : TokenFilterAssertionBase<PatternReplaceTests>
		{
			protected override string Name => "pr";

			protected override ITokenFilter Initializer =>
				new PatternReplaceTokenFilter {Pattern = @"(\d|\w)", Replacement = "replacement"};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.PatternReplace(n, t => t
					.Pattern(@"(\d|\w)")
					.Replacement("replacement")
				);

			protected override object Json => new
			{
				type = "pattern_replace",
				pattern = "(\\d|\\w)",
				replacement = "replacement"
			};

		}

		public class PorterStemTests : TokenFilterAssertionBase<PorterStemTests>
		{
			protected override string Name => "porter";
			protected override ITokenFilter Initializer => new PorterStemTokenFilter();
			protected override FuncTokenFilters Fluent => (n, tf) => tf.PorterStem(n);
			protected override object Json => new { type = "porter_stem" };
		}

		public class ReverseTests : TokenFilterAssertionBase<ReverseTests>
		{
			protected override string Name => "rev";
			protected override ITokenFilter Initializer => new ReverseTokenFilter();
			protected override FuncTokenFilters Fluent => (n, tf) => tf.Reverse(n);
			protected override object Json => new {type = "reverse"};

		}

		public class ShingleTests : TokenFilterAssertionBase<ShingleTests>
		{
			protected override string Name => "shing";

			protected override ITokenFilter Initializer => new ShingleTokenFilter
			{
				FillerToken = "x",
				MaxShingleSize = 10,
				MinShingleSize = 8,
				OutputUnigrams = true,
				OutputUnigramsIfNoShingles = true,
				TokenSeparator = "|"
			};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.Shingle(n, t => t
					.FillerToken("x")
					.MaxShingleSize(10)
					.MinShingleSize(8)
					.OutputUnigrams()
					.OutputUnigramsIfNoShingles()
					.TokenSeparator("|")
				);

			protected override object Json => new
			{
				type = "shingle",
				min_shingle_size = 8,
				max_shingle_size = 10,
				output_unigrams = true,
				output_unigrams_if_no_shingles = true,
				token_separator = "|",
				filler_token = "x"
			};

		}

		public class SnowballTests : TokenFilterAssertionBase<SnowballTests>
		{
			protected override string Name => "snow";

			protected override ITokenFilter Initializer => new SnowballTokenFilter {Language = SnowballLanguage.Dutch};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.Snowball(n, t => t.Language(SnowballLanguage.Dutch));

			protected override object Json => new
			{
				type = "snowball",
				language = "Dutch"
			};

		}

		public class StandardTests : TokenFilterAssertionBase<StandardTests>
		{
			protected override string Name => "standard";

			protected override ITokenFilter Initializer => new StandardTokenFilter();

			protected override FuncTokenFilters Fluent => (n, tf) => tf.Standard(n);

			protected override object Json => new { type = "standard" };

		}

		public class StemmerTests : TokenFilterAssertionBase<StemmerTests>
		{
			protected override string Name => "stem";

			protected override ITokenFilter Initializer => new StemmerTokenFilter {Language = "arabic"};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.Stemmer(n, t => t.Language("arabic"));

			protected override object Json => new
			{
				type = "stemmer",
				language = "arabic"
			};

		}

		public class StemmerOverrideTests : TokenFilterAssertionBase<StemmerOverrideTests>
		{
			protected override string Name => "stemo";

			protected override ITokenFilter Initializer => new StemmerOverrideTokenFilter {RulesPath = "analysis/custom_stems.txt"};

			protected override FuncTokenFilters Fluent => (n, tf) => tf.StemmerOverride(n, t => t.RulesPath("analysis/custom_stems.txt"));

			protected override object Json => new
			{
				type = "stemmer_override",
				rules_path = "analysis/custom_stems.txt"
			};

		}

		public class StopTests : TokenFilterAssertionBase<StopTests>
		{
			protected override string Name => "stop";

			protected override ITokenFilter Initializer =>
				new StopTokenFilter {IgnoreCase = true, RemoveTrailing = true, StopWords = new[] {"x", "y", "z"}};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.Stop(n, t => t
					.IgnoreCase()
					.RemoveTrailing()
					.StopWords("x", "y", "z")
				);

			protected override object Json => new
			{
				type = "stop",
				stopwords = new[] {"x", "y", "z"},
				ignore_case = true,
				remove_trailing = true
			};

		}

		public class SynonymTests : TokenFilterAssertionBase<SynonymTests>
		{
			protected override string Name => "syn";

			protected override ITokenFilter Initializer =>
				new SynonymTokenFilter
				{
					Expand = true,
					Format = SynonymFormat.WordNet,
					SynonymsPath = "analysis/stopwords.txt",
					Synonyms = new[] {"x=>y", "z=>s"},
					Tokenizer = "whitespace"
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.Synonym(n, t => t
					.Expand()
					.Format(SynonymFormat.WordNet)
					.SynonymsPath("analysis/stopwords.txt")
					.Synonyms("x=>y", "z=>s")
					.Tokenizer("whitespace")
				);

			protected override object Json => new
			{
				type = "synonym",
				synonyms_path = "analysis/stopwords.txt",
				format = "wordnet",
				synonyms = new[] {"x=>y", "z=>s"},
				expand = true,
				tokenizer = "whitespace"
			};

		}

		public class SynonymGraphTests : TokenFilterAssertionBase<SynonymGraphTests>
		{
			protected override string Name => "syn_graph";

			protected override ITokenFilter Initializer =>
				new SynonymGraphTokenFilter
				{
					Expand = true,
					Format = SynonymFormat.WordNet,
					SynonymsPath = "analysis/stopwords.txt",
					Synonyms = new[] {"x=>y", "z=>s"},
					Tokenizer = "whitespace"
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.SynonymGraph(n, t => t
					.Expand()
					.Format(SynonymFormat.WordNet)
					.SynonymsPath("analysis/stopwords.txt")
					.Synonyms("x=>y", "z=>s")
					.Tokenizer("whitespace")
				);

			protected override object Json => new
			{
				type = "synonym_graph",
				synonyms_path = "analysis/stopwords.txt",
				format = "wordnet",
				synonyms = new[] {"x=>y", "z=>s"},
				expand = true,
				tokenizer = "whitespace"
			};

		}

		public class TrimTests : TokenFilterAssertionBase<TrimTests>
		{
			protected override string Name => "trimmer";
			protected override ITokenFilter Initializer => new TrimTokenFilter();
			protected override FuncTokenFilters Fluent => (n, tf) => tf.Trim(n);
			protected override object Json => new {type = "trim"};
		}

		public class TruncateTests : TokenFilterAssertionBase<TruncateTests>
		{
			protected override string Name => "truncer";
			protected override ITokenFilter Initializer => new TruncateTokenFilter {Length = 100};
			protected override FuncTokenFilters Fluent => (n, tf) => tf.Truncate(n, t => t.Length(100));
			protected override object Json => new {type = "truncate", length = 100};
		}

		public class UniqueTests : TokenFilterAssertionBase<UniqueTests>
		{
			protected override string Name => "uq";
			protected override ITokenFilter Initializer => new UniqueTokenFilter {OnlyOnSamePosition = true,};
			protected override FuncTokenFilters Fluent => (n, tf) => tf.Unique(n, t => t.OnlyOnSamePosition());
			protected override object Json => new {type = "unique", only_on_same_position = true};

		}
		public class UppercaseTests : TokenFilterAssertionBase<UppercaseTests>
		{
			protected override string Name => "upper";
			protected override ITokenFilter Initializer => new UppercaseTokenFilter();
			protected override FuncTokenFilters Fluent => (n, tf) => tf.Uppercase(n);
			protected override object Json => new {type = "uppercase"};

		}
		public class WordDelimiterTests : TokenFilterAssertionBase<WordDelimiterTests>
		{
			protected override string Name => "wd";

			protected override ITokenFilter Initializer =>
				new WordDelimiterTokenFilter
				{
					CatenateAll = true,
					CatenateNumbers = true,
					CatenateWords = true,
					GenerateNumberParts = true,
					GenerateWordParts = true,
					PreserveOriginal = true,
					ProtectedWords = new[] {"x", "y", "z"},
					SplitOnCaseChange = true,
					SplitOnNumerics = true,
					StemEnglishPossessive = true
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.WordDelimiter(n, t => t
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
				);

			protected override object Json => new
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
				protected_words = new[] {"x", "y", "z"}
			};

		}

		public class WordDelimiterGraphTests : TokenFilterAssertionBase<WordDelimiterGraphTests>
		{
			protected override string Name => "wdg";

			protected override ITokenFilter Initializer =>
				new WordDelimiterGraphTokenFilter
				{
					CatenateAll = true,
					CatenateNumbers = true,
					CatenateWords = true,
					GenerateNumberParts = true,
					GenerateWordParts = true,
					PreserveOriginal = true,
					ProtectedWords = new[] {"x", "y", "z"},
					SplitOnCaseChange = true,
					SplitOnNumerics = true,
					StemEnglishPossessive = true
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.WordDelimiterGraph(n, t => t
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
				);

			protected override object Json => new
			{
				type = "word_delimiter_graph",
				generate_word_parts = true,
				generate_number_parts = true,
				catenate_words = true,
				catenate_numbers = true,
				catenate_all = true,
				split_on_case_change = true,
				preserve_original = true,
				split_on_numerics = true,
				stem_english_possessive = true,
				protected_words = new[] {"x", "y", "z"}
			};

		}

		public class PhoneticTests : TokenFilterAssertionBase<PhoneticTests>
		{
			protected override string Name => "phonetic";

			protected override ITokenFilter Initializer =>
				new PhoneticTokenFilter
				{
					Encoder = PhoneticEncoder.Beidermorse,
					RuleType = PhoneticRuleType.Exact,
					NameType = PhoneticNameType.Sephardic,
					LanguageSet = new[] {PhoneticLanguage.Cyrillic, PhoneticLanguage.English, PhoneticLanguage.Hebrew}
				};

			protected override FuncTokenFilters Fluent => (n, tf) => tf
				.Phonetic(n, t => t
					.Encoder(PhoneticEncoder.Beidermorse)
					.RuleType(PhoneticRuleType.Exact)
					.NameType(PhoneticNameType.Sephardic)
					.LanguageSet(
						PhoneticLanguage.Cyrillic,
						PhoneticLanguage.English,
						PhoneticLanguage.Hebrew
					)
				);

			protected override object Json => new
			{
				type = "phonetic",
				encoder = "beider_morse",
				rule_type = "exact",
				name_type = "sephardic",
				languageset = new[] {"cyrillic", "english", "hebrew"}
			};

		}
	}
}
