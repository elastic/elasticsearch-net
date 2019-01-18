using Utf8Json;

namespace Nest
{
	internal class TypeNameFormatter : IJsonFormatter<TypeName>, IObjectPropertyNameFormatter<TypeName>
	{
		public TypeName Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.String)
			{
				TypeName typeName = reader.ReadString();
				return typeName;
			}

			reader.ReadNextBlock();
			return null;
		}

		public void Serialize(ref JsonWriter writer, TypeName value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			var typeName = settings.Inferrer.TypeName(value);
			writer.WriteString(typeName);
		}

		public void SerializeToPropertyName(ref JsonWriter writer, TypeName value, IJsonFormatterResolver formatterResolver) =>
			Serialize(ref writer, value, formatterResolver);

		public TypeName DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Deserialize(ref reader, formatterResolver);
	}
}
