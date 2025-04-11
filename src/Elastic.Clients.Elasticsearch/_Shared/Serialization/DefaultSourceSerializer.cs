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
	/// Constructs a new <see cref="DefaultSourceSerializer"/> instance that accepts an <see cref="Action{T}"/> that can
	/// be provided to customize the default <see cref="JsonSerializerOptions"/>.
	/// </summary>
	/// <param name="settings">An <see cref="IElasticsearchClientSettings"/> instance to which this serializer
	/// <see cref="JsonSerializerOptions"/> will be linked.
	/// </param>
	/// <param name="configureOptions">
	/// An optional <see cref="Action{T}"/> to customize the configuration of the default <see cref="JsonSerializerOptions"/>.
	/// </param>
	public DefaultSourceSerializer(IElasticsearchClientSettings settings, Action<JsonSerializerOptions>? configureOptions = null) :
		base(new DefaultSourceSerializerOptionsProvider(settings, configureOptions))
	{
	}
}

/// <summary>
/// The options-provider for the built-in <see cref="DefaultSourceSerializer"/>.
/// </summary>
public class DefaultSourceSerializerOptionsProvider :
	TransportSerializerOptionsProvider
{
	public DefaultSourceSerializerOptionsProvider(IElasticsearchClientSettings settings, Action<JsonSerializerOptions>? configureOptions = null) :
		base(CreateDefaultBuiltInConverters(settings), null, options => MutateOptions(options, configureOptions))
	{
	}

	public DefaultSourceSerializerOptionsProvider(IElasticsearchClientSettings settings, bool registerDefaultConverters, Action<JsonSerializerOptions>? configureOptions = null) :
		base(registerDefaultConverters ? CreateDefaultBuiltInConverters(settings) : [], null, options => MutateOptions(options, configureOptions))
	{
	}

	/// <summary>
	/// Returns an array of the built-in <see cref="JsonConverter"/>s that are used registered with the source serializer by default.
	/// </summary>
	private static IReadOnlyCollection<JsonConverter> CreateDefaultBuiltInConverters(IElasticsearchClientSettings settings) =>
	[
		// For context aware JsonConverter/JsonConverterFactory implementations.
		new ContextProvider<IElasticsearchClientSettings>(settings),

		new JsonStringEnumConverter(),
		new DoubleWithFractionalPortionConverter(),
		new SingleWithFractionalPortionConverter()
	];

	private static void MutateOptions(JsonSerializerOptions options, Action<JsonSerializerOptions>? configureOptions)
	{
		options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();

		options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

		// For numerically mapped fields, it is possible for values in the source to be returned as strings, if they were indexed as such.
		options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

		configureOptions?.Invoke(options);
	}
}
