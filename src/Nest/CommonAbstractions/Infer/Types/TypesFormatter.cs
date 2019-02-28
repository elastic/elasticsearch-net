using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	internal class TypesFormatter : IJsonFormatter<Types>
	{
		public Types Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
			{
				reader.ReadNext();
				return null;
			}

			var formatter = formatterResolver.GetFormatter<IEnumerable<TypeName>>();
			var types = formatter.Deserialize(ref reader, formatterResolver);
			return new Types(types);
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
					var settings = formatterResolver.GetConnectionSettings();
					writer.WriteBeginArray();
					for (var index = 0; index < value.Item2.Types.Count; index++)
					{
						if (index > 0)
							writer.WriteValueSeparator();

						IUrlParameter type = value.Item2.Types[index];
						writer.WriteString(type.GetString(settings));
					}
					writer.WriteEndArray();
					break;
			}
		}
	}
}
