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
				if (reader.ReadPropertyName() == "type") tokenizerType = reader.ReadString();
			}

			if (tokenizerType == null)
				return null;

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			switch (tokenizerType)
			{
				case "edgengram":
				case "edge_ngram":
				{
					var formatter = formatterResolver.GetFormatter<EdgeNGramTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "ngram":
				{
					var formatter = formatterResolver.GetFormatter<NGramTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "path_hierarchy":
				{
					var formatter = formatterResolver.GetFormatter<PathHierarchyTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "pattern":
				{
					var formatter = formatterResolver.GetFormatter<PatternTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "standard":
				{
					var formatter = formatterResolver.GetFormatter<StandardTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "uax_url_email":
				{
					var formatter = formatterResolver.GetFormatter<UaxEmailUrlTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "whitespace":
				{
					var formatter = formatterResolver.GetFormatter<WhitespaceTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "kuromoji_tokenizer":
				{
					var formatter = formatterResolver.GetFormatter<KuromojiTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "icu_tokenizer":
				{
					var formatter = formatterResolver.GetFormatter<IcuTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				case "nori_tokenizer":
				{
					var formatter = formatterResolver.GetFormatter<NoriTokenizer>();
					return formatter.Deserialize(ref segmentReader, formatterResolver);
				}
				default:
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, ITokenizer value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ITokenizer>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
