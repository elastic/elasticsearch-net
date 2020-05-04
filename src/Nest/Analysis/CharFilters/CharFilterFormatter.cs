// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;


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
			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyName();
				if (propertyName == "type")
				{
					charFilterType = segmentReader.ReadString();
					break;
				}

				// skip value
				segmentReader.ReadNextBlock();
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

		public void Serialize(ref JsonWriter writer, ICharFilter value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Type)
			{
				case "html_strip":
					Serialize<IHtmlStripCharFilter>(ref writer, value, formatterResolver);
					break;
				case "mapping":
					Serialize<IMappingCharFilter>(ref writer, value, formatterResolver);
					break;
				case "pattern_replace":
					Serialize<IPatternReplaceCharFilter>(ref writer, value, formatterResolver);
					break;
				case "kuromoji_iteration_mark":
					Serialize<IKuromojiIterationMarkCharFilter>(ref writer, value, formatterResolver);
					break;
				case "icu_normalizer":
					Serialize<IIcuNormalizationCharFilter>(ref writer, value, formatterResolver);
					break;
				default:
					var formatter = formatterResolver.GetFormatter<object>();
					formatter.Serialize(ref writer, value, formatterResolver);
					break;
			}
		}

		private static void Serialize<TCharFilter>(ref JsonWriter writer, ICharFilter value, IJsonFormatterResolver formatterResolver)
			where TCharFilter : class, ICharFilter
		{
			var formatter = formatterResolver.GetFormatter<TCharFilter>();
			formatter.Serialize(ref writer, value as TCharFilter, formatterResolver);
		}

		private static TCharFilter Deserialize<TCharFilter>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TCharFilter : ICharFilter
		{
			var formatter = formatterResolver.GetFormatter<TCharFilter>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
