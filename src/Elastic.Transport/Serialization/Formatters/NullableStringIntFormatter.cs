// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net.Utf8Json;

namespace Elasticsearch.Net
{
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
}
