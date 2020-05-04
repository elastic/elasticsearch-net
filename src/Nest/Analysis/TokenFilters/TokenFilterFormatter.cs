// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	internal class TokenFilterFormatter : IJsonFormatter<ITokenFilter>
	{
		private static readonly byte[] TypeField = JsonWriter.GetEncodedPropertyNameWithoutQuotation("type");

		private static readonly AutomataDictionary TokenFilterTypes = new AutomataDictionary
		{
			{ "asciifolding", 0 },
			{ "common_grams", 1 },
			{ "delimited_payload", 2 },
			{ "delimited_payload_filter", 2 },
			{ "dictionary_decompounder", 3 },
			{ "edge_ngram", 4 },
			{ "elision", 5 },
			{ "hunspell", 6 },
			{ "hyphenation_decompounder", 7 },
			{ "keep_types", 8 },
			{ "keep", 9 },
			{ "keyword_marker", 10 },
			{ "kstem", 11 },
			{ "length", 12 },
			{ "limit", 13 },
			{ "lowercase", 14 },
			{ "ngram", 15 },
			{ "pattern_capture", 16 },
			{ "pattern_replace", 17 },
			{ "porter_stem", 18 },
			{ "phonetic", 19 },
			{ "reverse", 20 },
			{ "shingle", 21 },
			{ "snowball", 22 },
			{ "stemmer", 23 },
			{ "stemmer_override", 24 },
			{ "stop", 25 },
			{ "synonym", 26 },
			{ "synonym_graph", 27 },
			{ "trim", 28 },
			{ "truncate", 29 },
			{ "unique", 30 },
			{ "uppercase", 31 },
			{ "word_delimiter", 32 },
			{ "word_delimiter_graph", 33 },
			{ "fingerprint", 34 },
			{ "nori_part_of_speech", 35 },
			{ "kuromoji_readingform", 36 },
			{ "kuromoji_part_of_speech", 37 },
			{ "kuromoji_stemmer", 38 },
			{ "icu_collation", 39 },
			{ "icu_folding", 40 },
			{ "icu_normalizer", 41 },
			{ "icu_transform", 42 },
			{ "condition", 43 },
			{ "multiplexer", 44 },
			{ "predicate_token_filter", 45 }
		};

		public ITokenFilter Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);
			var count = 0;
			ArraySegment<byte> tokenFilterType = default;
			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyNameSegmentRaw();
				if (propertyName.EqualsBytes(TypeField))
				{
					tokenFilterType = segmentReader.ReadStringSegmentUnsafe();
					break;
				}

				segmentReader.ReadNextBlock();
			}

			if (tokenFilterType == default)
				return null;

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);
			if (TokenFilterTypes.TryGetValue(tokenFilterType, out var value))
			{
				switch (value)
				{
					case 0: return Deserialize<AsciiFoldingTokenFilter>(ref segmentReader, formatterResolver);
					case 1: return Deserialize<CommonGramsTokenFilter>(ref segmentReader, formatterResolver);
					case 2: return Deserialize<DelimitedPayloadTokenFilter>(ref segmentReader, formatterResolver);
					case 3: return Deserialize<DictionaryDecompounderTokenFilter>(ref segmentReader, formatterResolver);
					case 4: return Deserialize<EdgeNGramTokenFilter>(ref segmentReader, formatterResolver);
					case 5: return Deserialize<ElisionTokenFilter>(ref segmentReader, formatterResolver);
					case 6: return Deserialize<HunspellTokenFilter>(ref segmentReader, formatterResolver);
					case 7: return Deserialize<HyphenationDecompounderTokenFilter>(ref segmentReader, formatterResolver);
					case 8: return Deserialize<KeepTypesTokenFilter>(ref segmentReader, formatterResolver);
					case 9: return Deserialize<KeepWordsTokenFilter>(ref segmentReader, formatterResolver);
					case 10: return Deserialize<KeywordMarkerTokenFilter>(ref segmentReader, formatterResolver);
					case 11: return Deserialize<KStemTokenFilter>(ref segmentReader, formatterResolver);
					case 12: return Deserialize<LengthTokenFilter>(ref segmentReader, formatterResolver);
					case 13: return Deserialize<LimitTokenCountTokenFilter>(ref segmentReader, formatterResolver);
					case 14: return Deserialize<LowercaseTokenFilter>(ref segmentReader, formatterResolver);
					case 15: return Deserialize<NGramTokenFilter>(ref segmentReader, formatterResolver);
					case 16: return Deserialize<PatternCaptureTokenFilter>(ref segmentReader, formatterResolver);
					case 17: return Deserialize<PatternReplaceTokenFilter>(ref segmentReader, formatterResolver);
					case 18: return Deserialize<PorterStemTokenFilter>(ref segmentReader, formatterResolver);
					case 19: return Deserialize<PhoneticTokenFilter>(ref segmentReader, formatterResolver);
					case 20: return Deserialize<ReverseTokenFilter>(ref segmentReader, formatterResolver);
					case 21: return Deserialize<ShingleTokenFilter>(ref segmentReader, formatterResolver);
					case 22: return Deserialize<SnowballTokenFilter>(ref segmentReader, formatterResolver);
					case 23: return Deserialize<StemmerTokenFilter>(ref segmentReader, formatterResolver);
					case 24: return Deserialize<StemmerOverrideTokenFilter>(ref segmentReader, formatterResolver);
					case 25: return Deserialize<StopTokenFilter>(ref segmentReader, formatterResolver);
					case 26: return Deserialize<SynonymTokenFilter>(ref segmentReader, formatterResolver);
					case 27: return Deserialize<SynonymGraphTokenFilter>(ref segmentReader, formatterResolver);
					case 28: return Deserialize<TrimTokenFilter>(ref segmentReader, formatterResolver);
					case 29: return Deserialize<TruncateTokenFilter>(ref segmentReader, formatterResolver);
					case 30: return Deserialize<UniqueTokenFilter>(ref segmentReader, formatterResolver);
					case 31: return Deserialize<UppercaseTokenFilter>(ref segmentReader, formatterResolver);
					case 32: return Deserialize<WordDelimiterTokenFilter>(ref segmentReader, formatterResolver);
					case 33: return Deserialize<WordDelimiterGraphTokenFilter>(ref segmentReader, formatterResolver);
					case 34: return Deserialize<FingerprintTokenFilter>(ref segmentReader, formatterResolver);
					case 35: return Deserialize<NoriPartOfSpeechTokenFilter>(ref segmentReader, formatterResolver);
					case 36: return Deserialize<KuromojiReadingFormTokenFilter>(ref segmentReader, formatterResolver);
					case 37: return Deserialize<KuromojiPartOfSpeechTokenFilter>(ref segmentReader, formatterResolver);
					case 38: return Deserialize<KuromojiStemmerTokenFilter>(ref segmentReader, formatterResolver);
					case 39: return Deserialize<IcuCollationTokenFilter>(ref segmentReader, formatterResolver);
					case 40: return Deserialize<IcuFoldingTokenFilter>(ref segmentReader, formatterResolver);
					case 41: return Deserialize<IcuNormalizationTokenFilter>(ref segmentReader, formatterResolver);
					case 42: return Deserialize<IcuTransformTokenFilter>(ref segmentReader, formatterResolver);
					case 43: return Deserialize<ConditionTokenFilter>(ref segmentReader, formatterResolver);
					case 44: return Deserialize<MultiplexerTokenFilter>(ref segmentReader, formatterResolver);
					case 45: return Deserialize<PredicateTokenFilter>(ref segmentReader, formatterResolver);
					default: return null;
				}
			}

			return null;
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
				case "condition":
					Serialize<IConditionTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "multiplexer":
					Serialize<IMultiplexerTokenFilter>(ref writer, value, formatterResolver);
					break;
				case "predicate_token_filter":
					Serialize<IPredicateTokenFilter>(ref writer, value, formatterResolver);
					break;
				default:
					// serialize user defined token filter
					var formatter = formatterResolver.GetFormatter<object>();
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
