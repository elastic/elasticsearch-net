using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class TokenFilterJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);

			var typeProperty = o.Property("type");
			if (typeProperty == null) return null;

			var typePropertyValue = typeProperty.Value.ToString();
			switch(typePropertyValue.ToLowerInvariant())
			{
				case "asciifolding": return o.ToObject<AsciiFoldingTokenFilter>(ElasticContractResolver.Empty);
				case "common_grams": return o.ToObject<CommonGramsTokenFilter>(ElasticContractResolver.Empty);
				case "delimited_payload": return o.ToObject<DelimitedPayloadTokenFilter>(ElasticContractResolver.Empty);
				case "dictionary_decompounder": return o.ToObject<DictionaryDecompounderTokenFilter>(ElasticContractResolver.Empty);
				case "edge_ngram": return o.ToObject<EdgeNGramTokenFilter>(ElasticContractResolver.Empty);
				case "elision": return o.ToObject<ElisionTokenFilter>(ElasticContractResolver.Empty);
				case "hunspell": return o.ToObject<HunspellTokenFilter>(ElasticContractResolver.Empty);
				case "hyphenation_decompounder": return o.ToObject<HyphenationDecompounderTokenFilter>(ElasticContractResolver.Empty);
				case "keep_types": return o.ToObject<KeepTypesTokenFilter>(ElasticContractResolver.Empty);
				case "keep": return o.ToObject<KeepWordsTokenFilter>(ElasticContractResolver.Empty);
				case "keyword_marker": return o.ToObject<KeywordMarkerTokenFilter>(ElasticContractResolver.Empty);
				case "kstem": return o.ToObject<KStemTokenFilter>(ElasticContractResolver.Empty);
				case "length": return o.ToObject<LengthTokenFilter>(ElasticContractResolver.Empty);
				case "limit": return o.ToObject<LimitTokenCountTokenFilter>(ElasticContractResolver.Empty);
				case "lowercase": return o.ToObject<LowercaseTokenFilter>(ElasticContractResolver.Empty);
				case "ngram": return o.ToObject<NGramTokenFilter>(ElasticContractResolver.Empty);
				case "pattern_capture": return o.ToObject<PatternCaptureTokenFilter>(ElasticContractResolver.Empty);
				case "pattern_replace": return o.ToObject<PatternReplaceTokenFilter>(ElasticContractResolver.Empty);
				case "porter_stem": return o.ToObject<PorterStemTokenFilter>(ElasticContractResolver.Empty);
				case "phonetic": return o.ToObject<PhoneticTokenFilter>(ElasticContractResolver.Empty);
				case "reverse": return o.ToObject<ReverseTokenFilter>(ElasticContractResolver.Empty);
				case "shingle": return o.ToObject<ShingleTokenFilter>(ElasticContractResolver.Empty);
				case "snowball": return o.ToObject<SnowballTokenFilter>(ElasticContractResolver.Empty);
				case "stemmer": return o.ToObject<StemmerTokenFilter>(ElasticContractResolver.Empty);
				case "stemmer_override": return o.ToObject<StemmerOverrideTokenFilter>(ElasticContractResolver.Empty);
				case "stop": return o.ToObject<StopTokenFilter>(ElasticContractResolver.Empty);
				case "standard": return o.ToObject<StandardTokenFilter>(ElasticContractResolver.Empty);
				case "synonym": return o.ToObject<SynonymTokenFilter>(ElasticContractResolver.Empty);
				case "synonym_graph": return o.ToObject<SynonymGraphTokenFilter>(ElasticContractResolver.Empty);
				case "trim": return o.ToObject<TrimTokenFilter>(ElasticContractResolver.Empty);
				case "truncate": return o.ToObject<TruncateTokenFilter>(ElasticContractResolver.Empty);
				case "unique": return o.ToObject<UniqueTokenFilter>(ElasticContractResolver.Empty);
				case "uppercase": return o.ToObject<UppercaseTokenFilter>(ElasticContractResolver.Empty);
				case "word_delimiter": return o.ToObject<WordDelimiterTokenFilter>(ElasticContractResolver.Empty);
				case "word_delimiter_graph": return o.ToObject<WordDelimiterGraphTokenFilter>(ElasticContractResolver.Empty);
				case "fingerprint": return o.ToObject<FingerprintTokenFilter>(ElasticContractResolver.Empty);
				case "kuromoji_readingform": return o.ToObject<KuromojiReadingFormTokenFilter>(ElasticContractResolver.Empty);
				case "kuromoji_part_of_speech": return o.ToObject<KuromojiPartOfSpeechTokenFilter>(ElasticContractResolver.Empty);
				case "kuromoji_stemmer": return o.ToObject<KuromojiStemmerTokenFilter>(ElasticContractResolver.Empty);
				case "icu_collation": return o.ToObject<IcuCollationTokenFilter>(ElasticContractResolver.Empty);
				case "icu_folding": return o.ToObject<IcuFoldingTokenFilter>(ElasticContractResolver.Empty);
				case "icu_normalizer": return o.ToObject<IcuNormalizationTokenFilter>(ElasticContractResolver.Empty);
				case "icu_transform": return o.ToObject<IcuTransformTokenFilter>(ElasticContractResolver.Empty);
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
