// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;

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
		base(new DefaultSourceSerializerOptionsProvider(settings, [typeInfoResolver], configureOptions))
	{
	}

	public DefaultSourceSerializer(IElasticsearchClientSettings settings, IJsonTypeInfoResolver[] typeInfoResolvers, Action<JsonSerializerOptions>? configureOptions = null) :
		base(new DefaultSourceSerializerOptionsProvider(settings, typeInfoResolvers, configureOptions))
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
	public DefaultSourceSerializerOptionsProvider(IElasticsearchClientSettings settings, IJsonTypeInfoResolver[] typeInfoResolvers, Action<JsonSerializerOptions>? configureOptions = null) :
		base(
			CreateDefaultBuiltInConverters(settings),
			null,
			options => MutateOptions(options, typeInfoResolvers, configureOptions)
		)
	{
	}

	/// <summary>
	/// Returns an array of the built-in <see cref="JsonConverter"/>s that are registered with the source serializer by default.
	/// </summary>
	[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Always using explicit TypeInfoResolver")]
	[UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute'", Justification = "Always using explicit TypeInfoResolver")]
	private static IReadOnlyCollection<JsonConverter> CreateDefaultBuiltInConverters(IElasticsearchClientSettings settings) =>
	[
		// For context aware JsonConverter/JsonConverterFactory implementations.
		new ContextProvider<IElasticsearchClientSettings>(settings),

		new JsonStringEnumConverter(),
		new DoubleWithFractionalPortionConverter(),
		new SingleWithFractionalPortionConverter()
	];

	[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Always using explicit TypeInfoResolver")]
	[UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute'", Justification = "Always using explicit TypeInfoResolver")]
	private static void MutateOptions(JsonSerializerOptions options, IJsonTypeInfoResolver[]? typeInfoResolvers, Action<JsonSerializerOptions>? configureOptions)
	{
		var resolvers = typeInfoResolvers ?? [];

		options.TypeInfoResolver = JsonTypeInfoResolver.Combine(
			[new DefaultJsonTypeInfoResolver(), .. resolvers]
		);
		options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

		// For numerically mapped fields, it is possible for values in the source to be returned as strings, if they were indexed as such.
		options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

		configureOptions?.Invoke(options);
	}
}
