using Utf8Json;

namespace Nest
{
	internal class NullableStringBooleanFormatter : IJsonFormatter<bool?>
	{
		public void Serialize(ref JsonWriter writer, bool? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBoolean(value.Value);
		}

		public bool? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				case JsonToken.String:
					var s = reader.ReadString();
					if (!bool.TryParse(s, out var b))
						throw new JsonParsingException($"Cannot parse {typeof(bool).FullName} from: {s}");

					return b;
				case JsonToken.True:
				case JsonToken.False:
					return reader.ReadBoolean();
				default:
					throw new JsonParsingException($"Cannot parse {typeof(bool).FullName} from: {token}");
			}
		}
	}

	internal class NullableStringIntFormatter : IJsonFormatter<int?>
	{
		public void Serialize(ref JsonWriter writer, int? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteInt32(value.Value);
		}

		public int? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				case JsonToken.String:
					var s = reader.ReadString();
					if (!int.TryParse(s, out var i))
						throw new JsonParsingException($"Cannot parse {typeof(int).FullName} from: {s}");

					return i;
				case JsonToken.Number:
					return reader.ReadInt32();
				default:
					throw new JsonParsingException($"Cannot parse {typeof(int).FullName} from: {token}");
			}
		}
	}

	internal class NullableStringDoubleFormatter : IJsonFormatter<double?>
	{
		public void Serialize(ref JsonWriter writer, double? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteDouble(value.Value);
		}

		public double? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				case JsonToken.String:
					var s = reader.ReadString();
					if (!double.TryParse(s, out var d))
						throw new JsonParsingException($"Cannot parse {typeof(double).FullName} from: {s}");

					return d;
				case JsonToken.Number:
					return reader.ReadDouble();
				default:
					throw new JsonParsingException($"Cannot parse {typeof(double).FullName} from: {token}");
			}
		}
	}
}
