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
using Nest.Utf8Json;
namespace Nest
{
	internal class CatFielddataRecordFormatter : IJsonFormatter<CatFielddataRecord>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "id", 0 },
			{ "node", 1 },
			{ "n", 1 },
			{ "host", 2 },
			{ "ip", 3 },
			{ "field", 4 },
			{ "size", 5 }
		};

		public CatFielddataRecord Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var record = new CatFielddataRecord();
			var count = 0;

			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (AutomataDictionary.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							record.Id = reader.ReadString();
							break;
						case 1:
							record.Node = reader.ReadString();
							break;
						case 2:
							record.Host = reader.ReadString();
							break;
						case 3:
							record.Ip = reader.ReadString();
							break;
						case 4:
							record.Field = reader.ReadString();
							break;
						case 5:
							record.Size = reader.ReadString();
							break;
					}
				}
			}

			return record;
		}

		public void Serialize(ref JsonWriter writer, CatFielddataRecord value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
