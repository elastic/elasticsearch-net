// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class InterfaceConverterFactory : JsonConverterFactory
{
	private readonly IElasticsearchClientSettings _settings;

	public InterfaceConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

	public override bool CanConvert(Type typeToConvert)
	{
		var customAttributes = typeToConvert.GetCustomAttributes();

		var canConvert = false;

		foreach (var item in customAttributes)
		{
			var type = item.GetType();
			if (type == typeof(InterfaceConverterAttribute))
				canConvert = true;
		}

		return canConvert;
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var att = typeToConvert.GetCustomAttribute<InterfaceConverterAttribute>();

		return (JsonConverter)Activator.CreateInstance(att.ConverterType)!;
	}
}
