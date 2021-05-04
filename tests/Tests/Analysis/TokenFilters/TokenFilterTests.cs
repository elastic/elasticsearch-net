// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.Serialization;

namespace Tests.Analysis.TokenFilters
{
	using FuncTokenFilters = Func<string, TokenFiltersDescriptor, IPromise<ITokenFilters>>;

	public static class TokenFilterTests
	{
		public class AsciiFoldingTests : TokenFilterAssertionBase<AsciiFoldingTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.AsciiFolding(n, t => t.PreserveOriginal());
			public override ITokenFilter Initializer => new AsciiFoldingTokenFilter { PreserveOriginal = true };
			public override object Json => new { type = "asciifolding", preserve_original = true };
			public override string Name => "ascii";
		}

		public class CommonGramsTests : TokenFilterAssertionBase<CommonGramsTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.CommonGrams(n, t => t.CommonWords("x", "y", "z").IgnoreCase().QueryMode());

			public override ITokenFilter Initializer =>
				new CommonGramsTokenFilter { QueryMode = true, IgnoreCase = true, CommonWords = new[] { "x", "y", "z" } };

			public override object Json => new
			{
				type = "common_grams",
				common_words = new[] { "x", "y", "z" },
				ignore_case = true,
				query_mode = true
			};

			public override string Name => "mycomgram";
		}

		public class DelimitedPayloadFilterTests : TokenFilterAssertionBase<DelimitedPayloadFilterTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.DelimitedPayload(n, t => t.Delimiter('-').Encoding(DelimitedPayloadEncoding.Identity));

			public override ITokenFilter Initializer =>
				new DelimitedPayloadTokenFilter { Delimiter = '-', Encoding = DelimitedPayloadEncoding.Identity };

			public override object Json => new { type = "delimited_payload", delimiter = "-", encoding = "identity" };
			public override string Name => "mydp";
		}

		public class DictionaryDecompounderTests : TokenFilterAssertionBase<DictionaryDecompounderTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.DictionaryDecompounder(n, t => t
					.MaxSubwordSize(2)
					.MinSubwordSize(2)
					.MinWordSize(2)
					.OnlyLongestMatch()
					.WordList("x", "y", "z")
				);

			public override ITokenFilter Initializer =>
				new DictionaryDecompounderTokenFilter
				{
					MinWordSize = 2,
					MinSubwordSize = 2,
					MaxSubwordSize = 2,
					OnlyLongestMatch = true,
					WordList = new[] { "x", "y", "z" }
				};

			public override object Json => new
			{
				type = "dictionary_decompounder",
				word_list = new[] { "x", "y", "z" },
				min_word_size = 2,
				min_subword_size = 2,
				max_subword_size = 2,
				only_longest_match = true
			};

			public override string Name => "dcc";
		}

		public class EdgeNgramTests : TokenFilterAssertionBase<EdgeNgramTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.EdgeNGram(n, t => t.MaxGram(2).MinGram(1));

			public override ITokenFilter Initializer => new EdgeNGramTokenFilter { MaxGram = 2, MinGram = 1 };

			public override object Json => new { type = "edge_ngram", min_gram = 1, max_gram = 2 };
			public override string Name => "etf";
		}

		[SkipVersion("<7.8.0", "PreserveOriginal introduced in 7.8.0")]
		public class EdgeNgramPreserveOriginalTests : TokenFilterAssertionBase<EdgeNgramPreserveOriginalTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.EdgeNGram(n, t => t.MaxGram(2).MinGram(1).PreserveOriginal());

			public override ITokenFilter Initializer => new EdgeNGramTokenFilter { MaxGram = 2, MinGram = 1, PreserveOriginal = true };

			public override object Json => new { type = "edge_ngram", min_gram = 1, max_gram = 2, preserve_original = true };
			public override string Name => "etfpo";
		}

		public class ElisionTests : TokenFilterAssertionBase<ElisionTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Elision(n, t => t.Articles("a", "b", "c").ArticlesCase());

			public override ITokenFilter Initializer => new ElisionTokenFilter { Articles = new[] { "a", "b", "c" }, ArticlesCase = true };

			public override object Json => new { type = "elision", articles = new[] { "a", "b", "c" }, articles_case = true };
			public override string Name => "el";
		}

		public class HunspellTests : TokenFilterAssertionBase<HunspellTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.Hunspell(n, t => t
					.Dedup()
					.Dictionary("path_to_dict")
					.Locale("en_US")
					.LongestOnly()
				);

			public override ITokenFilter Initializer =>
				new HunspellTokenFilter
				{
					Dedup = true,
					Dictionary = "path_to_dict",
					Locale = "en_US",
					LongestOnly = true
				};

			public override object Json => new
			{
				type = "hunspell",
				locale = "en_US",
				dictionary = "path_to_dict",
				dedup = true,
				longest_only = true
			};

			public override string Name => "huns";
		}

		public class HyphenationDecompounderTests : TokenFilterAssertionBase<HyphenationDecompounderTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.HyphenationDecompounder(n, t => t
					.MaxSubwordSize(2)
					.MinSubwordSize(2)
					.MinWordSize(2)
					.OnlyLongestMatch()
					.WordList("x", "y", "z")
					.HyphenationPatternsPath("analysis/fop.xml")
				);

			public override ITokenFilter Initializer =>
				new HyphenationDecompounderTokenFilter
				{
					MaxSubwordSize = 2,
					MinSubwordSize = 2,
					MinWordSize = 2,
					OnlyLongestMatch = true,
					WordList = new[] { "x", "y", "z" },
					HyphenationPatternsPath = "analysis/fop.xml"
				};

			public override object Json => new
			{
				type = "hyphenation_decompounder",
				word_list = new[] { "x", "y", "z" },
				min_word_size = 2,
				min_subword_size = 2,
				max_subword_size = 2,
				only_longest_match = true,
				hyphenation_patterns_path = "analysis/fop.xml"
			};

			public override string Name => "hyphdecomp";
		}

		public class KeepTypesTests : TokenFilterAssertionBase<KeepTypesTests>
		{
			private readonly string[] _types = { "<NUM>", "<SOMETHINGELSE>" };

			public override FuncTokenFilters Fluent => (n, tf) => tf.KeepTypes(n, t => t.Types(_types));

			public override ITokenFilter Initializer => new KeepTypesTokenFilter { Types = _types };

			public override object Json => new { type = "keep_types", types = _types };
			public override string Name => "keeptypes";
		}

		[SkipVersion("<6.4.0", "The mode option was introduced in https://github.com/elastic/elasticsearch/pull/32012")]
		public class KeepTypesModeTests : TokenFilterAssertionBase<KeepTypesTests>
		{
			private readonly string[] _types = { "<NUM>", "<SOMETHINGELSE>" };

			public override FuncTokenFilters Fluent => (n, tf) => tf.KeepTypes(n, t => t
				.Mode(KeepTypesMode.Exclude)
				.Types(_types)
			);

			public override ITokenFilter Initializer => new KeepTypesTokenFilter
			{
				Mode = KeepTypesMode.Exclude,
				Types = _types
			};

			public override object Json => new { type = "keep_types", types = _types, mode = "exclude" };
			public override string Name => "keeptypes_mode";
		}

		public class IcuCollationTests : TokenFilterAssertionBase<IcuCollationTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
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

			public override ITokenFilter Initializer =>
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

			public override object Json => new
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

			public override string Name => "icuc";
		}

		public class IcuFoldingTests : TokenFilterAssertionBase<IcuFoldingTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.IcuFolding(n, t => t.UnicodeSetFilter("[^åäöÅÄÖ]"));

			public override ITokenFilter Initializer =>
				new IcuFoldingTokenFilter { UnicodeSetFilter = "[^åäöÅÄÖ]" };

			public override object Json => new
			{
				type = "icu_folding",
				unicode_set_filter = "[^åäöÅÄÖ]"
			};

			public override string Name => "icuf";
		}

		public class IcuNormalizerTests : TokenFilterAssertionBase<IcuNormalizerTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.IcuNormalization(n, t => t.Name(IcuNormalizationType.Canonical));

			public override ITokenFilter Initializer => new IcuNormalizationTokenFilter { Name = IcuNormalizationType.Canonical };

			public override object Json => new
			{
				name = "nfc",
				type = "icu_normalizer"
			};

			public override string Name => "icun";
		}

		public class IcuTransformTests : TokenFilterAssertionBase<IcuTransformTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.IcuTransform(n, t => t
					.Direction(IcuTransformDirection.Forward)
					.Id("Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC")
				);

			public override ITokenFilter Initializer =>
				new IcuTransformTokenFilter
				{
					Direction = IcuTransformDirection.Forward,
					Id = "Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC"
				};

			public override object Json => new
			{
				dir = "forward",
				id = "Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC",
				type = "icu_transform"
			};

			public override string Name => "icut";
		}

		public class KeepwordsTests : TokenFilterAssertionBase<KeepwordsTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.KeepWords(n, t => t
					.KeepWords("a", "b", "c")
					.KeepWordsCase()
				);

			public override ITokenFilter Initializer =>
				new KeepWordsTokenFilter { KeepWordsCase = true, KeepWords = new[] { "a", "b", "c" } };

			public override object Json => new
			{
				type = "keep",
				keep_words = new[] { "a", "b", "c" },
				keep_words_case = true
			};

			public override string Name => "keepwords";
		}

		public class MarkerTests : TokenFilterAssertionBase<MarkerTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.KeywordMarker("marker", t => t
					.IgnoreCase()
					.Keywords("a", "b")
				);

			public override ITokenFilter Initializer => new KeywordMarkerTokenFilter { IgnoreCase = true, Keywords = new[] { "a", "b" } };

			public override object Json => new
			{
				type = "keyword_marker",
				keywords = new[] { "a", "b" },
				ignore_case = true,
			};

			public override string Name => "marker";
		}

		public class MarkerWithPatternsTests : TokenFilterAssertionBase<MarkerWithPatternsTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.KeywordMarker("marker_patterns", t => t
					.IgnoreCase()
					.KeywordsPattern(".*")
				);

			public override ITokenFilter Initializer => new KeywordMarkerTokenFilter { IgnoreCase = true, KeywordsPattern = ".*" };

			public override object Json => new
			{
				type = "keyword_marker",
				ignore_case = true,
				keywords_pattern = ".*"
			};

			public override string Name => "marker_patterns";
		}

		public class KuromojiReadingFormTests : TokenFilterAssertionBase<KuromojiReadingFormTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.KuromojiReadingForm(n, t => t.UseRomaji());

			public override ITokenFilter Initializer => new KuromojiReadingFormTokenFilter { UseRomaji = true };

			public override object Json => new
			{
				type = "kuromoji_readingform",
				use_romaji = true
			};

			public override string Name => "kfr";
		}

		public class KuromojiPartOfSpeechTests : TokenFilterAssertionBase<KuromojiPartOfSpeechTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.KuromojiPartOfSpeech(n, t => t.StopTags("#  verb-main:", "動詞-自立"));

			public override ITokenFilter Initializer =>
				new KuromojiPartOfSpeechTokenFilter { StopTags = new[] { "#  verb-main:", "動詞-自立" } };

			public override object Json => new
			{
				stoptags = new[]
				{
					"#  verb-main:",
					"動詞-自立"
				},
				type = "kuromoji_part_of_speech"
			};

			public override string Name => "kpos";
		}

		public class KuromojiStemmerTests : TokenFilterAssertionBase<KuromojiStemmerTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.KuromojiStemmer(n, t => t.MinimumLength(4));

			public override ITokenFilter Initializer => new KuromojiStemmerTokenFilter { MinimumLength = 4 };

			public override object Json => new
			{
				minimum_length = 4,
				type = "kuromoji_stemmer"
			};

			public override string Name => "ks";
		}

		public class KStemTests : TokenFilterAssertionBase<KStemTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.KStem(n);
			public override ITokenFilter Initializer => new KStemTokenFilter();
			public override object Json => new { type = "kstem" };
			public override string Name => "kstem";
		}

		public class LengthTests : TokenFilterAssertionBase<LengthTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Length(n, t => t.Max(200).Min(10));
			public override ITokenFilter Initializer => new LengthTokenFilter { Min = 10, Max = 200 };
			public override object Json => new { type = "length", min = 10, max = 200 };
			public override string Name => "length";
		}

		public class LimitTests : TokenFilterAssertionBase<LimitTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.LimitTokenCount(n, t => t.ConsumeAllToken().MaxTokenCount(12));

			public override ITokenFilter Initializer => new LimitTokenCountTokenFilter { ConsumeAllTokens = true, MaxTokenCount = 12 };

			public override object Json => new
			{
				type = "limit",
				max_token_count = 12,
				consume_all_tokens = true
			};

			public override string Name => "limit";
		}

		public class LowercaseTests : TokenFilterAssertionBase<LowercaseTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Lowercase(n);

			public override ITokenFilter Initializer => new LowercaseTokenFilter();

			public override object Json => new { type = "lowercase" };
			public override string Name => "lc";
		}

		public class NGramTests : TokenFilterAssertionBase<NGramTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.NGram(n, t => t.MinGram(3).MaxGram(4));

			public override ITokenFilter Initializer => new NGramTokenFilter { MinGram = 3, MaxGram = 4 };

			public override object Json => new { type = "ngram", min_gram = 3, max_gram = 4 };
			public override string Name => "ngram";
		}

		[SkipVersion("<7.8.0", "PreserveOriginal introduced in 7.8.0")]
		public class NGramPreserveOriginalTests : TokenFilterAssertionBase<NGramPreserveOriginalTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.NGram(n, t => t.MinGram(3).MaxGram(4).PreserveOriginal());

			public override ITokenFilter Initializer => new NGramTokenFilter { MinGram = 3, MaxGram = 4, PreserveOriginal = true };

			public override object Json => new { type = "ngram", min_gram = 3, max_gram = 4, preserve_original = true };
			public override string Name => "ngrampo";
		}

		public class PatternCaptureTests : TokenFilterAssertionBase<PatternCaptureTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.PatternCapture(n, t => t.Patterns(@"\d", @"\w").PreserveOriginal());

			public override ITokenFilter Initializer =>
				new PatternCaptureTokenFilter { Patterns = new[] { @"\d", @"\w" }, PreserveOriginal = true };

			public override object Json => new
			{
				type = "pattern_capture",
				patterns = new[] { "\\d", "\\w" },
				preserve_original = true
			};

			public override string Name => "pc";
		}

		public class PatternReplaceTests : TokenFilterAssertionBase<PatternReplaceTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.PatternReplace(n, t => t
					.Flags("CASE_INSENSITIVE")
					.Pattern(@"(\d|\w)")
					.Replacement("replacement")
				);

			public override ITokenFilter Initializer =>
				new PatternReplaceTokenFilter { Flags = "CASE_INSENSITIVE", Pattern = @"(\d|\w)", Replacement = "replacement" };

			public override object Json => new
			{
				type = "pattern_replace",
				flags = "CASE_INSENSITIVE",
				pattern = "(\\d|\\w)",
				replacement = "replacement"
			};

			public override string Name => "pr";
		}

		public class PorterStemTests : TokenFilterAssertionBase<PorterStemTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.PorterStem(n);
			public override ITokenFilter Initializer => new PorterStemTokenFilter();
			public override object Json => new { type = "porter_stem" };
			public override string Name => "porter";
		}

		public class ReverseTests : TokenFilterAssertionBase<ReverseTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Reverse(n);
			public override ITokenFilter Initializer => new ReverseTokenFilter();
			public override object Json => new { type = "reverse" };
			public override string Name => "rev";
		}

		public class ShingleTests : TokenFilterAssertionBase<ShingleTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.Shingle(n, t => t
					.FillerToken("x")
					.MaxShingleSize(10)
					.MinShingleSize(8)
					.OutputUnigrams()
					.OutputUnigramsIfNoShingles()
					.TokenSeparator("|")
				);

			public override ITokenFilter Initializer => new ShingleTokenFilter
			{
				FillerToken = "x",
				MaxShingleSize = 10,
				MinShingleSize = 8,
				OutputUnigrams = true,
				OutputUnigramsIfNoShingles = true,
				TokenSeparator = "|"
			};

			public override object Json => new
			{
				type = "shingle",
				min_shingle_size = 8,
				max_shingle_size = 10,
				output_unigrams = true,
				output_unigrams_if_no_shingles = true,
				token_separator = "|",
				filler_token = "x"
			};

			public override string Name => "shing";
		}

		public class SnowballTests : TokenFilterAssertionBase<SnowballTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Snowball(n, t => t.Language(SnowballLanguage.Dutch));

			public override ITokenFilter Initializer => new SnowballTokenFilter { Language = SnowballLanguage.Dutch };

			public override object Json => new
			{
				type = "snowball",
				language = "Dutch"
			};

			public override string Name => "snow";
		}

		public class StemmerTests : TokenFilterAssertionBase<StemmerTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Stemmer(n, t => t.Language("arabic"));

			public override ITokenFilter Initializer => new StemmerTokenFilter { Language = "arabic" };

			public override object Json => new
			{
				type = "stemmer",
				language = "arabic"
			};

			public override string Name => "stem";
		}

		[SkipVersion("<6.5.0", "predicate token filter not available in earlier versions")]
		public class PredicateTests : TokenFilterAssertionBase<PredicateTests>
		{
			private readonly string _predicate = "token.getTerm().length() > 5";

			public override FuncTokenFilters Fluent => (n, tf) => tf.Predicate(n, t => t.Script(_predicate));

			public override ITokenFilter Initializer => new PredicateTokenFilter { Script = new InlineScript(_predicate) };

			public override object Json => new
			{
				type = "predicate_token_filter",
				script = new
				{
					source = _predicate
				}
			};

			public override string Name => "predicate";
		}


		public class StemmerOverrideTests : TokenFilterAssertionBase<StemmerOverrideTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.StemmerOverride(n, t => t.RulesPath("analysis/custom_stems.txt"));

			public override ITokenFilter Initializer => new StemmerOverrideTokenFilter { RulesPath = "analysis/custom_stems.txt" };

			public override object Json => new
			{
				type = "stemmer_override",
				rules_path = "analysis/custom_stems.txt"
			};

			public override string Name => "stemo";
		}

		public class StopTests : TokenFilterAssertionBase<StopTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.Stop(n, t => t
					.IgnoreCase()
					.RemoveTrailing()
					.StopWords("x", "y", "z")
				);

			public override ITokenFilter Initializer =>
				new StopTokenFilter { IgnoreCase = true, RemoveTrailing = true, StopWords = new[] { "x", "y", "z" } };

			public override object Json => new
			{
				type = "stop",
				stopwords = new[] { "x", "y", "z" },
				ignore_case = true,
				remove_trailing = true
			};

			public override string Name => "stop";
		}

		public class SynonymTests : TokenFilterAssertionBase<SynonymTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.Synonym(n, t => t
					.Expand()
					.Format(SynonymFormat.WordNet)
					.SynonymsPath("analysis/stopwords.txt")
					.Synonyms("x=>y", "z=>s")
					.Tokenizer("whitespace")
				);

			public override ITokenFilter Initializer =>
				new SynonymTokenFilter
				{
					Expand = true,
					Format = SynonymFormat.WordNet,
					SynonymsPath = "analysis/stopwords.txt",
					Synonyms = new[] { "x=>y", "z=>s" },
					Tokenizer = "whitespace"
				};

			public override object Json => new
			{
				type = "synonym",
				synonyms_path = "analysis/stopwords.txt",
				format = "wordnet",
				synonyms = new[] { "x=>y", "z=>s" },
				expand = true,
				tokenizer = "whitespace"
			};

			public override string Name => "syn";
		}

		[SkipVersion("<6.4.0", "Lenient is an option introduced in 6.4.0")]
		public class SynonymLenientTests : TokenFilterAssertionBase<SynonymLenientTests>
		{
			private readonly string[] _synonyms = { "foo", "bar => baz" };

			public override FuncTokenFilters Fluent => (n, tf) => tf
				.Synonym(n, t => t
					.Lenient()
					.Synonyms(_synonyms)
				);

			public override ITokenFilter Initializer =>
				new SynonymTokenFilter
				{
					Lenient = true,
					Synonyms = _synonyms
				};

			public override object Json => new
			{
				type = "synonym",
				synonyms = _synonyms,
				lenient = true,
			};

			public override string Name => "syn_lenient";
		}

		[SkipVersion("<7.3.0", "updateable introduced in 7.3.0")]
		public class SynonymUpdateableTests : TokenFilterAssertionBase<SynonymTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.Synonym(n, t => t
					.SynonymsPath("analysis/stopwords.txt")
					.Updateable()
				);

			public override ITokenFilter Initializer =>
				new SynonymTokenFilter
				{
					SynonymsPath = "analysis/stopwords.txt",
					Updateable = true
				};

			public override object Json => new
			{
				type = "synonym",
				synonyms_path = "analysis/stopwords.txt",
				updateable = true
			};

			public override string Name => "syn_updateable";
		}

		public class SynonymGraphTests : TokenFilterAssertionBase<SynonymGraphTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.SynonymGraph(n, t => t
					.Expand()
					.Format(SynonymFormat.WordNet)
					.SynonymsPath("analysis/stopwords.txt")
					.Synonyms("x=>y", "z=>s")
					.Tokenizer("whitespace")
				);

			public override ITokenFilter Initializer =>
				new SynonymGraphTokenFilter
				{
					Expand = true,
					Format = SynonymFormat.WordNet,
					SynonymsPath = "analysis/stopwords.txt",
					Synonyms = new[] { "x=>y", "z=>s" },
					Tokenizer = "whitespace"
				};

			public override object Json => new
			{
				type = "synonym_graph",
				synonyms_path = "analysis/stopwords.txt",
				format = "wordnet",
				synonyms = new[] { "x=>y", "z=>s" },
				expand = true,
				tokenizer = "whitespace"
			};

			public override string Name => "syn_graph";
		}

		[SkipVersion("<7.3.0", "updateable introduced in 7.3.0")]
		public class SynonymGraphUpdateableTests : TokenFilterAssertionBase<SynonymGraphUpdateableTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.SynonymGraph(n, t => t
					.SynonymsPath("analysis/stopwords.txt")
					.Updateable()
				);

			public override ITokenFilter Initializer =>
				new SynonymGraphTokenFilter
				{
					SynonymsPath = "analysis/stopwords.txt",
					Updateable = true
				};

			public override object Json => new
			{
				type = "synonym_graph",
				synonyms_path = "analysis/stopwords.txt",
				updateable = true,
			};

			public override string Name => "syn_graph_updateable";
		}

		public class TrimTests : TokenFilterAssertionBase<TrimTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Trim(n);
			public override ITokenFilter Initializer => new TrimTokenFilter();
			public override object Json => new { type = "trim" };
			public override string Name => "trimmer";
		}

		public class TruncateTests : TokenFilterAssertionBase<TruncateTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Truncate(n, t => t.Length(100));
			public override ITokenFilter Initializer => new TruncateTokenFilter { Length = 100 };
			public override object Json => new { type = "truncate", length = 100 };
			public override string Name => "truncer";
		}

		public class UniqueTests : TokenFilterAssertionBase<UniqueTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Unique(n, t => t.OnlyOnSamePosition());
			public override ITokenFilter Initializer => new UniqueTokenFilter { OnlyOnSamePosition = true, };
			public override object Json => new { type = "unique", only_on_same_position = true };
			public override string Name => "uq";
		}

		public class UppercaseTests : TokenFilterAssertionBase<UppercaseTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.Uppercase(n);
			public override ITokenFilter Initializer => new UppercaseTokenFilter();
			public override object Json => new { type = "uppercase" };
			public override string Name => "upper";
		}

		public class WordDelimiterTests : TokenFilterAssertionBase<WordDelimiterTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
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

			public override ITokenFilter Initializer =>
				new WordDelimiterTokenFilter
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
				};

			public override object Json => new
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
			};

			public override string Name => "wd";
		}

		public class WordDelimiterGraphTests : TokenFilterAssertionBase<WordDelimiterGraphTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.WordDelimiterGraph(n, t => t
					.AdjustOffsets()
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
				);

			public override ITokenFilter Initializer =>
				new WordDelimiterGraphTokenFilter
				{
					AdjustOffsets = true,
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
				};

			public override object Json => new
			{
				type = "word_delimiter_graph",
				adjust_offsets = true,
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
			};

			public override string Name => "wdg";
		}

		public class PhoneticTests : TokenFilterAssertionBase<PhoneticTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
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

			public override ITokenFilter Initializer =>
				new PhoneticTokenFilter
				{
					Encoder = PhoneticEncoder.Beidermorse,
					RuleType = PhoneticRuleType.Exact,
					NameType = PhoneticNameType.Sephardic,
					LanguageSet = new[] { PhoneticLanguage.Cyrillic, PhoneticLanguage.English, PhoneticLanguage.Hebrew }
				};

			public override object Json => new
			{
				type = "phonetic",
				encoder = "beider_morse",
				rule_type = "exact",
				name_type = "sephardic",
				languageset = new[] { "cyrillic", "english", "hebrew" }
			};

			public override string Name => "phonetic";
		}

		[SkipVersion("<6.4.0", "analysis-nori plugin introduced in 6.4.0")]
		public class NoriPartOfSpeechTests : TokenFilterAssertionBase<NoriPartOfSpeechTests>
		{
			private readonly string[] _stopTags = { "NR", "SP" };

			public override FuncTokenFilters Fluent => (n, tf) => tf.NoriPartOfSpeech(n, t => t.StopTags(_stopTags));

			public override ITokenFilter Initializer => new NoriPartOfSpeechTokenFilter { StopTags = _stopTags };

			public override object Json => new { type = "nori_part_of_speech", stoptags = _stopTags };
			public override string Name => "nori_pos";
		}

		[SkipVersion("<6.5.0", "Introduced in 6.5.0")]
		public class ConditionTests : TokenFilterAssertionBase<ConditionTests>
		{
			private readonly string _predicate = "token.getTerm().length() < 5";

			public override FuncTokenFilters Fluent => (n, tf) => tf
				.Condition(n, t => t
					.Filters("lowercase", "lowercase, porter_stem")
					.Script(_predicate)
				);

			public override ITokenFilter Initializer => new ConditionTokenFilter
			{
				Filters = new[] { "lowercase", "lowercase, porter_stem" },
				Script = new InlineScript(_predicate)
			};

			public override object Json => new
			{
				type = "condition",
				filter = new[] { "lowercase", "lowercase, porter_stem" },
				script = new { source = _predicate }
			};

			public override string Name => "condition";
		}

		[SkipVersion("<6.4.0", "Introduced in 6.4.0")]
		public class MultiplexerTests : TokenFilterAssertionBase<MultiplexerTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf
				.Multiplexer(n, t => t
					.Filters("lowercase", "lowercase, porter_stem")
					.PreserveOriginal()
				);

			public override ITokenFilter Initializer => new MultiplexerTokenFilter
			{
				Filters = new[] { "lowercase", "lowercase, porter_stem" },
				PreserveOriginal = true
			};

			public override object Json => new
			{
				type = "multiplexer",
				filters = new[] { "lowercase", "lowercase, porter_stem" },
				preserve_original = true
			};

			public override string Name => "multiplexer";
		}

		[SkipVersion("<6.4.0", "Introduced in 6.4.0")]
		public class RemoveDuplicatesTests : TokenFilterAssertionBase<RemoveDuplicatesTests>
		{
			public override FuncTokenFilters Fluent => (n, tf) => tf.RemoveDuplicates(n);
			public override ITokenFilter Initializer => new RemoveDuplicatesTokenFilter();
			public override object Json => new { type = "remove_duplicates" };
			public override string Name => "dupes";
		}

		public class UserDefinedTokenFilterTests
		{
			public class UserDefinedTokenFilter : TokenFilterBase
			{
				public UserDefinedTokenFilter(string type) : base(type)
				{
				}

				[DataMember(Name = "string_property")]
				public string StringProperty { get; set; }

				[DataMember(Name = "int_property")]
				public int? IntProperty { get; set; }
			}

			private static string FilterName => "user_defined";

			private static ITokenFilter UserDefinedFilter => new UserDefinedTokenFilter(FilterName) { StringProperty = "string", IntProperty = 1 };

			[U] public void Fluent() =>
				SerializationTestHelper.Expect(Json).FromRequest(c => c
					.Indices.Create("index", ci => ci
						.Settings(s => s
							.Analysis(a => a
								.TokenFilters(t => t
									.UserDefined(FilterName, UserDefinedFilter)
								)
							)
						)
					)
				);

			[U] public void Initializer() =>
				SerializationTestHelper.Expect(Json).FromRequest(c => c
					.Indices.Create(new CreateIndexRequest("index")
						{
							Settings = new IndexSettings
							{
								Analysis = new Nest.Analysis
								{
									TokenFilters = new Nest.TokenFilters
									{
										{ FilterName, UserDefinedFilter }
									}
								}
							}
						}
					)
				);

			private static object Json => new {
				settings = new {
					analysis = new {
						filter = new {
							user_defined = new {
								int_property = 1,
								string_property = "string",
								type = "user_defined"
							}
						}
					}
				}
			};

		}
	}
}
