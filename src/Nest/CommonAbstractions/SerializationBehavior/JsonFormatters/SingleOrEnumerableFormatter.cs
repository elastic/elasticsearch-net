// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class SingleOrEnumerableFormatter<T> : IJsonFormatter<IEnumerable<T>>
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
