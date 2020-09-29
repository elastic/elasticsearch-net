// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<TokenFilters, ITokenFilters, string, ITokenFilter>))]
	public interface ITokenFilters : IIsADictionary<string, ITokenFilter> { }

	public class TokenFilters : IsADictionaryBase<string, ITokenFilter>, ITokenFilters
	{
		public TokenFilters() { }

		public TokenFilters(IDictionary<string, ITokenFilter> container) : base(container) { }

		public TokenFilters(Dictionary<string, ITokenFilter> container) : base(container) { }

		public void Add(string name, ITokenFilter analyzer) => BackingDictionary.Add(name, analyzer);
	}

	public class TokenFiltersDescriptor : IsADictionaryDescriptorBase<TokenFiltersDescriptor, ITokenFilters, string, ITokenFilter>
	{
		public TokenFiltersDescriptor() : base(new TokenFilters()) { }

		public TokenFiltersDescriptor UserDefined(string name, ITokenFilter analyzer) => Assign(name, analyzer);

		/// <summary>
		/// Token filters that allow to decompose compound words using a dictionary
		/// </summary>
		public TokenFiltersDescriptor DictionaryDecompounder(string name,
			Func<DictionaryDecompounderTokenFilterDescriptor, IDictionaryDecompounderTokenFilter> selector
		) =>
			Assign(name, selector?.Invoke(new DictionaryDecompounderTokenFilterDescriptor()));

		/// <summary>
		/// Token filters that allow to decompose compound words using FOP XML
		/// </summary>
		public TokenFiltersDescriptor HyphenationDecompounder(string name,
			Func<HyphenationDecompounderTokenFilterDescriptor, IHyphenationDecompounderTokenFilter> selector
		) =>
			Assign(name, selector?.Invoke(new HyphenationDecompounderTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type edgeNGram.
		/// </summary>
		public TokenFiltersDescriptor EdgeNGram(string name, Func<EdgeNGramTokenFilterDescriptor, IEdgeNGramTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new EdgeNGramTokenFilterDescriptor()));

		/// <summary>
		/// The phonetic token filter is provided as a plugin.
		/// </summary>
		public TokenFiltersDescriptor Phonetic(string name, Func<PhoneticTokenFilterDescriptor, IPhoneticTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new PhoneticTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type shingle that constructs shingles (token n-grams) from a token stream.
		/// <para>In other words, it creates combinations of tokens as a single token. </para>
		/// </summary>
		public TokenFiltersDescriptor Shingle(string name, Func<ShingleTokenFilterDescriptor, IShingleTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new ShingleTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type stop that removes stop words from token streams.
		/// </summary>
		public TokenFiltersDescriptor Stop(string name, Func<StopTokenFilterDescriptor, IStopTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new StopTokenFilterDescriptor()));

		/// <summary>
		/// The synonym token filter allows to easily handle synonyms during the analysis process.
		/// </summary>
		public TokenFiltersDescriptor Synonym(string name, Func<SynonymTokenFilterDescriptor, ISynonymTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new SynonymTokenFilterDescriptor()));

		/// <summary>
		/// The synonym_graph token filter allows to easily handle synonyms,
		/// including multi-word synonyms correctly during the analysis process.
		/// </summary>
		public TokenFiltersDescriptor SynonymGraph(string name, Func<SynonymGraphTokenFilterDescriptor, ISynonymGraphTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new SynonymGraphTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type asciifolding that converts alphabetic, numeric, and symbolic Unicode characters which are
		/// <para>
		/// not in the first 127 ASCII characters (the “Basic Latin” Unicode block) into their ASCII equivalents, if one
		/// exists.
		/// </para>
		/// </summary>
		public TokenFiltersDescriptor WordDelimiter(string name, Func<WordDelimiterTokenFilterDescriptor, IWordDelimiterTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new WordDelimiterTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type asciifolding that converts alphabetic, numeric, and symbolic Unicode characters which are
		/// <para>
		/// not in the first 127 ASCII characters (the “Basic Latin” Unicode block) into their ASCII equivalents, if one
		/// exists.
		/// </para>
		/// </summary>
		public TokenFiltersDescriptor WordDelimiterGraph(string name,
			Func<WordDelimiterGraphTokenFilterDescriptor, IWordDelimiterGraphTokenFilter> selector
		) =>
			Assign(name, selector?.Invoke(new WordDelimiterGraphTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type asciifolding that converts alphabetic, numeric, and symbolic Unicode characters which are
		/// <para>
		/// not in the first 127 ASCII characters (the “Basic Latin” Unicode block) into their ASCII equivalents, if one
		/// exists.
		/// </para>
		/// </summary>
		public TokenFiltersDescriptor AsciiFolding(string name, Func<AsciiFoldingTokenFilterDescriptor, IAsciiFoldingTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new AsciiFoldingTokenFilterDescriptor()));

		/// <summary>
		///  Token filter that generates bigrams for frequently occuring terms. Single terms are still indexed.
		/// <para>Note, common_words or common_words_path field is required.</para>
		/// </summary>
		public TokenFiltersDescriptor CommonGrams(string name, Func<CommonGramsTokenFilterDescriptor, ICommonGramsTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new CommonGramsTokenFilterDescriptor()));

		/// <summary>
		/// Splits tokens into tokens and payload whenever a delimiter character is found.
		/// </summary>
		public TokenFiltersDescriptor DelimitedPayload(string name, Func<DelimitedPayloadTokenFilterDescriptor, IDelimitedPayloadTokenFilter> selector
		) =>
			Assign(name, selector?.Invoke(new DelimitedPayloadTokenFilterDescriptor()));

		/// <summary>
		/// A token filter which removes elisions. For example, “l’avion” (the plane) will tokenized as “avion” (plane).
		/// </summary>
		public TokenFiltersDescriptor Elision(string name, Func<ElisionTokenFilterDescriptor, IElisionTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new ElisionTokenFilterDescriptor()));

		/// <summary>
		///  Basic support for hunspell stemming.
		/// <para> Hunspell dictionaries will be picked up from a dedicated hunspell directory on the filesystem.</para>
		/// </summary>
		public TokenFiltersDescriptor Hunspell(string name, Func<HunspellTokenFilterDescriptor, IHunspellTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new HunspellTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
		/// </summary>
		public TokenFiltersDescriptor KeepTypes(string name, Func<KeepTypesTokenFilterDescriptor, IKeepTypesTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new KeepTypesTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
		/// </summary>
		public TokenFiltersDescriptor KeepWords(string name, Func<KeepWordsTokenFilterDescriptor, IKeepWordsTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new KeepWordsTokenFilterDescriptor()));

		/// <summary>
		/// Protects words from being modified by stemmers. Must be placed before any stemming filters.
		/// </summary>
		public TokenFiltersDescriptor KeywordMarker(string name, Func<KeywordMarkerTokenFilterDescriptor, IKeywordMarkerTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new KeywordMarkerTokenFilterDescriptor()));

		/// <summary>
		/// The kstem token filter is a high performance filter for english.
		/// <para> All terms must already be lowercased (use lowercase filter) for this filter to work correctly.</para>
		/// </summary>
		public TokenFiltersDescriptor KStem(string name, Func<KStemTokenFilterDescriptor, IKStemTokenFilter> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new KStemTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type length that removes words that are too long or too short for the stream.
		/// </summary>
		public TokenFiltersDescriptor Length(string name, Func<LengthTokenFilterDescriptor, ILengthTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new LengthTokenFilterDescriptor()));

		/// <summary>
		/// Limits the number of tokens that are indexed per document and field.
		/// </summary>
		public TokenFiltersDescriptor LimitTokenCount(string name, Func<LimitTokenCountTokenFilterDescriptor, ILimitTokenCountTokenFilter> selector
		) =>
			Assign(name, selector?.Invoke(new LimitTokenCountTokenFilterDescriptor()));

		/// <summary>
		///  A token filter of type lowercase that normalizes token text to lower case.
		/// <para> Lowercase token filter supports Greek and Turkish lowercase token filters through the language parameter.</para>
		/// </summary>
		public TokenFiltersDescriptor Lowercase(string name, Func<LowercaseTokenFilterDescriptor, ILowercaseTokenFilter> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new LowercaseTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type nGram.
		/// </summary>
		public TokenFiltersDescriptor NGram(string name, Func<NGramTokenFilterDescriptor, INGramTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new NGramTokenFilterDescriptor()));

		/// <summary>
		/// The pattern_capture token filter, unlike the pattern tokenizer, emits a token for every capture group in the regular
		/// expression.
		/// </summary>
		public TokenFiltersDescriptor PatternCapture(string name, Func<PatternCaptureTokenFilterDescriptor, IPatternCaptureTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new PatternCaptureTokenFilterDescriptor()));

		/// <summary>
		/// The pattern_replace token filter allows to easily handle string replacements based on a regular expression.
		/// </summary>
		public TokenFiltersDescriptor PatternReplace(string name, Func<PatternReplaceTokenFilterDescriptor, IPatternReplaceTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new PatternReplaceTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type porterStem that transforms the token stream as per the Porter stemming algorithm.
		/// </summary>
		public TokenFiltersDescriptor PorterStem(string name, Func<PorterStemTokenFilterDescriptor, IPorterStemTokenFilter> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new PorterStemTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type reverse that simply reverses the tokens.
		/// </summary>
		public TokenFiltersDescriptor Reverse(string name, Func<ReverseTokenFilterDescriptor, IReverseTokenFilter> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new ReverseTokenFilterDescriptor()));

		/// <summary>
		/// A filter that stems words using a Snowball-generated stemmer.
		/// </summary>
		public TokenFiltersDescriptor Snowball(string name, Func<SnowballTokenFilterDescriptor, ISnowballTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new SnowballTokenFilterDescriptor()));

		/// <summary>
		/// A filter that stems words (similar to snowball, but with more options).
		/// </summary>
		public TokenFiltersDescriptor Stemmer(string name, Func<StemmerTokenFilterDescriptor, IStemmerTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new StemmerTokenFilterDescriptor()));

		/// <inheritdoc cref="IPredicateTokenFilter" />
		public TokenFiltersDescriptor Predicate(string name, Func<PredicateTokenFilterDescriptor, IPredicateTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new PredicateTokenFilterDescriptor()));

		/// <inheritdoc cref="IConditionTokenFilter" />
		public TokenFiltersDescriptor Condition(string name, Func<ConditionTokenFilterDescriptor, IConditionTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new ConditionTokenFilterDescriptor()));

		/// <summary>
		/// Overrides stemming algorithms, by applying a custom mapping, then protecting these terms from being modified by
		/// stemmers. Must be placed
		/// before any stemming filters.
		/// </summary>
		public TokenFiltersDescriptor StemmerOverride(string name, Func<StemmerOverrideTokenFilterDescriptor, IStemmerOverrideTokenFilter> selector
		) =>
			Assign(name, selector?.Invoke(new StemmerOverrideTokenFilterDescriptor()));

		/// <summary>
		/// The trim token filter trims surrounding whitespaces around a token.
		/// </summary>
		public TokenFiltersDescriptor Trim(string name, Func<TrimTokenFilterDescriptor, ITrimTokenFilter> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new TrimTokenFilterDescriptor()));

		/// <summary>
		/// The truncate token filter can be used to truncate tokens into a specific length. This can come in handy with keyword
		/// (single token)
		/// <para> based mapped fields that are used for sorting in order to reduce memory usage.</para>
		/// </summary>
		public TokenFiltersDescriptor Truncate(string name, Func<TruncateTokenFilterDescriptor, ITruncateTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new TruncateTokenFilterDescriptor()));

		/// <summary>
		/// The unique token filter can be used to only index unique tokens during analysis. By default it is applied on all the
		/// token stream
		/// </summary>
		public TokenFiltersDescriptor Unique(string name, Func<UniqueTokenFilterDescriptor, IUniqueTokenFilter> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new UniqueTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type uppercase that normalizes token text to upper case.
		/// </summary>
		public TokenFiltersDescriptor Uppercase(string name, Func<UppercaseTokenFilterDescriptor, IUppercaseTokenFilter> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new UppercaseTokenFilterDescriptor()));

		/// <summary>
		/// A token filter of type fingerprint The fingerprint token filter that emits a single token which is useful
		/// for fingerprinting a body of text, and/or providing a token that can be clustered on.
		/// It does this by sorting the tokens, deduplicating and then concatenating them back into a single token.
		/// </summary>
		public TokenFiltersDescriptor Fingerprint(string name, Func<FingerprintTokenFilterDescriptor, IFingerprintTokenFilter> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new FingerprintTokenFilterDescriptor()));

		/// <summary>
		/// The kuromoji_stemmer token filter normalizes common katakana spelling variations ending in a
		/// long sound character by removing this character (U+30FC). Only full-width katakana characters are supported.
		/// Part of the `analysis-kuromoji` plugin:
		/// https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
		/// </summary>
		public TokenFiltersDescriptor KuromojiStemmer(string name,
			Func<KuromojiStemmerTokenFilterDescriptor, IKuromojiStemmerTokenFilter> selector = null
		) =>
			Assign(name, selector.InvokeOrDefault(new KuromojiStemmerTokenFilterDescriptor()));

		/// <summary>
		/// The kuromoji_readingform token filter replaces the token with its reading form in either katakana or romaji.
		/// Part of the `analysis-kuromoji` plugin:
		/// https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
		/// </summary>
		public TokenFiltersDescriptor KuromojiReadingForm(string name,
			Func<KuromojiReadingFormTokenFilterDescriptor, IKuromojiReadingFormTokenFilter> selector
		) =>
			Assign(name, selector.Invoke(new KuromojiReadingFormTokenFilterDescriptor()));

		/// <summary>
		/// The kuromoji_part_of_speech token filter removes tokens that match a set of part-of-speech tags.
		/// Part of the `analysis-kuromoji` plugin:
		/// https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
		/// </summary>
		public TokenFiltersDescriptor KuromojiPartOfSpeech(string name,
			Func<KuromojiPartOfSpeechTokenFilterDescriptor, IKuromojiPartOfSpeechTokenFilter> selector
		) =>
			Assign(name, selector.Invoke(new KuromojiPartOfSpeechTokenFilterDescriptor()));


		/// <summary>
		/// Collations are used for sorting documents in a language-specific word order. The icu_collation token filter is
		/// available to all indices and
		/// defaults to using the DUCET collation, which is a best-effort attempt at language-neutral sorting.
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public TokenFiltersDescriptor IcuCollation(string name, Func<IcuCollationTokenFilterDescriptor, IIcuCollationTokenFilter> selector) =>
			Assign(name, selector.Invoke(new IcuCollationTokenFilterDescriptor()));

		/// <summary>
		/// Case folding of Unicode characters based on UTR#30, like the ASCII-folding token filter on steroids.
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public TokenFiltersDescriptor IcuFolding(string name, Func<IcuFoldingTokenFilterDescriptor, IIcuFoldingTokenFilter> selector) =>
			Assign(name, selector.Invoke(new IcuFoldingTokenFilterDescriptor()));

		/// <summary>
		/// Normalizes as defined here: http://userguide.icu-project.org/transforms/normalization
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public TokenFiltersDescriptor IcuNormalization(string name, Func<IcuNormalizationTokenFilterDescriptor, IIcuNormalizationTokenFilter> selector
		) =>
			Assign(name, selector.Invoke(new IcuNormalizationTokenFilterDescriptor()));

		/// <summary>
		/// Transforms are used to process Unicode text in many different ways, such as case mapping,
		/// normalization, transliteration and bidirectional text handling.
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public TokenFiltersDescriptor IcuTransform(string name, Func<IcuTransformTokenFilterDescriptor, IIcuTransformTokenFilter> selector) =>
			Assign(name, selector.Invoke(new IcuTransformTokenFilterDescriptor()));

		/// <inheritdoc cref="INoriPartOfSpeechTokenFilter" />
		public TokenFiltersDescriptor NoriPartOfSpeech(string name, Func<NoriPartOfSpeechTokenFilterDescriptor, INoriPartOfSpeechTokenFilter> selector
		) =>
			Assign(name, selector.Invoke(new NoriPartOfSpeechTokenFilterDescriptor()));

		/// <summary>
		///  A token filter of type multiplexer will emit multiple tokens at the same position, each version of the token
		/// having been run through a different filter. Identical output tokens at the same position will be removed.
		/// </summary>
		public TokenFiltersDescriptor Multiplexer(string name, Func<MultiplexerTokenFilterDescriptor, IMultiplexerTokenFilter> selector) =>
			Assign(name, selector.Invoke(new MultiplexerTokenFilterDescriptor()));

		/// <summary> A token filter of type remove_duplicates that drops identical tokens at the same position. </summary>
		public TokenFiltersDescriptor RemoveDuplicates(string name,
			Func<RemoveDuplicatesTokenFilterDescriptor, IRemoveDuplicatesTokenFilter> selector = null
		) =>
			Assign(name, selector.InvokeOrDefault(new RemoveDuplicatesTokenFilterDescriptor()));
	}
}
