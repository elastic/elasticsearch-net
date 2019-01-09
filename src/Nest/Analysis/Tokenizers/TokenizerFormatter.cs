using Utf8Json;

namespace Nest
{
	internal class TokenizerFormatter : IJsonFormatter<ITokenizer>
	{
		public ITokenizer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);
			var count = 0;
			string tokenizerType = null;
			while (segmentReader.ReadIsInObject(ref count))
			{
				if (segmentReader.ReadPropertyName() == "type")
				{
					tokenizerType = segmentReader.ReadString();
					break;
				}
			}

			if (tokenizerType == null)
				return null;

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			switch (tokenizerType)
			{
				case "edgengram":
				case "edge_ngram":
					return Deserialize<EdgeNGramTokenizer>(ref segmentReader, formatterResolver);
				case "ngram":
					return Deserialize<NGramTokenizer>(ref segmentReader, formatterResolver);
				case "path_hierarchy":
					return Deserialize<PathHierarchyTokenizer>(ref segmentReader, formatterResolver);
				case "pattern":
					return Deserialize<PatternTokenizer>(ref segmentReader, formatterResolver);
				case "standard":
					return Deserialize<StandardTokenizer>(ref segmentReader, formatterResolver);
				case "uax_url_email":
					return Deserialize<UaxEmailUrlTokenizer>(ref segmentReader, formatterResolver);
				case "whitespace":
					return Deserialize<WhitespaceTokenizer>(ref segmentReader, formatterResolver);
				case "kuromoji_tokenizer":
					return Deserialize<KuromojiTokenizer>(ref segmentReader, formatterResolver);
				case "icu_tokenizer":
					return Deserialize<IcuTokenizer>(ref segmentReader, formatterResolver);
				case "nori_tokenizer":
					return Deserialize<NoriTokenizer>(ref segmentReader, formatterResolver);
				default:
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, ITokenizer value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Type)
			{
				case "edge_ngram":
					Serialize<IEdgeNGramTokenizer>(ref writer, value, formatterResolver);
					break;
				case "ngram":
					Serialize<INGramTokenizer>(ref writer, value, formatterResolver);
					break;
				case "path_hierarchy":
					Serialize<IPathHierarchyTokenizer>(ref writer, value, formatterResolver);
					break;
				case "pattern":
					Serialize<IPatternTokenizer>(ref writer, value, formatterResolver);
					break;
				case "standard":
					Serialize<IStandardTokenizer>(ref writer, value, formatterResolver);
					break;
				case "uax_url_email":
					Serialize<IUaxEmailUrlTokenizer>(ref writer, value, formatterResolver);
					break;
				case "whitespace":
					Serialize<IWhitespaceTokenizer>(ref writer, value, formatterResolver);
					break;
				case "kuromoji_tokenizer":
					Serialize<IKuromojiTokenizer>(ref writer, value, formatterResolver);
					break;
				case "icu_tokenizer":
					Serialize<IIcuTokenizer>(ref writer, value, formatterResolver);
					break;
				case "nori_tokenizer":
					Serialize<INoriTokenizer>(ref writer, value, formatterResolver);
					break;
				default:
					Serialize<ITokenizer>(ref writer, value, formatterResolver);
					break;
			}
		}

		private static TTokenizer Deserialize<TTokenizer>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TTokenizer : ITokenizer
		{
			var formatter = formatterResolver.GetFormatter<TTokenizer>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		private static void Serialize<TTokenizer>(ref JsonWriter writer, ITokenizer value, IJsonFormatterResolver formatterResolver)
			where TTokenizer : class, ITokenizer
		{
			var formatter = formatterResolver.GetFormatter<TTokenizer>();
			formatter.Serialize(ref writer, value as TTokenizer, formatterResolver);
		}
	}
}
