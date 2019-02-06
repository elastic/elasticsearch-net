using Utf8Json;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class TokenFilterFormatter : IJsonFormatter<ITokenFilter>
	{
		private static byte[] TypeField = JsonWriter.GetEncodedPropertyNameWithoutQuotation("type");

		public ITokenFilter Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);
			var count = 0;
			string tokenFilterType = null;
			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyNameSegmentRaw();
				if (propertyName.EqualsBytes(TypeField))
				{
					tokenFilterType = segmentReader.ReadString();
					break;
				}

				segmentReader.ReadNextBlock();
			}

			if (tokenFilterType == null)
				return null;

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			// TODO: Move to AutomataDictionary
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
				case "nori_part_of_speech": return Deserialize<NoriPartOfSpeechTokenFilter>(ref segmentReader, formatterResolver);
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
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Type)
			{
				case "asciifolding":
					Serialize<IAsciiFoldingTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "common_grams":
					Serialize<ICommonGramsTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "delimited_payload":
					Serialize<IDelimitedPayloadTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "dictionary_decompounder":
					Serialize<IDictionaryDecompounderTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "edge_ngram":
					Serialize<IEdgeNGramTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "elision":
					Serialize<IElisionTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "hunspell":
					Serialize<IHunspellTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "hyphenation_decompounder":
					Serialize<IHyphenationDecompounderTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "keep_types":
					Serialize<IKeepTypesTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "keep":
					Serialize<IKeepWordsTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "keyword_marker":
					Serialize<IKeywordMarkerTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "kstem":
					Serialize<IKStemTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "length":
					Serialize<ILengthTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "limit":
					Serialize<ILimitTokenCountTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "lowercase":
					Serialize<ILowercaseTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "ngram":
					Serialize<INGramTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "pattern_capture":
					Serialize<IPatternCaptureTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "pattern_replace":
					Serialize<IPatternReplaceTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "porter_stem":
					Serialize<IPorterStemTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "phonetic":
					Serialize<IPhoneticTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "reverse":
					Serialize<IReverseTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "shingle":
					Serialize<IShingleTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "snowball":
					Serialize<ISnowballTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "stemmer":
					Serialize<IStemmerTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "stemmer_override":
					Serialize<IStemmerOverrideTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "stop":
					Serialize<IStopTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "standard":
					Serialize<IStandardTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "synonym":
					Serialize<ISynonymTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "synonym_graph":
					Serialize<ISynonymGraphTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "trim":
					Serialize<ITrimTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "truncate":
					Serialize<ITruncateTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "unique":
					Serialize<IUniqueTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "uppercase":
					Serialize<IUppercaseTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "word_delimiter":
					Serialize<IWordDelimiterTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "word_delimiter_graph":
					Serialize<IWordDelimiterGraphTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "fingerprint":
					Serialize<IFingerprintTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "nori_part_of_speech":
					Serialize<INoriPartOfSpeechTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "kuromoji_readingform":
					Serialize<IKuromojiReadingFormTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "kuromoji_part_of_speech":
					Serialize<IKuromojiPartOfSpeechTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "kuromoji_stemmer":
					Serialize<IKuromojiStemmerTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "icu_collation":
					Serialize<IIcuCollationTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "icu_folding":
					Serialize<IIcuFoldingTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "icu_normalizer":
					Serialize<IIcuNormalizationTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "icu_transform":
					Serialize<IIcuTransformTokenFilter>(ref writer, value, formatterResolver);
					break;
				default:
					var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ITokenFilter>();
					formatter.Serialize(ref writer, value, formatterResolver);
					break;
			}
		}

		private static void Serialize<TTokenFilter>(ref JsonWriter writer, ITokenFilter value, IJsonFormatterResolver formatterResolver)
			where TTokenFilter : class, ITokenFilter
		{
			var formatter = formatterResolver.GetFormatter<TTokenFilter>();
			formatter.Serialize(ref writer, value as TTokenFilter, formatterResolver);
		}

		private static TTokenFilter Deserialize<TTokenFilter>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TTokenFilter : ITokenFilter
		{
			var formatter = formatterResolver.GetFormatter<TTokenFilter>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
