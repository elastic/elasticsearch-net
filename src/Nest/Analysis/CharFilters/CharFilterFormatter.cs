using System;
using Utf8Json;

namespace Nest
{
	internal class CharFilterFormatter : IJsonFormatter<ICharFilter>
	{
		public ICharFilter Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);
			var count = 0;
			string charFilterType = null;
			var tokenizerPresent = false;
			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				if (propertyName == "type")
				{
					charFilterType = reader.ReadString();
					break;
				}
			}

			if (charFilterType == null)
				return null;

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			switch (charFilterType)
			{
				case "html_strip":
					return Deserialize<HtmlStripCharFilter>(ref segmentReader, formatterResolver);
				case "mapping":
					return Deserialize<MappingCharFilter>(ref segmentReader, formatterResolver);
				case "pattern_replace":
					return Deserialize<PatternReplaceCharFilter>(ref segmentReader, formatterResolver);
				case "kuromoji_iteration_mark":
					return Deserialize<KuromojiIterationMarkCharFilter>(ref segmentReader, formatterResolver);
				case "icu_normalizer":
					return Deserialize<IcuNormalizationCharFilter>(ref segmentReader, formatterResolver);
				default:
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, ICharFilter value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		private static TCharFilter Deserialize<TCharFilter>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TCharFilter : ICharFilter
		{
			var formatter = formatterResolver.GetFormatter<TCharFilter>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
