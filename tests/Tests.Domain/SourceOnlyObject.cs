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
using Nest;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Tests.Domain
{
	public class SourceOnlyObject
	{
		[Ignore] [IgnoreDataMember]
		public string NotReadByDefaultSerializer { get; set; }

		[Ignore] [IgnoreDataMember]
		public string NotWrittenByDefaultSerializer { get; set; }
	}

	public class SourceOnlyUsingBuiltInConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("notWrittenByDefaultSerializer");
			writer.WriteValue("written");
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			//ignore json as provided
			var depth = reader.Depth;
			do
				reader.Read();
			while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);
			var p = new SourceOnlyObject
			{
				NotWrittenByDefaultSerializer = "written",
				NotReadByDefaultSerializer = "read"
			};
			return p;
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(SourceOnlyObject);
	}
}
