using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// JSON converter for IDictionary that ignores the contract resolver (e.g. CamelCaseFieldNamesContractResolver)
	/// when converting dictionary keys to property names.
	/// </summary>
	internal class VerbatimDictionaryKeysJsonConverter<TKey, TValue> : JsonConverter
	{
		public override bool CanConvert(Type t) => typeof (IDictionary<TKey, TValue>).IsAssignableFrom(t);

		public override bool CanRead => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dictionary = value as IDictionary<TKey, TValue>;
			if (dictionary == null) return;

			var settings = serializer.GetConnectionSettings();
			var seenEntries = new Dictionary<string, TValue>(dictionary.Count);
			var keyIsString = typeof(TKey) == typeof(string);
			var keyIsField = typeof(TKey) == typeof(Field);
			var keyIsPropertyName = typeof(TKey) == typeof(PropertyName);
			var keyIsIndexName = typeof(TKey) == typeof(IndexName);
			var keyIsTypeName = typeof(TKey) == typeof(TypeName);

			foreach (var entry in dictionary)
			{
				if (entry.Value == null && serializer.NullValueHandling == NullValueHandling.Ignore)
					continue;
				string key;
				if (keyIsString)
					key = entry.Key?.ToString();
				else if (settings == null)
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
				else if (keyIsField)
				{
					var fieldName = entry.Key as Field;
					key = settings.Inferrer.Field(fieldName);
				}
				else if (keyIsPropertyName)
				{
					var propertyName = entry.Key as PropertyName;
					if (propertyName?.Property != null)
					{
						IPropertyMapping mapping;
						if (settings.PropertyMappings.TryGetValue(propertyName.Property, out mapping) && mapping.Ignore)
						{
							continue;
						}
					}

					key = settings.Inferrer.PropertyName(propertyName);
				}
				else if (keyIsIndexName)
				{
					var indexName = entry.Key as IndexName;
					key = settings.Inferrer.IndexName(indexName);
				}
				else if (keyIsTypeName)
				{
					var typeName = entry.Key as TypeName;
					key = settings.Inferrer.TypeName(typeName);
				}
				else
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);

				if (key != null)
					seenEntries[key] = entry.Value;
			}

			writer.WriteStartObject();
			foreach(var entry in seenEntries)
			{
				writer.WritePropertyName(entry.Key);
				serializer.Serialize(writer, entry.Value);
			}
			writer.WriteEndObject();
		}
	}

	internal class VerbatimDictionaryKeysJsonConverter<TIsADictionary, TKey, TValue> : VerbatimDictionaryKeysJsonConverter<TKey, TValue>
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
