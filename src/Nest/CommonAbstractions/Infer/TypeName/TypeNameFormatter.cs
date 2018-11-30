using Utf8Json;

namespace Nest
{
	internal class TypeNameFormatter : IJsonFormatter<TypeName>
	{
		public TypeName Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.String)
			{
				TypeName typeName = reader.ReadString();
				return typeName;
			}
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
	}
}
