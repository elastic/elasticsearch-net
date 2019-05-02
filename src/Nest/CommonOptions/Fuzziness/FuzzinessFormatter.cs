using Elasticsearch.Net;


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
						if (rawAuto.Array[rawAuto.Offset + i] == (byte)':')
							colonIndex = rawAuto.Offset + i;
						else if (rawAuto.Array[rawAuto.Offset + i] == (byte)',')
						{
							commaIndex = rawAuto.Offset + i;
							break;
						}
					}

					var low = NumberConverter.ReadInt32(rawAuto.Array, colonIndex + 1, out var _);
					var high = NumberConverter.ReadInt32(rawAuto.Array, commaIndex + 1, out var _);
					return Fuzziness.AutoLength(low, high);
				}
				case JsonToken.Number: {
					var value = reader.ReadNumberSegment();

					if (value.IsDouble())
					{
						var ratio = NumberConverter.ReadDouble(value.Array, value.Offset, out var count);
						return Fuzziness.Ratio(ratio);
					}
					else
					{
						var editDistance = NumberConverter.ReadInt32(value.Array, value.Offset, out var count);
						return Fuzziness.EditDistance(editDistance);
					}
				}
				default:
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
