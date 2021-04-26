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

using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;

namespace Nest
{
	internal class RangeQueryFormatter : IJsonFormatter<IRangeQuery>
	{
		private static readonly AutomataDictionary RangeFields = new AutomataDictionary
		{
			{ "format", 0 },
			{ "time_zone", 1 },
			{ "gt", 2 },
			{ "gte", 3 },
			{ "lte", 4 },
			{ "lt", 5 }
		};

		public IRangeQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var segment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(segment.Array, segment.Offset);
			var isLong = false;
			var isDate = false;
			var isDouble = false;
			var count = 0;

			while (segmentReader.ReadIsInObject(ref count))
			{
				segmentReader.ReadPropertyNameSegmentRaw();

				var innerCount = 0;
				while (segmentReader.ReadIsInObject(ref innerCount))
				{
					var innerPropertyName = segmentReader.ReadPropertyNameSegmentRaw();
					if (RangeFields.TryGetValue(innerPropertyName, out var innerValue))
					{
						switch (innerValue)
						{
							case 0:
							case 1:
								isDate = true;
								break;
							case 2:
							case 3:
							case 4:
							case 5:
								var token = segmentReader.GetCurrentJsonToken();
								switch (token)
								{
									case JsonToken.String:
									case JsonToken.Null:
										if (!isDate)
										{
											var valueSegment = segmentReader.ReadStringSegmentUnsafe();
											isDate = valueSegment.IsDateTime(formatterResolver, out _) ||
												valueSegment.ContainsDateMathSeparator() && DateMath.IsValidDateMathString(valueSegment.Utf8String());
										}
										break;
									case JsonToken.Number:
										if (!isDouble)
										{
											var numberSegment = segmentReader.ReadNumberSegment();
											if (numberSegment.IsDouble())
												isDouble = true;
											else
												isLong = true;
										}
										break;
								}
								break;
						}
					}
					else
						segmentReader.ReadNextBlock();

					if (isDate || isDouble)
						break;
				}

				if (isDate || isDouble)
					break;
			}

			segmentReader = new JsonReader(segment.Array, segment.Offset);

			if (isDate)
				return Deserialize<IDateRangeQuery>(ref segmentReader, formatterResolver);
			if (isDouble)
				return Deserialize<INumericRangeQuery>(ref segmentReader, formatterResolver);
			if (isLong)
				return Deserialize<ILongRangeQuery>(ref segmentReader, formatterResolver);

			return Deserialize<ITermRangeQuery>(ref segmentReader, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, IRangeQuery value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value)
			{
				case IDateRangeQuery dateRangeQuery:
					Serialize(ref writer, dateRangeQuery, formatterResolver);
					break;
				case INumericRangeQuery numericRangeQuery:
					Serialize(ref writer, numericRangeQuery, formatterResolver);
					break;
				case ILongRangeQuery longRangeQuery:
					Serialize(ref writer, longRangeQuery, formatterResolver);
					break;
				case ITermRangeQuery termRangeQuery:
					Serialize(ref writer, termRangeQuery, formatterResolver);
					break;
				default:
					DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IRangeQuery>()
						.Serialize(ref writer, value, formatterResolver);
					break;
			}
		}

		private static void Serialize<TQuery>(ref JsonWriter writer, TQuery value, IJsonFormatterResolver formatterResolver)
			where TQuery : IRangeQuery
		{
			var formatter = formatterResolver.GetFormatter<TQuery>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		private static IRangeQuery Deserialize<TQuery>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TQuery : IRangeQuery
		{
			var formatter = formatterResolver.GetFormatter<TQuery>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
