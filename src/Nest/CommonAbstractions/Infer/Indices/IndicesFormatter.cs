using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public class IndicesFormatter : IJsonFormatter<Indices>
	{
		public Indices Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
			{
				reader.ReadNext();
				return null;
			}

			var indices = new List<IndexName>();
			var count = 0;
			while (reader.ReadIsInArray(ref count))
			{
				var index = reader.ReadString();
				indices.Add(index);
			}
			return new Indices(indices);
		}

		public void Serialize(ref JsonWriter writer, Indices value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value._tag)
			{
				case 0:
					writer.WriteBeginArray();
					writer.WriteString("_all");
					writer.WriteEndArray();
					break;
				case 1:
					var settings = formatterResolver.GetConnectionSettings();
					writer.WriteBeginArray();
					for (var index = 0; index < value.Item2.Indices.Count; index++)
					{
						if (index > 0)
							writer.WriteValueSeparator();

						var indexName = value.Item2.Indices[index];
						writer.WriteString(indexName.GetString(settings));
					}
					writer.WriteEndArray();
					break;
			}
		}
	}
}
