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
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	internal class ShardStoreFormatter : IJsonFormatter<ShardStore>
	{
		private AutomataDictionary Fields = new AutomataDictionary
		{
			{ "allocation", 0 },
			{ "allocation_id", 1 },
		};

		public ShardStore Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			ShardStore shardStore = null;
			var count = 0;
			ShardStoreAllocation allocation = default;
			string allocationId = null;
			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							allocation = formatterResolver.GetFormatter<ShardStoreAllocation>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 1:
							allocationId = reader.ReadString();
							break;
					}
				}
				else
				{
					var formatter = DynamicObjectResolver.AllowPrivateExcludeNullCamelCase.GetFormatter<ShardStore>();
					shardStore = formatter.Deserialize(ref reader, formatterResolver);
					shardStore.Id = property.Utf8String();
				}
			}

			if (shardStore != null)
			{
				shardStore.Allocation = allocation;
				shardStore.AllocationId = allocationId;
			}

			return shardStore;
		}

		public void Serialize(ref JsonWriter writer, ShardStore value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
