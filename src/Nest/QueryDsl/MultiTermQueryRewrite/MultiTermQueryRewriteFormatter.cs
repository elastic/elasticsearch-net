using System;
using Utf8Json;

namespace Nest
{
	internal class MultiTermQueryRewriteFormatter : IJsonFormatter<MultiTermQueryRewrite>
	{
		public MultiTermQueryRewrite Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.Null)
				return null;

			if (token != JsonToken.String)
				throw new Exception($"Invalid token type {token} to deserialize {nameof(MultiTermQueryRewrite)} from");

			return MultiTermQueryRewrite.Create(reader.ReadString());
		}

		public void Serialize(ref JsonWriter writer, MultiTermQueryRewrite value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
				writer.WriteString(value.ToString());
		}
	}
}
