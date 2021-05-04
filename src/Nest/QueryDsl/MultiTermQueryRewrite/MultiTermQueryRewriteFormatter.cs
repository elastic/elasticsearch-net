// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest.Utf8Json;

namespace Nest
{
	internal class MultiTermQueryRewriteFormatter : IJsonFormatter<MultiTermQueryRewrite>
	{
		public MultiTermQueryRewrite Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			if (token != JsonToken.String)
				throw new Exception($"Invalid token type {token} to deserialize {nameof(MultiTermQueryRewrite)} from");

			return MultiTermQueryRewrite.Create(reader.ReadString());
		}

		public void Serialize(ref JsonWriter writer, MultiTermQueryRewrite value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
				writer.WriteString(value.ToString());
		}
	}
}
