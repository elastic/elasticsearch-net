// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class SelfSerializableConverterFactory : JsonConverterFactory
{
	private readonly SelfSerializableJsonConverter _converter;

	public SelfSerializableConverterFactory(IElasticsearchClientSettings settings) => _converter = new SelfSerializableJsonConverter(settings);

	public override bool CanConvert(Type typeToConvert)
	{
		var canSerialize = typeof(ISelfSerializable).IsAssignableFrom(typeToConvert);
		return canSerialize;
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
		_converter;

	private class SelfSerializableJsonConverter : JsonConverter<ISelfSerializable>
	{
		private readonly IElasticsearchClientSettings _settings;

		public SelfSerializableJsonConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override ISelfSerializable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => default;

		public override void Write(Utf8JsonWriter writer, ISelfSerializable value, JsonSerializerOptions options) => value.Serialize(writer, options, _settings);
	}
}

internal sealed class SelfTwoWaySerializableConverterFactory : JsonConverterFactory
{
	private readonly SelfSerializableJsonConverter _converter;

	public SelfTwoWaySerializableConverterFactory(IElasticsearchClientSettings settings) => _converter = new SelfSerializableJsonConverter(settings);

	public override bool CanConvert(Type typeToConvert)
	{
		var canSerialize = typeof(ISelfTwoWaySerializable).IsAssignableFrom(typeToConvert);
		return canSerialize;
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
		_converter;

	private class SelfSerializableJsonConverter : JsonConverter<ISelfTwoWaySerializable>
	{
		private readonly IElasticsearchClientSettings _settings;

		public SelfSerializableJsonConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override ISelfTwoWaySerializable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var instance = (ISelfTwoWaySerializable)Activator.CreateInstance(typeToConvert, true);
			instance.Deserialize(ref reader, options, _settings);
			return instance;
		}

		public override void Write(Utf8JsonWriter writer, ISelfTwoWaySerializable value, JsonSerializerOptions options) => value.Serialize(writer, options, _settings);
	}
}

internal sealed class SelfDeserializableConverterFactory : JsonConverterFactory
{
	private readonly IElasticsearchClientSettings _settings;

	public SelfDeserializableConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

	public override bool CanConvert(Type typeToConvert)
	{
		var canSerialize = typeof(ISelfDeserializable).IsAssignableFrom(typeToConvert);
		return canSerialize;
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
		(JsonConverter)Activator.CreateInstance(typeof(SelfDeserializableJsonConverter), _settings);

	private class SelfDeserializableJsonConverter : JsonConverter<ISelfDeserializable>
	{
		private readonly IElasticsearchClientSettings _settings;

		public SelfDeserializableJsonConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override ISelfDeserializable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var instance = (ISelfDeserializable)Activator.CreateInstance(typeToConvert);
			instance.Deserialize(ref reader, options, _settings);
			return instance;
		}

		public override void Write(Utf8JsonWriter writer, ISelfDeserializable value, JsonSerializerOptions options) => throw new NotImplementedException();
	}
}
