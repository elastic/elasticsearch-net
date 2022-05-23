// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class DictionaryConverter : JsonConverterFactory
{
	private readonly IElasticsearchClientSettings _settings;

	public DictionaryConverter(IElasticsearchClientSettings settings) => _settings = settings;

	public override bool CanConvert(Type typeToConvert)
	{
		if (!typeToConvert.IsGenericType)
			return false;

		return typeToConvert.GetGenericTypeDefinition() == typeof(Dictionary<,>);
	}

	public override JsonConverter CreateConverter(
		Type typeToConvert,
		JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();

		var keyType = args[0];
		var valueType = args[1];

		if (keyType.IsClass)
		{
			return (JsonConverter)Activator.CreateInstance(
				typeof(DictionaryConverterInner<,>).MakeGenericType(keyType, valueType), _settings);
		}

		return null;
	}

	private class DictionaryConverterInner<TKey, TValue> : JsonConverter<Dictionary<TKey, TValue>> where TKey : class
	{
		private readonly JsonConverter<TValue>? _valueConverter = null;
		private readonly Type _valueType;
		private readonly IElasticsearchClientSettings _settings;

		public DictionaryConverterInner(IElasticsearchClientSettings settings)
		{
			_valueType = typeof(TValue);
			_settings = settings;
		}

		public override Dictionary<TKey, TValue> Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException();
			var dictionary = new Dictionary<TKey, TValue>();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					return dictionary;

				// Get the key.
				if (reader.TokenType != JsonTokenType.PropertyName)
					throw new JsonException();

				var keyValue = reader.GetString();

				if (keyValue is null)
					throw new JsonException("Key was null.");

				// TODO: This is all very basic
				TKey key;

				if (typeof(TKey) == typeof(string))
				{
					key = (TKey)Activator.CreateInstance(typeof(string), keyValue.ToCharArray());
				}
				else if (typeof(TKey) == typeof(IndexName))
				{
					key = IndexName.Parse(keyValue) as TKey;
				}
				else if (typeof(TKey) == typeof(Field))
				{
					key = new Field(keyValue) as TKey;
				}
				else if (typeof(TKey) == typeof(TaskId))
				{
					key = new TaskId(keyValue) as TKey;
				}
				else if (typeof(TKey) == typeof(PropertyName))
				{
					key = new PropertyName(keyValue) as TKey;
				}
				else
				{
					throw new JsonException("Unsupported dictionary key");
					//key = (TKey)Activator.CreateInstance(typeof(TKey),
					//	BindingFlags.Instance,
					//	null,
					//	new object[] { propertyName },
					//	null);
				}

				// Get the value.
				TValue value;
				if (_valueConverter != null)
				{
					reader.Read();
					value = _valueConverter.Read(ref reader, _valueType, options);
				}
				else
					value = JsonSerializer.Deserialize<TValue>(ref reader, options);

				// Add to dictionary.
				dictionary.Add(key, value);
			}

			return dictionary;
		}

		public override void Write(
			Utf8JsonWriter writer,
			Dictionary<TKey, TValue> dictionary,
			JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			foreach (var item in dictionary)
			{
				if (item.Key is null)
					throw new JsonException("Null key");

				var propertyName = item.Key switch
				{
					string stringKey => stringKey,
					IDictionaryKey key => key.Key(_settings),
					_ => throw new JsonException("Must implement IDictionaryKey")
				};

				writer.WritePropertyName
					(options.PropertyNamingPolicy?.ConvertName(propertyName) ?? propertyName);

				if (item.Value is null)
				{
					writer.WriteNullValue();
				}
				else if (_valueConverter != null)
				{
					_valueConverter.Write(writer, item.Value, options);
				}
				else
				{
					JsonSerializer.Serialize(writer, item.Value, item.Value.GetType(), options);
				}
			}

			writer.WriteEndObject();
		}
	}
}
