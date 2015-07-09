using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class TokenFilterCollectionConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var items = (IDictionary<string, TokenFilterBase>)value;
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
			var result = existingValue as Dictionary<string, TokenFilterBase> ?? new Dictionary<string, TokenFilterBase>();

			foreach (var childProperty in o.Children<JProperty>())
			{
				var FieldName = childProperty.Name;
				var typeProperty = ((JObject)childProperty.Value).Property("type");
				typeProperty.Remove();

				var typePropertyValue = typeProperty.Value.ToString().Replace("_", string.Empty);

				TokenFilterBase item;
				var itemType = Type.GetType("Nest." + typePropertyValue + "TokenFilter", false, true);
				if (itemType != null)
				{
					item = serializer.Deserialize(childProperty.Value.CreateReader(), itemType) as TokenFilterBase;
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
			return typeof(IDictionary<string,TokenFilterBase>).IsAssignableFrom(objectType);
		}

		public override bool CanRead
		{
			get { return true; }
		}
	}
}