// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	internal class FuzzinessInterfaceFormatter : IJsonFormatter<IFuzziness>
	{
		public void Serialize(ref JsonWriter writer, IFuzziness value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value.Auto)
			{
				if (!value.Low.HasValue || !value.High.HasValue)
					writer.WriteString("AUTO");
				else
					writer.WriteString($"AUTO:{value.Low},{value.High}");
			}
			else if (value.EditDistance.HasValue)
				writer.WriteInt32(value.EditDistance.Value);
			else if (value.Ratio.HasValue)
				writer.WriteDouble(value.Ratio.Value);
			else
				writer.WriteNull();
		}

		public IFuzziness Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Fuzziness>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}

	internal class FuzzinessFormatter : IJsonFormatter<Fuzziness>
	{
		private static readonly byte[] AutoBytes = JsonWriter.GetEncodedPropertyNameWithoutQuotation("AUTO");

		public Fuzziness Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			switch (token) {
				case JsonToken.String:
				{
					var rawAuto = reader.ReadStringSegmentUnsafe();
					if (rawAuto.EqualsBytes(AutoBytes))
						return Fuzziness.Auto;

					var colonIndex = -1;
					var commaIndex = -1;
					for (var i = AutoBytes.Length; i < rawAuto.Count; i++)
					{
						// ReSharper disable once PossibleNullReferenceException
						if (rawAuto.Array[rawAuto.Offset + i] == (byte)':')
							colonIndex = rawAuto.Offset + i;
						else if (rawAuto.Array[rawAuto.Offset + i] == (byte)',')
						{
							commaIndex = rawAuto.Offset + i;
							break;
						}
					}

					var low = NumberConverter.ReadInt32(rawAuto.Array, colonIndex + 1, out _);
					var high = NumberConverter.ReadInt32(rawAuto.Array, commaIndex + 1, out _);
					return Fuzziness.AutoLength(low, high);
				}
				case JsonToken.Number:
				{
					var value = reader.ReadNumberSegment();

					if (value.IsDouble())
					{
						var ratio = NumberConverter.ReadDouble(value.Array, value.Offset, out _);
						return Fuzziness.Ratio(ratio);
					}
					else
					{
						var editDistance = NumberConverter.ReadInt32(value.Array, value.Offset, out _);
						return Fuzziness.EditDistance(editDistance);
					}
				}
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Fuzziness value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<IFuzziness>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
