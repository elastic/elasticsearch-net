// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	internal class DistanceFormatter : IJsonFormatter<Distance>
	{
		public Distance Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.String)
			{
				reader.ReadNextBlock();
				return null;
			}

			var value = reader.ReadString();
			return value == null
				? null
				: new Distance(value);
		}

		public void Serialize(ref JsonWriter writer, Distance value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteString(value.ToString());
		}
	}
}
