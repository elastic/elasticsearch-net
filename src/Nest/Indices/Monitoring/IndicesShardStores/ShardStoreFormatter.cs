using System;
using Utf8Json;
using Utf8Json.Internal;
using Utf8Json.Resolvers;

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
