// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class ReadOnlyIndexNameDictionaryConverterFactory : JsonConverterFactory
{
	private readonly IElasticsearchClientSettings _settings;

	public ReadOnlyIndexNameDictionaryConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;
			 
	public override bool CanConvert(Type typeToConvert)
	{
		if (!typeToConvert.IsGenericType)
			return false;

		var canConvert = typeof(ReadOnlyIndexNameDictionary<>) == typeToConvert.GetGenericTypeDefinition();
		return canConvert;
	}

	public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
	{
		var valueType = type.GetGenericArguments()[0];
		return (JsonConverter)Activator.CreateInstance(typeof(ReadOnlyIndexNameDictionaryConverter<>).MakeGenericType(valueType), _settings);
	}
}
