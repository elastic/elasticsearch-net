using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nest
{
	public class DictionaryConverter : JsonConverterFactory
	{
		public override bool CanConvert(Type typeToConvert)
		{
			if (!typeToConvert.IsGenericType) return false;

			return typeToConvert.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
			       typeToConvert.GetGenericArguments()[0].GetInterfaces()
				       .Any(x => x.UnderlyingSystemType == typeof(IDictionaryKey));
		}

		public override JsonConverter CreateConverter(
			Type type,
			JsonSerializerOptions options)
		{
			var keyType = type.GetGenericArguments()[0];
			var valueType = type.GetGenericArguments()[1];

			var converter = (JsonConverter)Activator.CreateInstance(
				typeof(DictionaryConverterInner<,>).MakeGenericType(keyType, valueType),
				BindingFlags.Instance | BindingFlags.Public,
				null,
				new object[] {options},
				null);

			return converter;
		}

		private class DictionaryConverterInner<TKey, TValue> : JsonConverter<Dictionary<TKey, TValue>>
		{
			private readonly Type _keyType;
			private readonly JsonConverter<TValue>? _valueConverter;
			private readonly Type _valueType;

			public DictionaryConverterInner(JsonSerializerOptions options)
			{
				// For performance, use the existing converter if available.
				_valueConverter = (JsonConverter<TValue>)options
					.GetConverter(typeof(TValue));

				// Cache the key and value types.
				_keyType = typeof(TKey);
				_valueType = typeof(TValue);
			}

			public override Dictionary<TKey, TValue> Read(
				ref Utf8JsonReader reader,
				Type typeToConvert,
				JsonSerializerOptions options) =>
				//if (reader.TokenType != JsonTokenType.StartObject)
				//{
				//	throw new JsonException();
				//}
				//var dictionary = new Dictionary<TKey, TValue>();
				//while (reader.Read())
				//{
				//	if (reader.TokenType == JsonTokenType.EndObject)
				//	{
				//		return dictionary;
				//	}
				//	// Get the key.
				//	if (reader.TokenType != JsonTokenType.PropertyName)
				//	{
				//		throw new JsonException();
				//	}
				//	var propertyName = reader.GetString();
				//	// For performance, parse with ignoreCase:false first.
				//	if (!Enum.TryParse(propertyName, ignoreCase: false, out TKey key) &&
				//		!Enum.TryParse(propertyName, ignoreCase: true, out key))
				//	{
				//		throw new JsonException(
				//			$"Unable to convert \"{propertyName}\" to Enum \"{_keyType}\".");
				//	}
				//	// Get the value.
				//	TValue value;
				//	if (_valueConverter != null)
				//	{
				//		reader.Read();
				//		value = _valueConverter.Read(ref reader, _valueType, options);
				//	}
				//	else
				//	{
				//		value = JsonSerializer.Deserialize<TValue>(ref reader, options);
				//	}
				//	// Add to dictionary.
				//	dictionary.Add(key, value);
				//}
				throw new JsonException();

			public override void Write(
				Utf8JsonWriter writer,
				Dictionary<TKey, TValue> dictionary,
				JsonSerializerOptions options)
			{
				writer.WriteStartObject();

				foreach (var item in dictionary)
				{
					if (item.Key is null)
						throw new SerializationException("Null key");

					var propertyName = ((IDictionaryKey)item.Key).Key;

					writer.WritePropertyName
						(options.PropertyNamingPolicy?.ConvertName(propertyName) ?? propertyName);

					if (_valueConverter != null)
						_valueConverter.Write(writer, item.Value, options);
					else
						JsonSerializer.Serialize(writer, item.Value, options);
				}

				writer.WriteEndObject();
			}
		}
	}
}
