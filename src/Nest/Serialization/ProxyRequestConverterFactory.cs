using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nest
{
	public class ProxyRequestConverterFactory : JsonConverterFactory
	{
		private readonly IElasticsearchClientSettings _settings;

		public ProxyRequestConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

		public override bool CanConvert(Type typeToConvert)
		{
			var isGeneric = typeToConvert.IsGenericType;
			var interfaces = typeToConvert.GetInterfaces();

			var canConvert = false;

			foreach (var item in interfaces)
			{
				var type = item.UnderlyingSystemType;
				if (type == typeof(IProxyRequest))
					canConvert = true;
			}

			return canConvert && isGeneric;
		}

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var elementType = typeToConvert.GetGenericArguments()[0];

			var att = typeToConvert.GetCustomAttribute<ConvertAsAttribute>();

			var converter = (JsonConverter)Activator.CreateInstance(
				typeof(ProxyRequestConverter<>).MakeGenericType(att?.ConvertType.MakeGenericType(elementType) ??
				                                                elementType),
				BindingFlags.Instance | BindingFlags.Public,
				args: new object[] {_settings},
				binder: null,
				culture: null)!;

			return converter;
		}
	}
}
