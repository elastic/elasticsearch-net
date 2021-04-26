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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class NullableDateTimeEpochMillisecondsFormatter : IJsonFormatter<DateTime?>
	{
		public DateTime? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			switch (token)
			{
				case JsonToken.String:
				{
					var formatter = formatterResolver.GetFormatter<DateTime>();
					return formatter.Deserialize(ref reader, formatterResolver);
				}
				case JsonToken.Null:
				{
					reader.ReadNext();
					return null;
				}
				case JsonToken.Number:
				{
					var millisecondsSinceEpoch = reader.ReadDouble();
					var dateTimeOffset = DateTimeUtil.UnixEpoch.AddMilliseconds(millisecondsSinceEpoch);
					return dateTimeOffset.DateTime;
				}
				default:
					throw new Exception($"Cannot deserialize {nameof(DateTime)} from token {token}");
			}
		}

		public void Serialize(ref JsonWriter writer, DateTime? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var dateTimeDifference = (value.Value - DateTimeUtil.UnixEpoch).TotalMilliseconds;
			writer.WriteInt64((long)dateTimeDifference);
		}
	}
}
