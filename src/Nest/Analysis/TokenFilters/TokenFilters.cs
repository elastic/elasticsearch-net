using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITokenFilters : IWrapDictionary { }

	public class TokenFilters : WrapDictionary<string, ITokenFilter>, ITokenFilters
	{
		public TokenFilters() : base() { }
		public TokenFilters(IDictionary<string, ITokenFilter> container) : base(container) { }
		public TokenFilters(Dictionary<string, ITokenFilter> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, ITokenFilter analyzer) => _backingDictionary.Add(name, analyzer);
	}

	public class TokenFiltersDescriptor : WrapDictionary<string, ITokenFilter>, ITokenFilters
	{
		protected TokenFiltersDescriptor Assign(string name, ITokenFilter analyzer) =>
			Fluent.Assign<TokenFiltersDescriptor, TokenFiltersDescriptor>(this, (a) => _backingDictionary.Add(name, analyzer));

		public TokenFiltersDescriptor Add(string name, ITokenFilter analyzer) => Assign(name, analyzer);

		public TokenFiltersDescriptor DictionaryDecompounder(string name, Func<DictionaryDecompounderTokenFilterDescriptor, IDictionaryDecompounderTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new DictionaryDecompounderTokenFilterDescriptor()));

		public TokenFiltersDescriptor HyphenationDecompounder(string name, Func<HyphenationDecompounderTokenFilterDescriptor, IHyphenationDecompounderTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new HyphenationDecompounderTokenFilterDescriptor()));

		public TokenFiltersDescriptor EdgeNGram(string name, Func<EdgeNGramTokenFilterDescriptor, IEdgeNGramTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new EdgeNGramTokenFilterDescriptor()));

		public TokenFiltersDescriptor Phonetic(string name, Func<PhoneticTokenFilterDescriptor, IPhoneticTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new PhoneticTokenFilterDescriptor()));

		public TokenFiltersDescriptor Shingle(string name, Func<ShingleTokenFilterDescriptor, IShingleTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new ShingleTokenFilterDescriptor()));

		public TokenFiltersDescriptor Stop(string name, Func<StopTokenFilterDescriptor, IStopTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new StopTokenFilterDescriptor()));

		public TokenFiltersDescriptor Synonym(string name, Func<SynonymTokenFilterDescriptor, ISynonymTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new SynonymTokenFilterDescriptor()));

		public TokenFiltersDescriptor WordDelimiter(string name, Func<WordDelimiterTokenFilterDescriptor, IWordDelimiterTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new WordDelimiterTokenFilterDescriptor()));

		public TokenFiltersDescriptor AsciiFolding(string name, Func<AsciiFoldingTokenFilterDescriptor, IAsciiFoldingTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new AsciiFoldingTokenFilterDescriptor()));

		public TokenFiltersDescriptor CommonGrams(string name, Func<CommonGramsTokenFilterDescriptor, ICommonGramsTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new CommonGramsTokenFilterDescriptor()));

		public TokenFiltersDescriptor DelimitedPayload(string name, Func<DelimitedPayloadTokenFilterDescriptor, IDelimitedPayloadTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new DelimitedPayloadTokenFilterDescriptor()));

		public TokenFiltersDescriptor Elision(string name, Func<ElisionTokenFilterDescriptor, IElisionTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new ElisionTokenFilterDescriptor()));

		public TokenFiltersDescriptor Hunspell(string name, Func<HunspellTokenFilterDescriptor, IHunspellTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new HunspellTokenFilterDescriptor()));

		public TokenFiltersDescriptor KeepTypes(string name, Func<KeepTypesTokenFilterDescriptor, IKeepTypesTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new KeepTypesTokenFilterDescriptor()));

		public TokenFiltersDescriptor KeepWords(string name, Func<KeepWordsTokenFilterDescriptor, IKeepWordsTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new KeepWordsTokenFilterDescriptor()));

		public TokenFiltersDescriptor KeywordMarker(string name, Func<KeywordMarkerTokenFilterDescriptor, IKeywordMarkerTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new KeywordMarkerTokenFilterDescriptor()));

		public TokenFiltersDescriptor KeywordRepeat(string name, Func<KeywordRepeatTokenFilterDescriptor, IKeywordRepeatTokenFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new KeywordRepeatTokenFilterDescriptor()));

		public TokenFiltersDescriptor KStem(string name, Func<KStemTokenFilterDescriptor, IKStemTokenFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new KStemTokenFilterDescriptor()));

		public TokenFiltersDescriptor Length(string name, Func<LengthTokenFilterDescriptor, ILengthTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new LengthTokenFilterDescriptor()));

		public TokenFiltersDescriptor LimitTokenCount(string name, Func<LimitTokenCountTokenFilterDescriptor, ILimitTokenCountTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new LimitTokenCountTokenFilterDescriptor()));

		public TokenFiltersDescriptor Lowercase(string name, Func<LowercaseTokenFilterDescriptor, ILowercaseTokenFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new LowercaseTokenFilterDescriptor()));

		public TokenFiltersDescriptor NGram(string name, Func<NGramTokenFilterDescriptor, INGramTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new NGramTokenFilterDescriptor()));

		public TokenFiltersDescriptor PatternCapture(string name, Func<PatternCaptureTokenFilterDescriptor, IPatternCaptureTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new PatternCaptureTokenFilterDescriptor()));

		public TokenFiltersDescriptor PatternReplace(string name, Func<PatternReplaceTokenFilterDescriptor, IPatternReplaceTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new PatternReplaceTokenFilterDescriptor()));

		public TokenFiltersDescriptor PorterStem(string name, Func<PorterStemTokenFilterDescriptor, IPorterStemTokenFilter> selector=null) =>
			Assign(name, selector?.InvokeOrDefault(new PorterStemTokenFilterDescriptor()));

		public TokenFiltersDescriptor Reverse(string name, Func<ReverseTokenFilterDescriptor, IReverseTokenFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new ReverseTokenFilterDescriptor()));

		public TokenFiltersDescriptor Snowball(string name, Func<SnowballTokenFilterDescriptor, ISnowballTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new SnowballTokenFilterDescriptor()));

		public TokenFiltersDescriptor Standard(string name, Func<StandardTokenFilterDescriptor, IStandardTokenFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new StandardTokenFilterDescriptor()));

		public TokenFiltersDescriptor Stemmer(string name, Func<StemmerTokenFilterDescriptor, IStemmerTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new StemmerTokenFilterDescriptor()));

		public TokenFiltersDescriptor StemmerOverride(string name, Func<StemmerOverrideTokenFilterDescriptor, IStemmerOverrideTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new StemmerOverrideTokenFilterDescriptor()));

		public TokenFiltersDescriptor Trim(string name, Func<TrimTokenFilterDescriptor, ITrimTokenFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new TrimTokenFilterDescriptor()));

		public TokenFiltersDescriptor Truncate(string name, Func<TruncateTokenFilterDescriptor, ITruncateTokenFilter> selector) =>
			Assign(name, selector?.Invoke(new TruncateTokenFilterDescriptor()));

		public TokenFiltersDescriptor Unique(string name, Func<UniqueTokenFilterDescriptor, IUniqueTokenFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new UniqueTokenFilterDescriptor()));

		public TokenFiltersDescriptor Uppercase(string name, Func<UppercaseTokenFilterDescriptor, IUppercaseTokenFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new UppercaseTokenFilterDescriptor()));

	}
}
