using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class CustomJsonWriterConverterFactory : JsonConverterFactory
{
	private readonly IElasticsearchClientSettings _settings;

	public CustomJsonWriterConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

	public override bool CanConvert(Type typeToConvert)
	{
		if (!typeToConvert.IsGenericType)
			return false;

		var interfaces = typeToConvert.GetInterfaces();

		var canConvert = false;

		foreach (var item in interfaces)
		{
			var type = item.UnderlyingSystemType;
			if (type == typeof(ICustomJsonWriter))
				canConvert = true;
		}

		return canConvert;
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var converter = (JsonConverter)Activator.CreateInstance(
			typeof(CustomJsonWriterConverter<>).MakeGenericType(typeToConvert),
			BindingFlags.Instance | BindingFlags.Public,
			args: new object[] { _settings },
			binder: null,
			culture: null)!;

		return converter;
	}
}
