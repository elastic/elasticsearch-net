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
		public Fuzziness Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			switch (token) {
				case JsonToken.String:
				{
					// TODO: read bytes from reader and avoid string allocation
					var rawAuto = reader.ReadString();
					var colonIndex = rawAuto.IndexOf(':');
					var commaIndex = rawAuto.IndexOf(',');
					if (colonIndex == -1 || commaIndex == -1)
						return Fuzziness.Auto;

					var low = int.Parse(rawAuto.Substring(colonIndex + 1, commaIndex - colonIndex - 1));
					var high = int.Parse(rawAuto.Substring(commaIndex + 1));
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
