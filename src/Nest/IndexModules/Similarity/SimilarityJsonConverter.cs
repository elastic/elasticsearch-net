using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal class SimilarityCollectionJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(ISimilarity).IsAssignableFrom(objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);
			var result = existingValue as ISimilarity;
			
			//TODO This is broken
			foreach (var childProperty in o.Children<JProperty>())
			{
				var FieldName = childProperty.Name;
				var typeProperty = ((JObject)childProperty.Value).Property("type");
				typeProperty.Remove();

				var typePropertyValue = typeProperty.Value.ToString();
				var itemType = Type.GetType("Nest." + typePropertyValue + "Similarity", false, true);
				
				ISimilarity item;

				if (itemType != null)
				{
					item = serializer.Deserialize(childProperty.Value.CreateReader(), itemType) as ISimilarity;
				}
				else
				{
					continue;
				}

			}

			return result;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
			//var items = (ISimilarity)value;
			//writer.WriteStartObject();
			//foreach(var item in items)
			//{
			//	writer.WritePropertyName(item.Key);
			//	serializer.Serialize(writer, item.Value);
			//}
			//writer.WriteEndObject();
		}
	}
}
