// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{

	internal interface ISelfDeserializable
	{
		void Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings);
	}

	internal interface ISelfTwoWaySerializable
	{
		void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);
		void Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings);
	}


	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum)]
	public class StringEnumAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Property)]
	public class IgnoreAttribute : Attribute { }

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
