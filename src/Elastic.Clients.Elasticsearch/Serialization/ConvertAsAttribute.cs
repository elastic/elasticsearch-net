using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	[AttributeUsage(AttributeTargets.Interface)]
	internal class InterfaceConverterAttribute : Attribute
	{
		public InterfaceConverterAttribute(Type converterType) => ConverterType = converterType;

		public Type ConverterType { get; }
	}

	internal sealed class SimpleInterfaceConverter<TInterface, TConcrete> : JsonConverter<TInterface> where TConcrete : class, TInterface
	{
		public override TInterface Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			JsonSerializer.Deserialize<TConcrete>(ref reader, options);

		public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options)
			=> JsonSerializer.Serialize(writer, value, typeof(TConcrete), options);
	}

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

	[AttributeUsage(AttributeTargets.Interface)]
	internal class FieldNameQueryAttribute : Attribute
	{
		public FieldNameQueryAttribute(Type convertType) => ConvertType = convertType;

		public Type ConvertType { get; }
	}
}
