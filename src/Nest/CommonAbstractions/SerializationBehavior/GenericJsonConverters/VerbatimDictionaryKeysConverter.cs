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
	internal class VerbatimDictionaryKeysJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => typeof(IDictionary).IsAssignableFrom(t);

		public override bool CanRead => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dictionary = (IDictionary)value;
			if (dictionary == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = serializer.GetConnectionSettings();
			var seenEntries = new Dictionary<string, object>(dictionary.Count);

			Field fieldName;
			PropertyName propertyName;
			IndexName indexName;
			TypeName typeName;

			foreach (DictionaryEntry entry in dictionary)
			{
				if (entry.Value == null && serializer.NullValueHandling == NullValueHandling.Ignore)
					continue;
				string key;
				if (settings == null)
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
				else if (AsType(entry.Key, out fieldName))
				{
					key = settings.Inferrer.Field(fieldName);
				}
				else if (AsType(entry.Key, out propertyName))
				{
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
				else if (AsType(entry.Key, out indexName))
				{
					key = settings.Inferrer.IndexName(indexName);
				}
				else if (AsType(entry.Key, out typeName))
				{
					key = settings.Inferrer.TypeName(typeName);
				}
				else
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);

				if (key != null)
					seenEntries[key] = entry.Value;
			}

			writer.WriteStartObject();
			foreach (var entry in seenEntries)
			{
				writer.WritePropertyName(entry.Key);
				serializer.Serialize(writer, entry.Value);
			}
			writer.WriteEndObject();
		}

		private static bool AsType<T>(object value, out T convertedValue) where T : class
		{
			convertedValue = value as T;
			return convertedValue != null;
		}
	}

	/// <summary>
	/// JSON converter for IDictionary&lt;TKey,TValue&gt; and IReadOnlyDictionary&lt;TKey,TValue&gt;
	/// that ignores the contract resolver (e.g. CamelCaseFieldNamesContractResolver)
	/// when converting dictionary keys to property names.
	/// </summary>
	internal class VerbatimDictionaryKeysJsonConverter<TKey, TValue> : JsonConverter
	{
		private readonly bool _keyIsString = typeof(TKey) == typeof(string);
		private readonly bool _keyIsField = typeof(TKey) == typeof(Field);
		private readonly bool _keyIsPropertyName = typeof(TKey) == typeof(PropertyName);
		private readonly bool _keyIsIndexName = typeof(TKey) == typeof(IndexName);
		private readonly bool _keyIsTypeName = typeof(TKey) == typeof(TypeName);

		public override bool CanConvert(Type t) =>
			typeof(IDictionary<TKey, TValue>).IsAssignableFrom(t) ||
			typeof(IReadOnlyDictionary<TKey, TValue>).IsAssignableFrom(t);

		public override bool CanRead => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var enumerable = (IEnumerable<KeyValuePair<TKey, TValue>>)value;
			if (enumerable == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = serializer.GetConnectionSettings();
			var seenEntries = new Dictionary<string, TValue>(enumerable.Count());

			foreach (var entry in enumerable)
			{
				if (SkipValue(serializer, entry))
					continue;
				string key;
				if (_keyIsString)
					key = entry.Key?.ToString();
				else if (settings == null)
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
				else if (_keyIsField)
				{
					var fieldName = entry.Key as Field;
					key = settings.Inferrer.Field(fieldName);
				}
				else if (_keyIsPropertyName)
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
				else if (_keyIsIndexName)
				{
					var indexName = entry.Key as IndexName;
					key = settings.Inferrer.IndexName(indexName);
				}
				else if (_keyIsTypeName)
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

		protected virtual bool SkipValue(JsonSerializer serializer, KeyValuePair<TKey, TValue> entry)
		{
			return entry.Value == null && serializer.NullValueHandling == NullValueHandling.Ignore;
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

	internal class VerbatimDictionaryKeysPreservingNullJsonConverter<TKey, TValue> : VerbatimDictionaryKeysJsonConverter<TKey, TValue>
	{
		protected override bool SkipValue(JsonSerializer serializer, KeyValuePair<TKey, TValue> entry) => false;
	}

}
