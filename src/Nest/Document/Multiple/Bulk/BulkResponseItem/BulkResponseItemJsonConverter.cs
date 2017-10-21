using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class BulkResponseItemJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			reader.Read();
			if (reader.TokenType != JsonToken.PropertyName)
				return null;

			var key = (string)reader.Value;
			reader.Read();
			switch (key)
			{
				case "delete":
					var deleteItem = new BulkDeleteResponseItem();
					serializer.Populate(reader, deleteItem);
					deleteItem.Operation = key;
					reader.Read();
					return deleteItem;
				case "update":
					var updateItem = new BulkUpdateResponseItem();
					serializer.Populate(reader, updateItem);
					updateItem.Operation = key;
					reader.Read();
					return updateItem;
				case "index":
					var indexItem = new BulkIndexResponseItem();
					serializer.Populate(reader, indexItem);
					indexItem.Operation = key;
					reader.Read();
					return indexItem;
				case "create":
					var createItem = new BulkCreateResponseItem();
					serializer.Populate(reader, createItem);
					createItem.Operation = key;
					reader.Read();
					return createItem;
				default:
					return null;
			}
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
