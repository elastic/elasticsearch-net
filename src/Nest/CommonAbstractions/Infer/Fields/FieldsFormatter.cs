using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	internal class FieldsFormatter : IJsonFormatter<Fields>
	{
		public Fields Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token != JsonToken.BeginArray)
			{
				reader.ReadNext();
				return null;
			}

			var fields = new Fields();
			var count = 0;
			while (reader.ReadIsInArray(ref count))
			{
				token = reader.GetCurrentJsonToken();
				switch (token)
				{
					case JsonToken.String:
						fields.And(reader.ReadString());
						break;
					case JsonToken.BeginObject:
						/// TODO 6.4 this is temporary until we add proper support for doc_values format
						var innerCount = 0;
						while (reader.ReadIsInObject(ref innerCount))
						{
							if (reader.ReadPropertyName() == "field") fields.And(reader.ReadString());
						}
						break;
				}
			}
			return fields;
		}

		public void Serialize(ref JsonWriter writer, Fields value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var formatter = formatterResolver.GetFormatter<List<Field>>();
			formatter.Serialize(ref writer, value.ListOfFields, formatterResolver);
		}
	}
}
