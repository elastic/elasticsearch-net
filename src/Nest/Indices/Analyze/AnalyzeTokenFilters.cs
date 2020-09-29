// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A list of string references to stored token filters and/or inline token filter definitions
	/// </summary>
	[JsonFormatter(typeof(UnionListFormatter<AnalyzeTokenFilters, string, ITokenFilter>))]
	public class AnalyzeTokenFilters : List<Union<string, ITokenFilter>>
	{
		public AnalyzeTokenFilters() { }

		public AnalyzeTokenFilters(List<Union<string, ITokenFilter>> tokenFilters)
		{
			if (tokenFilters == null) return;

			foreach (var v in tokenFilters) this.AddIfNotNull(v);
		}

		public AnalyzeTokenFilters(string[] tokenFilters)
		{
			if (tokenFilters == null) return;

			foreach (var v in tokenFilters) this.AddIfNotNull(v);
		}

		public void Add(ITokenFilter filter) => Add(new Union<string, ITokenFilter>(filter));

		public static implicit operator AnalyzeTokenFilters(TokenFilterBase tokenFilter) =>
			tokenFilter == null ? null : new AnalyzeTokenFilters { tokenFilter };

		public static implicit operator AnalyzeTokenFilters(string tokenFilter) =>
			tokenFilter == null ? null : new AnalyzeTokenFilters { tokenFilter };

		public static implicit operator AnalyzeTokenFilters(string[] tokenFilters) =>
			tokenFilters == null ? null : new AnalyzeTokenFilters(tokenFilters);
	}

	public class AnalyzeTokenFiltersDescriptor : DescriptorPromiseBase<AnalyzeTokenFiltersDescriptor, AnalyzeTokenFilters>
	{
		public AnalyzeTokenFiltersDescriptor() : base(new AnalyzeTokenFilters()) { }

		/// <summary>
		/// A reference to a token filter that is part of the mapping
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Name(string tokenFilter) => Assign(tokenFilter, (a, v) => a.AddIfNotNull(v));

		private AnalyzeTokenFiltersDescriptor AssignIfNotNull(ITokenFilter filter) =>
			Assign(filter, (a, v) => { if (v != null) a.Add(v); });

		/// <summary>
		/// Token filters that allow to decompose compound words using a dictionary
		/// </summary>
		public AnalyzeTokenFiltersDescriptor DictionaryDecompounder(
			Func<DictionaryDecompounderTokenFilterDescriptor, IDictionaryDecompounderTokenFilter> selector
		) =>
			AssignIfNotNull(selector?.Invoke(new DictionaryDecompounderTokenFilterDescriptor()));

		/// <summary>
		/// Token filters that allow to decompose compound words using FOP XML
		/// </summary>
		public AnalyzeTokenFiltersDescriptor HyphenationDecompounder(
			Func<HyphenationDecompounderTokenFilterDescriptor, IHyphenationDecompounderTokenFilter> selector
		) =>
			AssignIfNotNull(selector?.Invoke(new HyphenationDecompounderTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type edgeNGram.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor EdgeNGram(Func<EdgeNGramTokenFilterDescriptor, IEdgeNGramTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new EdgeNGramTokenFilterDescriptor()));

		/// <summary>
		/// The phonetic token filter is provided as a plugin.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Phonetic(Func<PhoneticTokenFilterDescriptor, IPhoneticTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new PhoneticTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type shingle that constructs shingles (token n-grams) from a token stream.
		/// <para>In other words, it creates combinations of tokens as a single token. </para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Shingle(Func<ShingleTokenFilterDescriptor, IShingleTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new ShingleTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type stop that removes stop words from token streams.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Stop(Func<StopTokenFilterDescriptor, IStopTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new StopTokenFilterDescriptor()));

		/// <summary>
		/// The synonym token filter allows to easily handle synonyms during the analysis process.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Synonym(Func<SynonymTokenFilterDescriptor, ISynonymTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new SynonymTokenFilterDescriptor()));

		/// <summary>
		/// The synonym_graph token filter allows to easily handle synonyms,
		/// including multi-word synonyms correctly during the analysis process.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor SynonymGraph(Func<SynonymGraphTokenFilterDescriptor, ISynonymGraphTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new SynonymGraphTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type asciifolding that converts alphabetic, numeric, and symbolic Unicode characters which are
		/// <para> not in the first 127 ASCII characters (the “Basic Latin” Unicode block) into their ASCII equivalents, if one exists.</para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor WordDelimiter(Func<WordDelimiterTokenFilterDescriptor, IWordDelimiterTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new WordDelimiterTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type asciifolding that converts alphabetic, numeric, and symbolic Unicode characters which are
		/// <para> not in the first 127 ASCII characters (the “Basic Latin” Unicode block) into their ASCII equivalents, if one exists.</para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor WordDelimiterGraph(Func<WordDelimiterGraphTokenFilterDescriptor, IWordDelimiterGraphTokenFilter> selector
		) =>
			AssignIfNotNull(selector?.Invoke(new WordDelimiterGraphTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type asciifolding that converts alphabetic, numeric, and symbolic Unicode characters which are
		/// <para> not in the first 127 ASCII characters (the “Basic Latin” Unicode block) into their ASCII equivalents, if one exists.</para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor AsciiFolding(Func<AsciiFoldingTokenFilterDescriptor, IAsciiFoldingTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new AsciiFoldingTokenFilterDescriptor()));

		/// <summary>
		///  Token filter that generates bigrams for frequently occuring terms. Single terms are still indexed.
		/// <para>Note, common_words or common_words_path field is required.</para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor CommonGrams(Func<CommonGramsTokenFilterDescriptor, ICommonGramsTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new CommonGramsTokenFilterDescriptor()));

		/// <summary>
		/// Splits tokens into tokens and payload whenever a delimiter character is found.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor DelimitedPayload(Func<DelimitedPayloadTokenFilterDescriptor, IDelimitedPayloadTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new DelimitedPayloadTokenFilterDescriptor()));

		/// <summary>
		/// A token filter which removes elisions. For example, “l’avion” (the plane) will tokenized as “avion” (plane).
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Elision(Func<ElisionTokenFilterDescriptor, IElisionTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new ElisionTokenFilterDescriptor()));

		/// <summary>
		///  Basic support for hunspell stemming.
		/// <para> Hunspell dictionaries will be picked up from a dedicated hunspell directory on the filesystem.</para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Hunspell(Func<HunspellTokenFilterDescriptor, IHunspellTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new HunspellTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor KeepTypes(Func<KeepTypesTokenFilterDescriptor, IKeepTypesTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new KeepTypesTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor KeepWords(Func<KeepWordsTokenFilterDescriptor, IKeepWordsTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new KeepWordsTokenFilterDescriptor()));

		/// <summary>
		/// Protects words from being modified by stemmers. Must be placed before any stemming filters.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor KeywordMarker(Func<KeywordMarkerTokenFilterDescriptor, IKeywordMarkerTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new KeywordMarkerTokenFilterDescriptor()));

		/// <summary>
		/// The kstem token filter is a high performance filter for english.
		/// <para> All terms must already be lowercased (use lowercase filter) for this filter to work correctly.</para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor KStem(Func<KStemTokenFilterDescriptor, IKStemTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new KStemTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type length that removes words that are too long or too short for the stream.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Length(Func<LengthTokenFilterDescriptor, ILengthTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new LengthTokenFilterDescriptor()));

		/// <summary>
		/// Limits the number of tokens that are indexed per document and field.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor LimitTokenCount(Func<LimitTokenCountTokenFilterDescriptor, ILimitTokenCountTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new LimitTokenCountTokenFilterDescriptor()));

		/// <summary>
		///  A token filter of type lowercase that normalizes token text to lower case.
		/// <para> Lowercase token filter supports Greek and Turkish lowercase token filters through the language parameter.</para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Lowercase(Func<LowercaseTokenFilterDescriptor, ILowercaseTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new LowercaseTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type nGram.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor NGram(Func<NGramTokenFilterDescriptor, INGramTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new NGramTokenFilterDescriptor()));

		/// <summary>
		/// The pattern_capture token filter, unlike the pattern tokenizer, emits a token for every capture group in the regular expression.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor PatternCapture(Func<PatternCaptureTokenFilterDescriptor, IPatternCaptureTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new PatternCaptureTokenFilterDescriptor()));

		/// <summary>
		/// The pattern_replace token filter allows to easily handle string replacements based on a regular expression.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor PatternReplace(Func<PatternReplaceTokenFilterDescriptor, IPatternReplaceTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new PatternReplaceTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type porterStem that transforms the token stream as per the Porter stemming algorithm.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor PorterStem(Func<PorterStemTokenFilterDescriptor, IPorterStemTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new PorterStemTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type reverse that simply reverses the tokens.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Reverse(Func<ReverseTokenFilterDescriptor, IReverseTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new ReverseTokenFilterDescriptor()));

		/// <summary>
		/// A filter that stems words using a Snowball-generated stemmer.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Snowball(Func<SnowballTokenFilterDescriptor, ISnowballTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new SnowballTokenFilterDescriptor()));

		/// <inheritdoc cref="IConditionTokenFilter" />
		public AnalyzeTokenFiltersDescriptor Condition(Func<ConditionTokenFilterDescriptor, IConditionTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new ConditionTokenFilterDescriptor()));

		/// <summary>
		/// A filter that stems words (similar to snowball, but with more options).
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Stemmer(Func<StemmerTokenFilterDescriptor, IStemmerTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new StemmerTokenFilterDescriptor()));

		/// <inheritdoc cref="IPredicateTokenFilter" />
		public AnalyzeTokenFiltersDescriptor Predicate(Func<PredicateTokenFilterDescriptor, IPredicateTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new PredicateTokenFilterDescriptor()));

		/// <summary>
		/// Overrides stemming algorithms, by applying a custom mapping, then protecting these terms from being modified by stemmers. Must be placed
		/// before any stemming filters.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor StemmerOverride(Func<StemmerOverrideTokenFilterDescriptor, IStemmerOverrideTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new StemmerOverrideTokenFilterDescriptor()));

		/// <summary>
		/// The trim token filter trims surrounding whitespaces around a token.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Trim(Func<TrimTokenFilterDescriptor, ITrimTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new TrimTokenFilterDescriptor()));

		/// <summary>
		/// The truncate token filter can be used to truncate tokens into a specific length. This can come in handy with keyword (single token)
		/// <para> based mapped fields that are used for sorting in order to reduce memory usage.</para>
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Truncate(Func<TruncateTokenFilterDescriptor, ITruncateTokenFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new TruncateTokenFilterDescriptor()));

		/// <summary>
		/// The unique token filter can be used to only index unique tokens during analysis. By default it is applied on all the token stream
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Unique(Func<UniqueTokenFilterDescriptor, IUniqueTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new UniqueTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type uppercase that normalizes token text to upper case.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Uppercase(Func<UppercaseTokenFilterDescriptor, IUppercaseTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new UppercaseTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type fingerprint The fingerprint token filter that emits a single token which is useful
		/// for fingerprinting a body of text, and/or providing a token that can be clustered on.
		/// It does this by sorting the tokens, deduplicating and then concatenating them back into a single token.
		/// </summary>
		public AnalyzeTokenFiltersDescriptor Fingerprint(Func<FingerprintTokenFilterDescriptor, IFingerprintTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new FingerprintTokenFilterDescriptor()));

		/// <summary>
		/// The kuromoji_stemmer token filter normalizes common katakana spelling variations ending in a
		/// long sound character by removing this character (U+30FC). Only full-width katakana characters are supported.
		/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
		/// </summary>
		public AnalyzeTokenFiltersDescriptor
			KuromojiStemmer(Func<KuromojiStemmerTokenFilterDescriptor, IKuromojiStemmerTokenFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new KuromojiStemmerTokenFilterDescriptor()));

		/// <summary>
		/// The kuromoji_readingform token filter replaces the token with its reading form in either katakana or romaji.
		/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
		/// </summary>
		public AnalyzeTokenFiltersDescriptor KuromojiReadingForm(
			Func<KuromojiReadingFormTokenFilterDescriptor, IKuromojiReadingFormTokenFilter> selector
		) =>
			AssignIfNotNull(selector.Invoke(new KuromojiReadingFormTokenFilterDescriptor()));

		/// <summary>
		/// The kuromoji_part_of_speech token filter removes tokens that match a set of part-of-speech tags.
		/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
		/// </summary>
		public AnalyzeTokenFiltersDescriptor KuromojiPartOfSpeech(
			Func<KuromojiPartOfSpeechTokenFilterDescriptor, IKuromojiPartOfSpeechTokenFilter> selector
		) =>
			AssignIfNotNull(selector.Invoke(new KuromojiPartOfSpeechTokenFilterDescriptor()));


		/// <summary>
		/// Collations are used for sorting documents in a language-specific word order. The icu_collation token filter is available to all indices and
		/// defaults to using the DUCET collation, which is a best-effort attempt at language-neutral sorting.
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public AnalyzeTokenFiltersDescriptor IcuCollation(Func<IcuCollationTokenFilterDescriptor, IIcuCollationTokenFilter> selector) =>
			AssignIfNotNull(selector.Invoke(new IcuCollationTokenFilterDescriptor()));

		/// <summary>
		/// Case folding of Unicode characters based on UTR#30, like the ASCII-folding token filter on steroids.
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public AnalyzeTokenFiltersDescriptor IcuFolding(Func<IcuFoldingTokenFilterDescriptor, IIcuFoldingTokenFilter> selector) =>
			AssignIfNotNull(selector.Invoke(new IcuFoldingTokenFilterDescriptor()));

		/// <summary>
		/// Normalizes as defined here: http://userguide.icu-project.org/transforms/normalization
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public AnalyzeTokenFiltersDescriptor IcuNormalization(Func<IcuNormalizationTokenFilterDescriptor, IIcuNormalizationTokenFilter> selector) =>
			AssignIfNotNull(selector.Invoke(new IcuNormalizationTokenFilterDescriptor()));

		/// <summary>
		/// Transforms are used to process Unicode text in many different ways, such as case mapping,
		/// normalization, transliteration and bidirectional text handling.
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public AnalyzeTokenFiltersDescriptor IcuTransform(Func<IcuTransformTokenFilterDescriptor, IIcuTransformTokenFilter> selector) =>
			AssignIfNotNull(selector.Invoke(new IcuTransformTokenFilterDescriptor()));

		/// <inheritdoc cref="INoriPartOfSpeechTokenFilter" />
		public AnalyzeTokenFiltersDescriptor NoriPartOfSpeech(Func<NoriPartOfSpeechTokenFilterDescriptor, INoriPartOfSpeechTokenFilter> selector) =>
			AssignIfNotNull(selector.Invoke(new NoriPartOfSpeechTokenFilterDescriptor()));

		/// <inheritdoc cref="IMultiplexerTokenFilter" />
		public AnalyzeTokenFiltersDescriptor Multiplexer(Func<MultiplexerTokenFilterDescriptor, IMultiplexerTokenFilter> selector) =>
			AssignIfNotNull(selector.Invoke(new MultiplexerTokenFilterDescriptor()));

		/// <inheritdoc cref="IRemoveDuplicatesTokenFilter" />
		public AnalyzeTokenFiltersDescriptor RemoveDuplicates(Func<RemoveDuplicatesTokenFilterDescriptor, IRemoveDuplicatesTokenFilter> selector) =>
			AssignIfNotNull(selector.Invoke(new RemoveDuplicatesTokenFilterDescriptor()));
	}
}
