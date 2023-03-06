// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch.Serialization;

public class DefaultSourceSerializer : SystemTextJsonSerializer
{
	public static JsonConverter[] DefaultBuiltInConverters => new JsonConverter[]
	{
		new JsonStringEnumConverter(),
		new QueryConverter()
	};

	private readonly JsonSerializerOptions _jsonSerializerOptions;

	internal DefaultSourceSerializer(IElasticsearchClientSettings settings) : base(settings)
	{
		_jsonSerializerOptions = CreateDefaultJsonSerializerOptions();

		Initialize();
	}

	public DefaultSourceSerializer(IElasticsearchClientSettings settings, Action<JsonSerializerOptions> configureOptions) : base(settings)
	{
		if (configureOptions is null)
			throw new ArgumentNullException(nameof(configureOptions));

		var options = CreateDefaultJsonSerializerOptions();

		configureOptions(options);

		_jsonSerializerOptions = options;

		Initialize();
	}	

	public static JsonSerializerOptions CreateDefaultJsonSerializerOptions(bool includeDefaultConverters = true)
	{
		var options = new JsonSerializerOptions()
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals
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

	public static JsonSerializerOptions AddDefaultConverters(JsonSerializerOptions options)
	{
		foreach (var converter in DefaultBuiltInConverters)
		{
			options.Converters.Add(converter);
		}

		return options;	
	}

	protected override JsonSerializerOptions CreateJsonSerializerOptions() => _jsonSerializerOptions;
}
