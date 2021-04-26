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

using System;
using Nest.Utf8Json;
namespace Nest
{
	internal class SuggestContextFormatter : IJsonFormatter<ISuggestContext>
	{
		private static readonly AutomataDictionary ContextTypes = new AutomataDictionary
		{
			{ "geo", 0 },
			{ "category", 1 }
		};

		public ISuggestContext Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			var segment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(segment.Array, segment.Offset);

			var count = 0;
			ArraySegment<byte> contextType = default;
			while (segmentReader.ReadIsInObject(ref count))
			{
				if (segmentReader.ReadPropertyName() == "type")
				{
					contextType = segmentReader.ReadStringSegmentRaw();
					break;
				}

				segmentReader.ReadNextBlock();
			}

			segmentReader = new JsonReader(segment.Array, segment.Offset);

			if (ContextTypes.TryGetValue(contextType, out var value))
			{
				switch (value)
				{
					case 0:
						return Deserialize<GeoSuggestContext>(ref segmentReader, formatterResolver);
					case 1:
						return Deserialize<CategorySuggestContext>(ref segmentReader, formatterResolver);
				}
			}

			return Deserialize<CategorySuggestContext>(ref segmentReader, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, ISuggestContext value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			formatterResolver.GetFormatter<object>().Serialize(ref writer, value, formatterResolver);
		}

		private static TContext Deserialize<TContext>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TContext : ISuggestContext
		{
			var formatter = formatterResolver.GetFormatter<TContext>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
