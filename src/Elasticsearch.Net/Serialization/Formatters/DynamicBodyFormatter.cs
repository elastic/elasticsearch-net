using System.Collections.Generic;

namespace Elasticsearch.Net
{
	internal class DynamicBodyFormatter : IJsonFormatter<DynamicBody>
	{
		private static readonly DictionaryFormatter<string, object> DictionaryFormatter =
			new DictionaryFormatter<string, object>();

		public void Serialize(ref JsonWriter writer, DynamicBody value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var formatter = formatterResolver.GetFormatter<object>();
			foreach (var kv in (IDictionary<string, object>)value)
			{
				writer.WritePropertyName(kv.Key);
				formatter.Serialize(ref writer, kv.Value, formatterResolver);
			}
			writer.WriteEndObject();
		}

		public DynamicBody Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dictionary = DictionaryFormatter.Deserialize(ref reader, formatterResolver);
			return DynamicBody.Create(dictionary);
		}
	}
}
