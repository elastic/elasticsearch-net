using System.Collections.Generic;
using Utf8Json;
using Utf8Json.Formatters;

namespace Nest
{
	internal class FieldsFormatter : IJsonFormatter<Fields>
	{
		private static readonly FieldFormatter FieldFormatter = new FieldFormatter();

		public Fields Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
			{
				reader.ReadNextBlock();
				return null;
			}

			var count = 0;
			var fields = new List<Field>();
			while (reader.ReadIsInArray(ref count))
			{
				var field = FieldFormatter.Deserialize(ref reader, formatterResolver);
				if (field != null)
					fields.Add(field);
			}
			return new Fields(fields);
		}

		public void Serialize(ref JsonWriter writer, Fields value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var fields = value.ListOfFields;
			writer.WriteBeginArray();
			for (int i = 0; i < fields.Count; i++)
			{
				if (i > 0)
					writer.WriteValueSeparator();
				FieldFormatter.Serialize(ref writer, fields[i], formatterResolver);
			}
			writer.WriteEndArray();
		}
	}
}
