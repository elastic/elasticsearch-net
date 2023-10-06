// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class ResolvableReadonlyDictionaryConverterFactory : JsonConverterFactory
{
	private readonly IElasticsearchClientSettings _settings;

	public ResolvableReadonlyDictionaryConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

	public override bool CanConvert(Type typeToConvert)
	{
		if (typeToConvert.BaseType is null || !typeToConvert.BaseType.IsGenericType || typeToConvert.BaseType.GetGenericTypeDefinition() != typeof(IsADictionary<,>))
		{
			return false;
		}

		var args = typeToConvert.BaseType.GetGenericArguments();

		var keyType = args[0];

		return keyType is not null && typeof(IUrlParameter).IsAssignableFrom(keyType);
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var args = typeToConvert.BaseType.GetGenericArguments();

		var keyType = args[0];
		var valueType = args[1];

		if (keyType.IsClass)
		{
			return (JsonConverter)Activator.CreateInstance(
				typeof(ResolvableReadOnlyDictionaryConverterInner<,,>).MakeGenericType(typeToConvert, keyType, valueType), _settings);
		}

		return null;
	}

	private class ResolvableReadOnlyDictionaryConverterInner<TType, TKey, TValue> : JsonConverter<IReadOnlyDictionary<TKey, TValue>>
		where TKey : class, IUrlParameter
		where TType : IReadOnlyDictionary<TKey, TValue>
	{
		private readonly IElasticsearchClientSettings _settings;

		public ResolvableReadOnlyDictionaryConverterInner(IElasticsearchClientSettings settings) => _settings = settings;

		public override IReadOnlyDictionary<TKey, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var dictionary = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(ref reader, options);

			if (dictionary is null)
				return default;

			var dictionaryProxy = new ResolvableDictionaryProxy<TKey, TValue>(_settings, dictionary);

			return dictionaryProxy;
		}

		public override void Write(Utf8JsonWriter writer, IReadOnlyDictionary<TKey, TValue> value, JsonSerializerOptions options) =>
			throw new NotImplementedException($"Serialization is not supported for '{typeof(TType)}'.");
	}
}
