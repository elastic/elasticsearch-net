using Elasticsearch.Net;

namespace Nest
{
	internal class TypesMultiSyntaxFormatter : IJsonFormatter<Types>
	{
		public Types Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.String)
			{
				Types types = reader.ReadString();
				return types;
			}

			return null;
		}

		public void Serialize(ref JsonWriter writer, Types value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value._tag)
			{
				case 0:
					writer.WriteNull();
					break;
				case 1:
					writer.WriteString(((IUrlParameter)value).GetString(formatterResolver.GetConnectionSettings()));
					break;
			}
		}
	}
}
