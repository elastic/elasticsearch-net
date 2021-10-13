using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	[AttributeUsage(AttributeTargets.Interface)]
	internal class ConvertAsAttribute : Attribute
	{
		public ConvertAsAttribute(Type convertType) => ConvertType = convertType;

		public Type ConvertType { get; }
	}

	[AttributeUsage(AttributeTargets.Interface)]
	internal class InterfaceConverterAttribute : Attribute
	{
		public InterfaceConverterAttribute(Type converterType) => ConverterType = converterType;

		public Type ConverterType { get; }
	}

	// This would be generated per interface
	// Add new attribute to interface: [InterfaceConverterAttribute(typeof(SearchRequestInterfaceConverter<SearchRequest>))]
	public class SearchRequestInterfaceConverter<TReadAs> : JsonConverter<ISearchRequest> where TReadAs : class, ISearchRequest
	{
		public override ISearchRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			JsonSerializer.Deserialize<TReadAs>(ref reader, options);

		public override void Write(Utf8JsonWriter writer, ISearchRequest value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			if (value.MinScore.HasValue)
			{
				writer.WritePropertyName("min_score");
				writer.WriteNumberValue(value.MinScore.Value);
			}

			if (value.Profile.HasValue)
			{
				writer.WritePropertyName("profile");
				writer.WriteBooleanValue(value.Profile.Value);
			}

			writer.WriteEndObject();
		}
	}

	public class InterfaceConverterFactory : JsonConverterFactory
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
