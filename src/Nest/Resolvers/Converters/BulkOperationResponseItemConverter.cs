using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{

	public class BulkOperationResponseItemConverter : JsonConverter
	{

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);

			var prop = o.Properties().First();
			var key = prop.Name;
			switch (key)
			{
				case "delete":
					var deleteItem = (BulkDeleteResponseItem)serializer.Deserialize(prop.Value.CreateReader(), typeof(BulkDeleteResponseItem));
					if (deleteItem != null)
						deleteItem.Operation = key;
					return deleteItem;
				case "index":
				case "create":
					var indexItem = (BulkIndexResponseItem)serializer.Deserialize(prop.Value.CreateReader(), typeof(BulkIndexResponseItem));
					if (indexItem != null)
						indexItem.Operation = key;
					return indexItem;
			}
			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(BulkOperationResponseItem);
		}
	}
}