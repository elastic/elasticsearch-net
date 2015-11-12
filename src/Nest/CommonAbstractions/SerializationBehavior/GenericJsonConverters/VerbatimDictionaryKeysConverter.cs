using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// JSON converter for IDictionary that ignores the contract resolver (e.g. CamelCaseFieldNamesContractResolver)
	/// when converting dictionary keys to property names.
	/// </summary>
	internal class VerbatimDictionaryKeysJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => typeof (IDictionary).IsAssignableFrom(t);

		public override bool CanRead => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new InvalidOperationException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var contract = serializer.ContractResolver as SettingsContractResolver;

			var dictionary = value as IDictionary;
			if (dictionary == null) return;

			writer.WriteStartObject();
			foreach (DictionaryEntry entry in dictionary)
			{
				if (entry.Value == null && serializer.NullValueHandling == NullValueHandling.Ignore)
					continue;
				string key;
				var fieldName = entry.Key as Field;
				var propertyName = entry.Key as PropertyName;
				var indexName = entry.Key as IndexName;
				var typeName = entry.Key as TypeName;
				if (contract == null)
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
				else if (fieldName != null)
					key = contract.Infer.Field(fieldName);
				else if (propertyName != null)
					key = contract.Infer.PropertyName(propertyName);
				else if (indexName != null)
					key = contract.Infer.IndexName(indexName);
				else if (typeName != null)
					key = contract.Infer.TypeName(typeName);
				else
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
				writer.WritePropertyName(key);
				serializer.Serialize(writer, entry.Value);
			}

			writer.WriteEndObject();
		}
	}

	internal class VerbatimDictionaryKeysJsonConverter<TIsADictionary, TKey, TValue> : VerbatimDictionaryKeysJsonConverter
		where TIsADictionary : IIsADictionary
	{
		public override bool CanConvert(Type t) => true;

		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var dictionary = serializer.Deserialize<Dictionary<TKey, TValue>>(reader);
			return typeof(TIsADictionary).CreateInstance<TIsADictionary>(dictionary);
		}
	}
}