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
					var deleteItem = new BulkDeleteResponseItem();
					serializer.Populate(prop.Value.CreateReader(), deleteItem);
					if (deleteItem != null)
						deleteItem.Operation = key;
					return deleteItem;
				case "index":
					var indexItem = new BulkIndexResponseItem();
					serializer.Populate(prop.Value.CreateReader(), indexItem);
					if (indexItem != null)
						indexItem.Operation = key;
					return indexItem;
				case "create":
					var createItem = new BulkCreateResponseItem();
					serializer.Populate(prop.Value.CreateReader(), createItem);
					if (createItem != null)
						createItem.Operation = key;
					return createItem;
			}
			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(BulkOperationResponseItem);
		}
	}
}