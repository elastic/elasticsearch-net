/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Nest.Utf8Json;


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
