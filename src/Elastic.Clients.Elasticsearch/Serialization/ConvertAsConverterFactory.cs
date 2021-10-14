using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch
{
	public class FieldNameQueryConverterFactory : JsonConverterFactory
	{
		private readonly IElasticsearchClientSettings _settings;

		public FieldNameQueryConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

		public override bool CanConvert(Type typeToConvert)
		{
			var customAttributes = typeToConvert.GetCustomAttributes();

			var canConvert = false;

			foreach (var item in customAttributes)
			{
				var type = item.GetType();
				if (type == typeof(FieldNameQueryAttribute))
					canConvert = true;
			}

			return canConvert;
		}	

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var att = typeToConvert.GetCustomAttribute<FieldNameQueryAttribute>();

			return (JsonConverter)Activator.CreateInstance(
					typeof(FieldNameQueryConverter<>).MakeGenericType(att.ConvertType),
					BindingFlags.Instance | BindingFlags.Public,
					args: new object[] { _settings },
					binder: null,
					culture: null);
		}

		public class FieldNameQueryConverter<T> : JsonConverter<IFieldNameQuery> where T : class
		{
			private readonly IElasticsearchClientSettings _settings;

			public FieldNameQueryConverter(IElasticsearchClientSettings settings) => _settings = settings;

			public override IFieldNameQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

			public override void Write(Utf8JsonWriter writer, IFieldNameQuery value, JsonSerializerOptions options)
			{
				if (value is null)
				{
					writer.WriteNullValue();
					return;
				}

				// TODO
				//var fieldName = _settings.Inferrer.

				writer.WriteStartObject();
				writer.WritePropertyName(value.Field.ToString());

				// TODO - WriteRaw on .NET 6
				// #if NET6_0_OR_GREATER
				// writer.WriteRawValue(_settings.SourceSerializer.Serialize(value, options));

				// Less efficient but works on older versions of System.Text.Json
				using var ms = new MemoryStream();
				_settings.SourceSerializer.Serialize((T)value, ms);
				ms.Position = 0;
				using var document = JsonDocument.Parse(ms);
				document.RootElement.WriteTo(writer);

				writer.WriteEndObject();
			}
		}
	}

	public class ConvertAsConverterFactory : JsonConverterFactory
	{
		private readonly object[] _settings;

		public ConvertAsConverterFactory(IElasticsearchClientSettings settings) => _settings = new[] { settings };

		public override bool CanConvert(Type typeToConvert)
		{
			var customAttributes = typeToConvert.GetCustomAttributes();

			var canConvert = false;

			foreach (var item in customAttributes)
			{
				var type = item.GetType();
				if (type == typeof(ConvertAsAttribute))
					canConvert = true;
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
					args: _settings,
					binder: null,
					culture: null)!;
			}
			else
			{
				return (JsonConverter)Activator.CreateInstance(
					typeof(InterfaceConverter<,>).MakeGenericType(typeToConvert, att?.ConvertType))!;
			}
		}
	}
}
