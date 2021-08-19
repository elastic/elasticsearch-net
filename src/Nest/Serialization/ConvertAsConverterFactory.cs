using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nest
{
	public class ConvertAsConverterFactory : JsonConverterFactory
	{
		private readonly IElasticsearchClientSettings _settings;

		public ConvertAsConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

		public override bool CanConvert(Type typeToConvert)
		{
			var customAttributes = typeToConvert.GetCustomAttributes();

			var canConvert = false;

			foreach (var item in customAttributes)
			{
				var type = item.GetType();
				if (type == typeof(ConvertAsAttribute)) canConvert = true;
			}

			return canConvert;
		}

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var att = typeToConvert.GetCustomAttribute<ConvertAsAttribute>();

			var genericArgs = typeToConvert.GetGenericArguments();

			if (genericArgs.Any() && genericArgs[0].GetInterfaces()
				.Any(x => x.UnderlyingSystemType == typeof(ICustomJsonWriter)))
			{
				var elementType = typeToConvert.GetGenericArguments()[0];

				return (JsonConverter)Activator.CreateInstance(
					typeof(CustomJsonWriterConverter<>).MakeGenericType(att?.ConvertType.MakeGenericType(elementType) ??
					                                                elementType),
					BindingFlags.Instance | BindingFlags.Public,
					args: new object[] {_settings},
					binder: null,
					culture: null)!;
			}
			else
			{
				return (JsonConverter)Activator.CreateInstance(
					typeof(InterfaceConverter<,>).MakeGenericType(typeToConvert, att?.ConvertType),
					BindingFlags.Instance | BindingFlags.Public,
					args: null,
					binder: null,
					culture: null)!;
			}
		}
	}
}
