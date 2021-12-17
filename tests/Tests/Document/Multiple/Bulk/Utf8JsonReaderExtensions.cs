// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Text.Json;

namespace Tests.Document.Multiple;

internal static class Utf8JsonReaderExtensions
{
	public static byte[] GetBytesUntilEndOfObject(ref this Utf8JsonReader reader)
	{
		var bytes = new List<byte>(); // probably use an array and grow as needed

		var depth = 0;

		do
		{
			// prototype code - not efficient
			// todo - null, bool, number tokens
			switch (reader.TokenType)
			{
				case JsonTokenType.StartObject:
					depth++;
					bytes.Add((byte)'{');
					break;
				case JsonTokenType.StartArray:
					bytes.Add((byte)'[');
					break;
				case JsonTokenType.EndArray:
					var lastComma = bytes.LastIndexOf((byte)',');
					if (lastComma == bytes.Count - 1)
						bytes.RemoveAt(lastComma);
					bytes.Add((byte)']');
					bytes.Add((byte)',');
					break;
				case JsonTokenType.PropertyName:
					bytes.Add((byte)'"');
					bytes.AddRange(reader.ValueSpan.ToArray()); // could use span and copy
					bytes.Add((byte)'"');
					bytes.Add((byte)':');
					break;
				case JsonTokenType.String:
					bytes.Add((byte)'"');
					bytes.AddRange(reader.ValueSpan.ToArray()); 
					bytes.Add((byte)'"');
					bytes.Add((byte)',');
					break;
				case JsonTokenType.EndObject:
					depth--;
					var lastComma2 = bytes.LastIndexOf((byte)',');
					if (lastComma2 == bytes.Count - 1)
						bytes.RemoveAt(lastComma2);
					bytes.Add((byte)'}');
					bytes.Add((byte)',');
					break;
			}
		}
		while (reader.Read() && (depth > 1 || reader.TokenType != JsonTokenType.EndObject));

		var lastComma3 = bytes.LastIndexOf((byte)',');
		if (lastComma3 == bytes.Count - 1)
			bytes.RemoveAt(lastComma3);

		bytes.Add((byte)'}');

		return bytes.ToArray();
	}
}
