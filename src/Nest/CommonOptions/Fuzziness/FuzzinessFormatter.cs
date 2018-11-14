using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class FuzzinessFormatter : IJsonFormatter<IFuzziness>
	{
		public IFuzziness Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
				return Fuzziness.Auto;

			if (token == JsonToken.Number)
			{
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

			return null;
		}

		public void Serialize(ref JsonWriter writer, IFuzziness value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value.Auto)
				writer.WriteString("AUTO");
			else if (value.EditDistance.HasValue)
				writer.WriteInt32(value.EditDistance.Value);
			else if (value.Ratio.HasValue)
				writer.WriteDouble(value.Ratio.Value);
			else
				writer.WriteNull();
		}
	}
}
