using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public class SingleOrEnumerableFormatter<T> : IJsonFormatter<IEnumerable<T>>
	{
		public IEnumerable<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			return token == JsonToken.BeginArray
				? formatterResolver.GetFormatter<IEnumerable<T>>().Deserialize(ref reader, formatterResolver)
				: new[] { formatterResolver.GetFormatter<T>().Deserialize(ref reader, formatterResolver) };
		}

		public void Serialize(ref JsonWriter writer, IEnumerable<T> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var formatter = formatterResolver.GetFormatter<IEnumerable<T>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
