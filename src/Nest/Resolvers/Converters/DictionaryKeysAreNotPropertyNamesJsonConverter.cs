using System;
using System.Collections;
using System.Globalization;
using Nest.Resolvers;
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
			var contract = serializer.ContractResolver as ElasticContractResolver;
			writer.WriteStartObject();

			foreach (DictionaryEntry entry in dictionary)
			{
				if (entry.Value == null)
					continue;
				string key;
				var pp = entry.Key as PropertyPathMarker;
				var pn = entry.Key as PropertyNameMarker; 
				if (pp != null)
					key = contract.Infer.PropertyPath(pp);
				else if (pn != null)
					key = contract.Infer.PropertyName(pn);
				else
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
				writer.WritePropertyName(key);
				serializer.Serialize(writer, entry.Value);
			}

			writer.WriteEndObject();
		}
	}
}