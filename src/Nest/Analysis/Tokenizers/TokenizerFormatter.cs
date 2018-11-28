using System;
using Utf8Json;
using Utf8Json.Resolvers;

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
				if (reader.ReadPropertyName() == "type")
				{
					tokenizerType = reader.ReadString();
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

		private static TTokenizer Deserialize<TTokenizer>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TTokenizer : ITokenizer
		{
			var formatter = formatterResolver.GetFormatter<TTokenizer>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, ITokenizer value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ITokenizer>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
