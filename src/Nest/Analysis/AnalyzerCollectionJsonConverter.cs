using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{

	public class AnalyzerCollectionJsonConverter: JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var items = (IDictionary<string, AnalyzerBase>)value;
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
			var result = existingValue as Dictionary<string, AnalyzerBase> ?? new Dictionary<string, AnalyzerBase>();

			foreach (var childProperty in o.Children<JProperty>())
			{
				var FieldName = childProperty.Name;
				var typeProperty = ((JObject)childProperty.Value).Property("type");
				if (typeProperty != null)
					typeProperty.Remove();

				var typePropertyValue = (typeProperty != null)
					? typeProperty.Value.ToString()
					: "custom";

				Language language;
				if (Enum.TryParse(typePropertyValue, true, out language))
				{
					typePropertyValue = "Language";
				}

				var itemType = Type.GetType("Nest." + typePropertyValue + "Analyzer", false, true);
				AnalyzerBase item;
				if (itemType == typeof(LanguageAnalyzer))
				{
					item = new LanguageAnalyzer(language);
					serializer.Populate(childProperty.Value.CreateReader(), item);
				}
				else if (itemType != null)
				{
					item = serializer.Deserialize(childProperty.Value.CreateReader(), itemType) as AnalyzerBase;
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
			return typeof(IDictionary<string,AnalyzerBase>).IsAssignableFrom(objectType);
		}

		public override bool CanRead
		{
			get { return true; }
		}
	}
}