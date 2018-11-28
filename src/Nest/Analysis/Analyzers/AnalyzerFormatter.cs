using Utf8Json;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class AnalyzerFormatter : IJsonFormatter<IAnalyzer>
	{
		public IAnalyzer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);
			var count = 0;
			string analyzerType = null;
			var tokenizerPresent = false;
			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				switch (propertyName)
				{
					case "type":
						analyzerType = reader.ReadString();
						break;
					case "tokenizer":
						tokenizerPresent = true;
						break;
				}
			}

			if (analyzerType == null)
				return null;

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			switch (analyzerType)
			{
				case "stop":
					return Deserialize<StopAnalyzer>(ref segmentReader, formatterResolver);
				case "standard":
					return Deserialize<StandardAnalyzer>(ref segmentReader, formatterResolver);
				case "snowball":
					return Deserialize<SnowballAnalyzer>(ref segmentReader, formatterResolver);
				case "pattern":
					return Deserialize<PatternAnalyzer>(ref segmentReader, formatterResolver);
				case "keyword":
					return Deserialize<KeywordAnalyzer>(ref segmentReader, formatterResolver);
				case "whitespace":
					return Deserialize<WhitespaceAnalyzer>(ref segmentReader, formatterResolver);
				case "simple":
					return Deserialize<SimpleAnalyzer>(ref segmentReader, formatterResolver);
				case "fingerprint":
					return Deserialize<FingerprintAnalyzer>(ref segmentReader, formatterResolver);
				case "kuromoji":
					return Deserialize<KuromojiAnalyzer>(ref segmentReader, formatterResolver);
				case "nori":
					return Deserialize<NoriAnalyzer>(ref segmentReader, formatterResolver);
				default:
					if (tokenizerPresent)
						return Deserialize<CustomAnalyzer>(ref segmentReader, formatterResolver);

					return Deserialize<LanguageAnalyzer>(ref segmentReader, formatterResolver);
			}
		}

		public void Serialize(ref JsonWriter writer, IAnalyzer value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IAnalyzer>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		private static TAnalyzer Deserialize<TAnalyzer>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TAnalyzer : IAnalyzer
		{
			var formatter = formatterResolver.GetFormatter<TAnalyzer>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
