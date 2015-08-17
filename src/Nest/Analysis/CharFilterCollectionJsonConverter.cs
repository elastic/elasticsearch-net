using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class CharFilterCollectionJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var items = (IDictionary<string, CharFilterBase>)value;
			writer.WriteStartObject();
			foreach (var item in items)
			{
				writer.WritePropertyName(item.Key);
				serializer.Serialize(writer, item.Value);
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);
			var result = existingValue as Dictionary<string, CharFilterBase> ?? new Dictionary<string, CharFilterBase>();

			foreach (var childProperty in o.Children<JProperty>())
			{
				var FieldName = childProperty.Name;
				var typeProperty = ((JObject)childProperty.Value).Property("type");
				typeProperty.Remove();

				var typePropertyValue = typeProperty.Value.ToString().Replace("_", string.Empty);

				CharFilterBase item;
				var itemType = Type.GetType("Nest." + typePropertyValue + "CharFilter", false, true);
				if (itemType != null)
				{
					item = serializer.Deserialize(childProperty.Value.CreateReader(), itemType) as CharFilterBase;
				}
				else
				{
					continue;
				}

				result[FieldName] = item;
			}
			return result;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(IDictionary<string,CharFilterBase>).IsAssignableFrom(objectType);
		}

		public override bool CanRead
		{
			get { return true; }
		}
	}
}