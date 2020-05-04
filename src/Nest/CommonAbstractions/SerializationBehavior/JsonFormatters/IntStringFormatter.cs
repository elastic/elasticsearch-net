// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A formatter to deserialize an int into a string,
	/// and serialize a string into an int.
	/// </summary>
	internal class IntStringFormatter: IJsonFormatter<string>
	{
		public void Serialize(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver)
		{
			if (int.TryParse(value, out var i))
				writer.WriteInt32(i);
			else
				throw new InvalidOperationException($"expected a int string value, but found {value}");
		}

		public string Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			switch (reader.GetCurrentJsonToken())
			{
				case JsonToken.Number:
					return reader.ReadInt32().ToString(CultureInfo.InvariantCulture);
				case JsonToken.String:
					return reader.ReadString();
				default:
					throw new JsonParsingException($"expected string or int but found {reader.GetCurrentJsonToken()}");
			}
		}
	}
}
