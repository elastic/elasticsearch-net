// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// The built in internal serializer that the <see cref="ElasticsearchClient"/> uses to serialize
/// source document types.
/// </summary>
public class DefaultSourceSerializer : SystemTextJsonSerializer
{
	/// <summary>
	/// Returns an array of the built in <see cref="JsonConverter"/>s that are used registered with the
	/// source serializer by default.
	/// </summary>
	public static JsonConverter[] DefaultBuiltInConverters => new JsonConverter[]
	{
		new JsonStringEnumConverter(),
		new DoubleWithFractionalPortionConverter(),
		new SingleWithFractionalPortionConverter()
	};

	private readonly JsonSerializerOptions _jsonSerializerOptions;

	internal DefaultSourceSerializer(IElasticsearchClientSettings settings) : base(settings)
	{
		_jsonSerializerOptions = CreateDefaultJsonSerializerOptions();

		Initialize();
	}

	/// <summary>
	/// Constructs a new <see cref="DefaultSourceSerializer"/> instance that accepts an <see cref="Action{T}"/> that can
	/// be provided to customize the default <see cref="JsonSerializerOptions"/>.
	/// </summary>
	/// <param name="settings">An <see cref="IElasticsearchClientSettings"/> instance to which this
	/// serializers <see cref="JsonSerializerOptions"/> will be linked.</param>
	/// <param name="configureOptions">An <see cref="Action{T}"/> to customize the configuration of the
	/// default <see cref="JsonSerializerOptions"/>.</param>
	public DefaultSourceSerializer(IElasticsearchClientSettings settings, Action<JsonSerializerOptions> configureOptions) : base(settings)
	{
		var options = CreateDefaultJsonSerializerOptions();

		configureOptions?.Invoke(options);

		_jsonSerializerOptions = options;

		Initialize();
	}

	/// <summary>
	/// A factory method which returns a new instance of <see cref="JsonSerializerOptions"/> configured with the
	/// default configuration applied to for serialization by the <see cref="DefaultSourceSerializer"/>.
	/// </summary>
	/// <param name="includeDefaultConverters"></param>
	/// <returns></returns>
	public static JsonSerializerOptions CreateDefaultJsonSerializerOptions(bool includeDefaultConverters = true)
	{
		var options = new JsonSerializerOptions()
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			NumberHandling = JsonNumberHandling.AllowReadingFromString // For numerically mapped fields, it is possible for values in the source to be returned as strings, if they were indexed as such.
		};

		if (includeDefaultConverters)
		{
			foreach (var converter in DefaultBuiltInConverters)
			{
				options.Converters.Add(converter);
			}
		}

		return options;
	}

	/// <summary>
	/// A helper method which applies the default converters for the built in source serializer to an
	/// existing <see cref="JsonSerializerOptions"/> instance.
	/// </summary>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> instance to which to append the default
	/// built in <see cref="JsonConverter"/>s.</param>
	/// <returns>The <see cref="JsonSerializerOptions"/> instance that was provided as an argument.</returns>
	public static JsonSerializerOptions AddDefaultConverters(JsonSerializerOptions options)
	{
		foreach (var converter in DefaultBuiltInConverters)
		{
			options.Converters.Add(converter);
		}

		return options;	
	}

	protected sealed override JsonSerializerOptions CreateJsonSerializerOptions() => _jsonSerializerOptions;
}
