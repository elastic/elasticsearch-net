// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// The built-in internal serializer that the <see cref="ElasticsearchClient"/> uses to serialize
/// source document types.
/// </summary>
public class DefaultSourceSerializer :
	SystemTextJsonSerializer
{
	/// <summary>
	/// Constructs a new <see cref="DefaultSourceSerializer"/> instance.
	/// </summary>
	/// <param name="settings">The <see cref="IElasticsearchClientSettings"/> instance to which this serializer will be linked.</param>
	/// <param name="configureOptions">An optional <see cref="Action{T}"/> to customize the default <see cref="JsonSerializerOptions"/>.</param>
	public DefaultSourceSerializer(IElasticsearchClientSettings settings, Action<JsonSerializerOptions>? configureOptions = null) :
		base(new DefaultSourceSerializerOptionsProvider(settings, configureOptions))
	{
	}

	/// <summary>
	/// Constructs a new <see cref="DefaultSourceSerializer"/> instance.
	/// </summary>
	/// <param name="settings">The <see cref="IElasticsearchClientSettings"/> instance to which this serializer will be linked.</param>
	/// <param name="typeInfoResolver">A custom <see cref="IJsonTypeInfoResolver"/> to use.</param>
	/// <param name="configureOptions">An optional <see cref="Action{T}"/> to customize the default <see cref="JsonSerializerOptions"/>.</param>
	public DefaultSourceSerializer(IElasticsearchClientSettings settings, IJsonTypeInfoResolver typeInfoResolver, Action<JsonSerializerOptions>? configureOptions = null) :
		base(new DefaultSourceSerializerOptionsProvider(settings, typeInfoResolver, configureOptions))
	{
	}
}

/// <summary>
/// The default <see cref="IJsonSerializerOptionsProvider"/> implementation for the built-in <see cref="DefaultSourceSerializer"/>.
/// </summary>
internal sealed class DefaultSourceSerializerOptionsProvider :
	TransportSerializerOptionsProvider
{
	/// <summary>
	/// Constructs a new <see cref="DefaultSourceSerializerOptionsProvider"/> instance.
	/// </summary>
	/// <param name="settings">The <see cref="IElasticsearchClientSettings"/> instance to which this serializer options will be linked.</param>
	/// <param name="configureOptions">An optional <see cref="Action{T}"/> to customize the default <see cref="JsonSerializerOptions"/>.</param>
	public DefaultSourceSerializerOptionsProvider(IElasticsearchClientSettings settings, Action<JsonSerializerOptions>? configureOptions = null) :
		base(
			CreateDefaultBuiltInConverters(settings),
			null,
			options => MutateOptions(options, null, configureOptions)
		)
	{
	}

	/// <summary>
	/// Constructs a new <see cref="DefaultSourceSerializerOptionsProvider"/> instance.
	/// </summary>
	/// <param name="settings">The <see cref="IElasticsearchClientSettings"/> instance to which this serializer options will be linked.</param>
	/// <param name="typeInfoResolver">A custom <see cref="IJsonTypeInfoResolver"/> to use.</param>
	/// <param name="configureOptions">An optional <see cref="Action{T}"/> to customize the default <see cref="JsonSerializerOptions"/>.</param>
	public DefaultSourceSerializerOptionsProvider(IElasticsearchClientSettings settings, IJsonTypeInfoResolver typeInfoResolver, Action<JsonSerializerOptions>? configureOptions = null) :
		base(
			CreateDefaultBuiltInConverters(settings),
			null,
			options => MutateOptions(options, typeInfoResolver ?? throw new ArgumentNullException(nameof(typeInfoResolver)), configureOptions)
		)
	{
	}

	/// <summary>
	/// Returns an array of the built-in <see cref="JsonConverter"/>s that are registered with the source serializer by default.
	/// </summary>
	private static IReadOnlyCollection<JsonConverter> CreateDefaultBuiltInConverters(IElasticsearchClientSettings settings) =>
	[
		// For context aware JsonConverter/JsonConverterFactory implementations.
		new ContextProvider<IElasticsearchClientSettings>(settings),

#pragma warning disable IL3050
		new JsonStringEnumConverter(),
#pragma warning restore IL3050
		new DoubleWithFractionalPortionConverter(),
		new SingleWithFractionalPortionConverter()
	];

	private static void MutateOptions(JsonSerializerOptions options, IJsonTypeInfoResolver? typeInfoResolver, Action<JsonSerializerOptions>? configureOptions)
	{
#pragma warning disable IL2026, IL3050
		options.TypeInfoResolver = typeInfoResolver ?? new DefaultJsonTypeInfoResolver();
#pragma warning restore IL2026, IL3050

		options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

		// For numerically mapped fields, it is possible for values in the source to be returned as strings, if they were indexed as such.
		options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

		configureOptions?.Invoke(options);
	}
}
