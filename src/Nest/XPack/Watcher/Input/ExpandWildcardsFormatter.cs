// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class ExpandWildcardsFormatter : IJsonFormatter<IEnumerable<ExpandWildcards>>
	{
		public IEnumerable<ExpandWildcards> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			return token == JsonToken.BeginArray
				? formatterResolver.GetFormatter<IEnumerable<ExpandWildcards>>().Deserialize(ref reader, formatterResolver)
				: new[] { formatterResolver.GetFormatter<ExpandWildcards>().Deserialize(ref reader, formatterResolver) };
		}

		public void Serialize(ref JsonWriter writer, IEnumerable<ExpandWildcards> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var wildcards = value.ToArray();

			switch (wildcards.Length)
			{
				case 1:
					var singleFormatter = formatterResolver.GetFormatter<ExpandWildcards>();
					singleFormatter.Serialize(ref writer, wildcards.First(), formatterResolver);
					break;
				case > 1:
					var formatter = formatterResolver.GetFormatter<IEnumerable<ExpandWildcards>>();
					formatter.Serialize(ref writer, wildcards, formatterResolver);
					break;
			}
		}
	}
}
