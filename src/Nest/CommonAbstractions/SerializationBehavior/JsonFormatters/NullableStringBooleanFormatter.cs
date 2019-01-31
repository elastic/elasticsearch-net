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
					var boolString = reader.ReadString();
					if (!bool.TryParse(boolString, out var b))
						throw new JsonParsingException($"Cannot parse {typeof(bool).FullName} from: {boolString}");

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
					var intString = reader.ReadString();
					if (!int.TryParse(intString, out var b))
						throw new JsonParsingException($"Cannot parse {typeof(int).FullName} from: {intString}");

					return b;
				case JsonToken.Number:
					return reader.ReadInt32();
				default:
					throw new JsonParsingException($"Cannot parse {typeof(int).FullName} from: {token}");
			}
		}
	}
}
