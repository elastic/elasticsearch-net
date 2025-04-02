// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Serialization;

public sealed class SourceMarker<T>
{
	static SourceMarker()
	{
		DynamicallyAccessed.PublicConstructors(typeof(SourceMarkerConverter<T>));
	}
}

internal sealed class SourceMarkerConverter<T> :
	JsonConverter<SourceMarker<T>>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public SourceMarkerConverter(IElasticsearchClientSettings settings)
	{
		WrappedConverter = new SourceConverter<T>(settings);
	}

	public override SourceMarker<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, SourceMarker<T> value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class SourceMarkerConverterFactory(IElasticsearchClientSettings settings) :
	JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
	{
		return typeToConvert.IsGenericType &&
			   typeToConvert.GetGenericTypeDefinition() == typeof(SourceMarker<>);
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();

#pragma warning disable IL3050 // SourceMarker<T> static constructor roots SourceMarkerConverter<T>.

		var converter = (JsonConverter)Activator.CreateInstance(
			typeof(SourceMarkerConverter<>).MakeGenericType(args[0]),
			BindingFlags.Instance | BindingFlags.Public,
			binder: null,
			args: [settings],
			culture: null)!;

#pragma warning restore IL3050

		return converter;
	}
}

internal sealed class SourceConverter<T> :
	JsonConverter<T>
{
	private readonly IElasticsearchClientSettings _settings;

	public SourceConverter(IElasticsearchClientSettings settings)
	{
		_settings = settings;
	}

	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return _settings.SourceSerializer.Deserialize<T>(ref reader);
	}

	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		_settings.SourceSerializer.Serialize(value, writer);
	}
}
