// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	internal class NullableStringLongFormatter : IJsonFormatter<long?>
	{
		public long? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				case JsonToken.String:
					var s = reader.ReadString();
					if (!long.TryParse(s, out var l))
						throw new JsonParsingException($"Cannot parse {typeof(long).FullName} from: {s}");

					return l;
				case JsonToken.Number:
					return reader.ReadInt64();
				default:
					throw new JsonParsingException($"Cannot parse {typeof(long).FullName} from: {token}");
			}
		}

		public void Serialize(ref JsonWriter writer, long? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteInt64(value.Value);
		}
	}

	internal class NullableStringBooleanFormatter : IJsonFormatter<bool?>
	{
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

		public void Serialize(ref JsonWriter writer, bool? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBoolean(value.Value);
		}
	}

	internal class StringLongFormatter : IJsonFormatter<long>
	{
		public long Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					var s = reader.ReadString();
					if (!long.TryParse(s, out var i))
						throw new JsonParsingException($"Cannot parse {typeof(long).FullName} from: {s}");

					return i;
				case JsonToken.Number:
					return reader.ReadInt64();
				default:
					throw new JsonParsingException($"Cannot parse {typeof(long).FullName} from: {token}");
			}
		}

		public void Serialize(ref JsonWriter writer, long value, IJsonFormatterResolver formatterResolver) =>
			writer.WriteInt64(value);
	}

	internal class StringIntFormatter : IJsonFormatter<int>
	{
		public int Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
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

		public void Serialize(ref JsonWriter writer, int value, IJsonFormatterResolver formatterResolver) =>
			writer.WriteInt32(value);
	}

	internal class NullableStringIntFormatter : IJsonFormatter<int?>
	{
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

		public void Serialize(ref JsonWriter writer, int? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteInt32(value.Value);
		}
	}

	internal class NullableStringDoubleFormatter : IJsonFormatter<double?>
	{
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

		public void Serialize(ref JsonWriter writer, double? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteDouble(value.Value);
		}
	}
}
