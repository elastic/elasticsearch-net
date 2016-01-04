using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class NoMatchQueryJsonConverter : ReserializeJsonConverter<QueryContainer, IQueryContainer>
	{

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var nq = value as NoMatchQueryContainer;
			var q = value as QueryContainer;
			if (nq == null && q == null)
			{
				writer.WriteNull();
				return;
			}
			if (nq != null && nq.Shortcut.HasValue)
			{
				serializer.Serialize(writer, nq.Shortcut.Value);
				return;
			}
			base.WriteJson(writer, value, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String)
				return base.ReadJson(reader, objectType, existingValue, serializer);

			var en = serializer.Deserialize<NoMatchShortcut>(reader);
			return new NoMatchQueryContainer {Shortcut = en};
		}
	}
}