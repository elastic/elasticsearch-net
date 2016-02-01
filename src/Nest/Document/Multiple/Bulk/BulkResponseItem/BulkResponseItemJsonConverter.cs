using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class BulkResponseItemJsonConverter : JsonConverter
	{
		
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			reader.Read();
			if (reader.TokenType != JsonToken.PropertyName)
				return null;

			var key = reader.Value as string;
			reader.Read();
			switch (key)
			{
				case "delete":
					var deleteItem = new BulkDeleteResponseItem();
					serializer.Populate(reader, deleteItem);
					if (deleteItem != null)
						deleteItem.Operation = key;
					reader.Read();
					return deleteItem;
				case "update":
					var updateItem = new BulkUpdateResponseItem();
					serializer.Populate(reader, updateItem);
					if (updateItem != null)
						updateItem.Operation = key;
					reader.Read();
					return updateItem;
				case "index":
					var indexItem = new BulkIndexResponseItem();
					serializer.Populate(reader, indexItem);
					if (indexItem != null)
						indexItem.Operation = key;
					reader.Read();
					return indexItem;
				case "create":
					var createItem = new BulkCreateResponseItem();
					serializer.Populate(reader, createItem);
					if (createItem != null)
						createItem.Operation = key;
					reader.Read();
					return createItem;
			}
			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(BulkResponseItemBase);
		}
	}
}