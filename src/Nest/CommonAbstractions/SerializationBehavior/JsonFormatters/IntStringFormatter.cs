/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
