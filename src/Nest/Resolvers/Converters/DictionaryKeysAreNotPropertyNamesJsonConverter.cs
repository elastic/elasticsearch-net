using System;
using System.Collections;
using System.Globalization;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// JSON converter for IDictionary that ignores the contract resolver (e.g. CamelCasePropertyNamesContractResolver)
	/// when converting dictionary keys to property names.
	/// </summary>
	public class DictionaryKeysAreNotPropertyNamesJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(IDictionary).IsAssignableFrom(objectType);
		}

		public override bool CanRead
		{
			get { return false; }
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new InvalidOperationException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			IDictionary dictionary = (IDictionary) value;

			writer.WriteStartObject();

			foreach (DictionaryEntry entry in dictionary)
			{
				string key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
				writer.WritePropertyName(key);
				serializer.Serialize(writer, entry.Value);
			}

			writer.WriteEndObject();
		}
	}
}