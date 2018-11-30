using System;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class ShardStoreFormatter : IJsonFormatter<ShardStore>
	{
		public ShardStore Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			ShardStore shardStore = null;
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				if (count == 1)
				{
					var id = reader.ReadPropertyName();
					var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ShardStore>();
					shardStore = formatter.Deserialize(ref reader, formatterResolver);
					shardStore.Id = id;
				}
			}

			return shardStore;
		}

		public void Serialize(ref JsonWriter writer, ShardStore value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
