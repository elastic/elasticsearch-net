using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Formatters;

namespace Elasticsearch.Net
{
	internal class DynamicDictionaryFormatter : IJsonFormatter<DynamicDictionary>
	{
		protected static readonly DictionaryFormatter<string, object> DictionaryFormatter =
			new DictionaryFormatter<string, object>();

		public void Serialize(ref JsonWriter writer, DynamicDictionary value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var formatter = formatterResolver.GetFormatter<object>();
			var count = 0;
			foreach (var kv in (IDictionary<string, DynamicValue>)value)
			{
				if (count > 0)
					writer.WriteValueSeparator();
				writer.WritePropertyName(kv.Key);
				formatter.Serialize(ref writer, kv.Value?.Value, formatterResolver);
				count++;
			}
			writer.WriteEndObject();
		}

		public DynamicDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dictionary = DictionaryFormatter.Deserialize(ref reader, formatterResolver);
			return DynamicDictionary.Create(dictionary);
		}
	}
}
