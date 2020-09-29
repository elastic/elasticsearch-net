// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	internal class SortOrderFormatter<TSortOrder> : IJsonFormatter<TSortOrder>
		where TSortOrder : class, ISortOrder, new()
	{
		public TSortOrder Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			var count = 0;
			var sortOrder = new TSortOrder();
			while (reader.ReadIsInObject(ref count))
			{
				sortOrder.Key = reader.ReadPropertyName();
				sortOrder.Order = formatterResolver.GetFormatter<SortOrder>()
					.Deserialize(ref reader, formatterResolver);
			}

			return sortOrder;
		}

		public void Serialize(ref JsonWriter writer, TSortOrder value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Key == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			writer.WritePropertyName(value.Key);
			formatterResolver.GetFormatter<SortOrder>().Serialize(ref writer, value.Order, formatterResolver);
			writer.WriteEndObject();
		}
	}
}
