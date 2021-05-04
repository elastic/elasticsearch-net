// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class ReindexRoutingFormatter : IJsonFormatter<ReindexRouting>
	{
		public ReindexRouting Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var value = reader.ReadString();
			switch (value)
			{
				case "keep": return ReindexRouting.Keep;
				case "discard": return ReindexRouting.Discard;
				default: return new ReindexRouting(value);
			}
		}

		public void Serialize(ref JsonWriter writer, ReindexRouting value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) writer.WriteNull();
			else writer.WriteString(value.ToString());
		}
	}
}
