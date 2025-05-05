// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// The built-in internal serializer that the <see cref="ElasticsearchClient"/> uses to serialize
/// source document types.
/// </summary>
public class DefaultSourceSerializer :
	SystemTextJsonSerializer
{
#if !NET8_0_OR_GREATER
	private readonly object _lock = new();
#endif

	/// <summary>
	/// Constructs a new <see cref="DefaultSourceSerializer"/> instance that accepts an <see cref="Action{T}"/> that can
	/// be provided to customize the default <see cref="JsonSerializerOptions"/>.
	/// </summary>
	/// <param name="settings">An <see cref="IElasticsearchClientSettings"/> instance to which this serializers
	/// <see cref="JsonSerializerOptions"/> will be linked.
	/// </param>
	/// <param name="configureOptions">
	/// An optional <see cref="Action{T}"/> to customize the configuration of the default <see cref="JsonSerializerOptions"/>.
	/// </param>
	public DefaultSourceSerializer(IElasticsearchClientSettings settings, Action<JsonSerializerOptions>? configureOptions = null) :
		base(new DefaultSourceSerializerOptionsProvider(configureOptions)) =>
		LinkSettings(settings);

	/// <summary>
	/// Links the <see cref="JsonSerializerOptions"/> of this serializer to the given <see cref="IElasticsearchClientSettings"/>.
	/// </summary>
	private void LinkSettings(IElasticsearchClientSettings settings)
	{
		var options = GetJsonSerializerOptions(SerializationFormatting.None);
		var indentedOptions = GetJsonSerializerOptions(SerializationFormatting.Indented);

#if NET8_0_OR_GREATER
		ElasticsearchClient.SettingsTable.TryAdd(options, settings);
		ElasticsearchClient.SettingsTable.TryAdd(indentedOptions, settings);
#else
		lock (_lock)
		{
			if (!ElasticsearchClient.SettingsTable.TryGetValue(options, out _))
			{
				ElasticsearchClient.SettingsTable.Add(options, settings);
			}

			if (!ElasticsearchClient.SettingsTable.TryGetValue(indentedOptions, out _))
			{
				ElasticsearchClient.SettingsTable.Add(indentedOptions, settings);
			}
		}
#endif
	}
}

/// <summary>
/// The options-provider for the built-in <see cref="DefaultSourceSerializer"/>.
/// </summary>
public class DefaultSourceSerializerOptionsProvider :
	TransportSerializerOptionsProvider
{
	/// <summary>
	/// Returns an array of the built-in <see cref="JsonConverter"/>s that are used registered with the source serializer by default.
	/// </summary>
	private static JsonConverter[] DefaultBuiltInConverters =>
	[
		new JsonStringEnumConverter(),
		new DoubleWithFractionalPortionConverter(),
		new SingleWithFractionalPortionConverter()
	];

	public DefaultSourceSerializerOptionsProvider(Action<JsonSerializerOptions>? configureOptions = null) :
		base(DefaultBuiltInConverters, null, options => MutateOptions(options, configureOptions))
	{
	}

	public DefaultSourceSerializerOptionsProvider(bool registerDefaultConverters = true, Action<JsonSerializerOptions>? configureOptions = null) :
		base(registerDefaultConverters ? DefaultBuiltInConverters : [], null, options => MutateOptions(options, configureOptions))
	{
	}

	private static void MutateOptions(JsonSerializerOptions options, Action<JsonSerializerOptions>? configureOptions)
	{
		options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

		// For numerically mapped fields, it is possible for values in the source to be returned as strings, if they were indexed as such.
		options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

		configureOptions?.Invoke(options);
	}
}
