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
using Newtonsoft.Json;

namespace Nest.JsonNetSerializer.Converters
{
	/// <summary>
	/// Included for compatibility reasons
	/// </summary>
	internal class TimeSpanToStringConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				writer.WriteNull();
			else
			{
				var timeSpan = (TimeSpan)value;
				writer.WriteValue(timeSpan.Ticks);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null: return null;
				case JsonToken.String: return TimeSpan.Parse((string)reader.Value);
				case JsonToken.Integer: return new TimeSpan((long)reader.Value);
			}
			throw new JsonSerializationException($"Cannot convert token of type {reader.TokenType} to {objectType}.");
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
	}
}
