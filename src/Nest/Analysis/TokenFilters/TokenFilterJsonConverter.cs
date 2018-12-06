using Utf8Json;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class TokenFilterFormatter : IJsonFormatter<ITokenFilter>
	{
		public ITokenFilter Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);
			var count = 0;
			string tokenFilterType = null;
			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				switch (propertyName)
				{
					case "type":
						tokenFilterType = reader.ReadString();
						break;
				}
			}

			if (tokenFilterType == null)
				return null;

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			switch (tokenFilterType)
			{
				case "asciifolding": return Deserialize<AsciiFoldingTokenFilter>(ref segmentReader, formatterResolver);
				case "common_grams": return Deserialize<CommonGramsTokenFilter>(ref segmentReader, formatterResolver);
				case "delimited_payload": return Deserialize<DelimitedPayloadTokenFilter>(ref segmentReader, formatterResolver);
				case "dictionary_decompounder": return Deserialize<DictionaryDecompounderTokenFilter>(ref segmentReader, formatterResolver);
				case "edge_ngram": return Deserialize<EdgeNGramTokenFilter>(ref segmentReader, formatterResolver);
				case "elision": return Deserialize<ElisionTokenFilter>(ref segmentReader, formatterResolver);
				case "hunspell": return Deserialize<HunspellTokenFilter>(ref segmentReader, formatterResolver);
				case "hyphenation_decompounder": return Deserialize<HyphenationDecompounderTokenFilter>(ref segmentReader, formatterResolver);
				case "keep_types": return Deserialize<KeepTypesTokenFilter>(ref segmentReader, formatterResolver);
				case "keep": return Deserialize<KeepWordsTokenFilter>(ref segmentReader, formatterResolver);
				case "keyword_marker": return Deserialize<KeywordMarkerTokenFilter>(ref segmentReader, formatterResolver);
				case "kstem": return Deserialize<KStemTokenFilter>(ref segmentReader, formatterResolver);
				case "length": return Deserialize<LengthTokenFilter>(ref segmentReader, formatterResolver);
				case "limit": return Deserialize<LimitTokenCountTokenFilter>(ref segmentReader, formatterResolver);
				case "lowercase": return Deserialize<LowercaseTokenFilter>(ref segmentReader, formatterResolver);
				case "ngram": return Deserialize<NGramTokenFilter>(ref segmentReader, formatterResolver);
				case "pattern_capture": return Deserialize<PatternCaptureTokenFilter>(ref segmentReader, formatterResolver);
				case "pattern_replace": return Deserialize<PatternReplaceTokenFilter>(ref segmentReader, formatterResolver);
				case "porter_stem": return Deserialize<PorterStemTokenFilter>(ref segmentReader, formatterResolver);
				case "phonetic": return Deserialize<PhoneticTokenFilter>(ref segmentReader, formatterResolver);
				case "reverse": return Deserialize<ReverseTokenFilter>(ref segmentReader, formatterResolver);
				case "shingle": return Deserialize<ShingleTokenFilter>(ref segmentReader, formatterResolver);
				case "snowball": return Deserialize<SnowballTokenFilter>(ref segmentReader, formatterResolver);
				case "stemmer": return Deserialize<StemmerTokenFilter>(ref segmentReader, formatterResolver);
				case "stemmer_override": return Deserialize<StemmerOverrideTokenFilter>(ref segmentReader, formatterResolver);
				case "stop": return Deserialize<StopTokenFilter>(ref segmentReader, formatterResolver);
				case "standard": return Deserialize<StandardTokenFilter>(ref segmentReader, formatterResolver);
				case "synonym": return Deserialize<SynonymTokenFilter>(ref segmentReader, formatterResolver);
				case "synonym_graph": return Deserialize<SynonymGraphTokenFilter>(ref segmentReader, formatterResolver);
				case "trim": return Deserialize<TrimTokenFilter>(ref segmentReader, formatterResolver);
				case "truncate": return Deserialize<TruncateTokenFilter>(ref segmentReader, formatterResolver);
				case "unique": return Deserialize<UniqueTokenFilter>(ref segmentReader, formatterResolver);
				case "uppercase": return Deserialize<UppercaseTokenFilter>(ref segmentReader, formatterResolver);
				case "word_delimiter": return Deserialize<WordDelimiterTokenFilter>(ref segmentReader, formatterResolver);
				case "word_delimiter_graph": return Deserialize<WordDelimiterGraphTokenFilter>(ref segmentReader, formatterResolver);
				case "fingerprint": return Deserialize<FingerprintTokenFilter>(ref segmentReader, formatterResolver);
				case "kuromoji_readingform": return Deserialize<KuromojiReadingFormTokenFilter>(ref segmentReader, formatterResolver);
				case "kuromoji_part_of_speech": return Deserialize<KuromojiPartOfSpeechTokenFilter>(ref segmentReader, formatterResolver);
				case "kuromoji_stemmer": return Deserialize<KuromojiStemmerTokenFilter>(ref segmentReader, formatterResolver);
				case "icu_collation": return Deserialize<IcuCollationTokenFilter>(ref segmentReader, formatterResolver);
				case "icu_folding": return Deserialize<IcuFoldingTokenFilter>(ref segmentReader, formatterResolver);
				case "icu_normalizer": return Deserialize<IcuNormalizationTokenFilter>(ref segmentReader, formatterResolver);
				case "icu_transform": return Deserialize<IcuTransformTokenFilter>(ref segmentReader, formatterResolver);
				default: return null;
			}
		}

		public void Serialize(ref JsonWriter writer, ITokenFilter value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ITokenFilter>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		private static TTokenFilter Deserialize<TTokenFilter>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TTokenFilter : ITokenFilter
		{
			var formatter = formatterResolver.GetFormatter<TTokenFilter>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
