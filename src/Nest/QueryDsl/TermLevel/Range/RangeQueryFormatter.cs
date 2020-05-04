// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Extensions;
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
