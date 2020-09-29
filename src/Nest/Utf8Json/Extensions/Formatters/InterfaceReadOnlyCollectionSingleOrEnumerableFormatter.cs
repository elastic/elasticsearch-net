// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Elasticsearch.Net.Utf8Json;

namespace Elasticsearch.Net
{
	internal class InterfaceReadOnlyCollectionSingleOrEnumerableFormatter<T> : IJsonFormatter<IReadOnlyCollection<T>>
	{
		public IReadOnlyCollection<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			return token == JsonToken.BeginArray
				? formatterResolver.GetFormatter<IReadOnlyCollection<T>>().Deserialize(ref reader, formatterResolver)
				: new ReadOnlyCollection<T>(new List<T>(1) { formatterResolver.GetFormatter<T>().Deserialize(ref reader, formatterResolver) });
		}

		public void Serialize(ref JsonWriter writer, IReadOnlyCollection<T> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var formatter = formatterResolver.GetFormatter<IReadOnlyCollection<T>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
