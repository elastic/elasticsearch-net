// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest.Utf8Json;

namespace Nest
{
	internal class MinimumShouldMatchFormatter : IJsonFormatter<MinimumShouldMatch>
	{
		public MinimumShouldMatch Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new MinimumShouldMatch(reader.ReadString());
				case JsonToken.Number:
					return new MinimumShouldMatch(reader.ReadInt32());
				default:
					throw new Exception($"Expected {nameof(JsonToken.String)} or {nameof(JsonToken.Number)} but got {token}");
			}
		}

		public void Serialize(ref JsonWriter writer, MinimumShouldMatch value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Tag)
			{
				case 0:
					writer.WriteInt32(value.Item1.Value);
					break;
				case 1:
					writer.WriteString(value.Item2);
					break;
			}
		}
	}
}
