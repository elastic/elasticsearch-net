// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Used for derived converters which need access to <see cref="IElasticsearchClientSettings"/> in order to serialize.
/// </summary>
/// <typeparam name="T">The type of object or value handled by the converter.</typeparam>
internal abstract class SettingsJsonConverter<T> : JsonConverter<T>
{
	private IElasticsearchClientSettings _settings;

	/// <summary>
	/// Access the <see cref="IElasticsearchClientSettings"/> for a given <see cref="JsonSerializerOptions"/> instance.
	/// </summary>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> for which the <see cref="IElasticsearchClientSettings"/> should be retrieved.</param>
	/// <returns>An <see cref="IElasticsearchClientSettings"/> instance related to the provided <see cref="JsonSerializerOptions"/>.</returns>
	/// <exception cref="JsonException">Thrown if a corresponding <see cref="IElasticsearchClientSettings"/> instance cannot be found for the <see cref="JsonSerializerOptions"/>.</exception>
	protected IElasticsearchClientSettings GetSettings(JsonSerializerOptions options)
	{
		if (_settings is not null)
			return _settings;

		// We avoid locking here as the result of accessing the settings should be idemopotent and low cost.

		if (options.TryGetClientSettings(out var settings))
		{
			_settings = settings;
			return _settings;
		}

		throw new JsonException("Unable to retrieve ElasticsearchClientSettings settings from JsonSerializerOptions.");
	}
}
