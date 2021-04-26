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

using Nest.Utf8Json;

namespace Nest
{
	internal class TimeFormatter : IJsonFormatter<Time>
	{
		public Time Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new Time(reader.ReadString());
				case JsonToken.Number:
					var milliseconds = reader.ReadInt64();
					if (milliseconds == -1)
						return Time.MinusOne;
					if (milliseconds == 0)
						return Time.Zero;

					return new Time(milliseconds);
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Time value, IJsonFormatterResolver formatterResolver)
		{
			if (value == Time.MinusOne) writer.WriteInt32(-1);
			else if (value == Time.Zero) writer.WriteInt32(0);
			else if (value.Factor.HasValue && value.Interval.HasValue) writer.WriteString(value.ToString());
			else if (value.Milliseconds != null) writer.WriteInt64((long)value.Milliseconds);
		}
	}
}
