// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest.Utf8Json;
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
