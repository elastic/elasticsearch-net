using Utf8Json;

namespace Nest
{
	internal class SortOrderFormatter<TSortOrder> : IJsonFormatter<TSortOrder>
		where TSortOrder : class, ISortOrder, new()
	{
		public TSortOrder Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

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
			// TODO: Should this be a Field?
			writer.WritePropertyName(value.Key);
			formatterResolver.GetFormatter<SortOrder>().Serialize(ref writer, value.Order, formatterResolver);
			writer.WriteEndObject();
		}
	}
}
