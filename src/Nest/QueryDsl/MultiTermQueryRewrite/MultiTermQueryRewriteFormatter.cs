using Newtonsoft.Json;
using Utf8Json;

namespace Nest {
	internal class MultiTermQueryRewriteFormatter : IJsonFormatter<MultiTermQueryRewrite>
	{
		public void Serialize(ref Utf8Json.JsonWriter writer, MultiTermQueryRewrite value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
				writer.WriteString(value.ToString());
		}

		public MultiTermQueryRewrite Deserialize(ref Utf8Json.JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			
			if (token == Utf8Json.JsonToken.Null)
				return null;

			if (token != Utf8Json.JsonToken.String)
				throw new JsonSerializationException($"Invalid token type {token} to deserialize {nameof(MultiTermQueryRewrite)} from");

			return MultiTermQueryRewrite.Create(reader.ReadString());
		}
	}
}