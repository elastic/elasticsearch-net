// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class DictionaryResponseConverterFactory : JsonConverterFactory
{
	private readonly IElasticsearchClientSettings _settings;

	public DictionaryResponseConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.BaseType is not null &&
		typeToConvert.BaseType.IsGenericType &&
		typeToConvert.BaseType.GetGenericTypeDefinition() == typeof(DictionaryResponse<,>);

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var args = typeToConvert.BaseType.GetGenericArguments();

		var keyType = args[0];
		var valueType = args[1];

		if (keyType.IsClass)
		{
			if (keyType == typeof(IndexName))
			{
				return (JsonConverter)Activator.CreateInstance(
					typeof(ResolvableDictionaryResponseConverterInner<,,>).MakeGenericType(typeToConvert, keyType, valueType), _settings);
			}

			return (JsonConverter)Activator.CreateInstance(
				typeof(DictionaryResponseConverterInner<,,>).MakeGenericType(typeToConvert, keyType, valueType));
		}

		return null;
	}

	private class DictionaryResponseConverterInner<TType, TKey, TValue> : JsonConverter<TType>
		where TKey : class
		where TType : DictionaryResponse<TKey, TValue>, new()
	{
		public override TType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var dictionary = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(ref reader, options);

			if (dictionary is null)
				return null;

			return (TType)Activator.CreateInstance(typeof(TType), new object[] { dictionary });
		}

		public override void Write(Utf8JsonWriter writer, TType value, JsonSerializerOptions options) =>
			throw new NotImplementedException("Response converters do not support serialization.");
	}

	private class ResolvableDictionaryResponseConverterInner<TType, TKey, TValue> : JsonConverter<TType>
		where TKey : class, IUrlParameter
		where TType : DictionaryResponse<TKey, TValue>, new()
	{
		private readonly IElasticsearchClientSettings _settings;

		public ResolvableDictionaryResponseConverterInner(IElasticsearchClientSettings settings) => _settings = settings;

		public override TType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var dictionary = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(ref reader, options);

			if (dictionary is null)
				return null;

			var dictionaryProxy = new ResolvableDictionaryProxy<TKey, TValue>(_settings, dictionary);

			return (TType)Activator.CreateInstance(typeof(TType), new object[] { dictionaryProxy });
		}

		public override void Write(Utf8JsonWriter writer, TType value, JsonSerializerOptions options) =>
			throw new NotImplementedException("Response converters do not support serialization.");
	}
}
