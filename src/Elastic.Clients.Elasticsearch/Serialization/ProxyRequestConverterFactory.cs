using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	public class CustomJsonWriterConverterFactory : JsonConverterFactory
	{
		private readonly IElasticsearchClientSettings _settings;

		public CustomJsonWriterConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

		public override bool CanConvert(Type typeToConvert)
		{
			var isGeneric = typeToConvert.IsGenericType;
			var interfaces = typeToConvert.GetInterfaces();

			var canConvert = false;

			foreach (var item in interfaces)
			{
				var type = item.UnderlyingSystemType;
				if (type == typeof(ICustomJsonWriter))
					canConvert = true;
			}

			return canConvert && isGeneric;
		}

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var elementType = typeToConvert.GetGenericArguments()[0];

			var att = typeToConvert.GetCustomAttribute<ConvertAsAttribute>();

			var converter = (JsonConverter)Activator.CreateInstance(
				typeof(CustomJsonWriterConverter<>).MakeGenericType(att?.ConvertType.MakeGenericType(elementType) ?? elementType),
				BindingFlags.Instance | BindingFlags.Public,
				args: new object[] {_settings},
				binder: null,
				culture: null)!;

			return converter;
		}
	}
}
