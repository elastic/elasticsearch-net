// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	internal class RoutingFormatter : IJsonFormatter<Routing>
	{
		public Routing Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			reader.GetCurrentJsonToken() == JsonToken.Number
				? new Routing(reader.ReadInt64())
				: new Routing(reader.ReadString());

		public void Serialize(ref JsonWriter writer, Routing value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value.Document != null)
			{
				var settings = formatterResolver.GetConnectionSettings();
				var documentId = settings.Inferrer.Routing(value.Document.GetType(), value.Document);
				writer.WriteString(documentId);
			}
			else if (value.DocumentGetter != null)
			{
				var settings = formatterResolver.GetConnectionSettings();
				var doc = value.DocumentGetter();
				var documentId = settings.Inferrer.Routing(doc.GetType(), doc);
				writer.WriteString(documentId);
			}
			else if (value.LongValue != null) writer.WriteInt64(value.LongValue.Value);
			else writer.WriteString(value.StringValue);
		}
	}
}
