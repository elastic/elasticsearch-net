// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Nest.Utf8Json;

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
			for (var i = 0; i < fields.Count; i++)
			{
				if (i > 0)
					writer.WriteValueSeparator();
				FieldFormatter.Serialize(ref writer, fields[i], formatterResolver);
			}
			writer.WriteEndArray();
		}
	}
}
