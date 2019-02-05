using Utf8Json;

namespace Nest
{
	internal class TimeFormatter : IJsonFormatter<Time>
	{
		public Time Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new Time(reader.ReadString());
				case JsonToken.Number:
					var milliseconds = reader.ReadInt64();
					if (milliseconds == -1)
						return Time.MinusOne;
					if (milliseconds == 0)
						return Time.Zero;

					return new Time(milliseconds);
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Time value, IJsonFormatterResolver formatterResolver)
		{
			if (value == Time.MinusOne) writer.WriteInt32(-1);
			else if (value == Time.Zero) writer.WriteInt32(0);
			else if (value.Factor.HasValue && value.Interval.HasValue) writer.WriteString(value.ToString());
			else if (value.Milliseconds != null) writer.WriteInt64((long)value.Milliseconds);
		}
	}
}
