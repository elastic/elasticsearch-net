// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// <para>Lazily deserializable JSON.</para>
/// <para>Holds raw JSON bytes which can be lazily deserialized to a specific <see cref="Type"/> using the source serializer at a later time.</para>
/// </summary>
[JsonConverter(typeof(Json.LazyJsonConverter))]
public readonly struct LazyJson
{
	internal LazyJson(JsonElement json, IElasticsearchClientSettings? settings)
	{
		Json = json;
		Settings = settings;
	}

	internal JsonElement Json { get; }
	internal IElasticsearchClientSettings? Settings { get; }

	/// <summary>
	/// Creates an instance of <typeparamref name="T" /> from this
	/// <see cref="LazyJson" /> instance.
	/// </summary>
	/// <typeparam name="T">The type</typeparam>
	public T? As<T>()
	{
		if (Settings is null)
		{
			throw new InvalidOperationException($"Can not deserialize value without '{nameof(Settings)}'.");
		}

		return Settings.SourceSerializer.Deserialize<T>(Json);
	}
}
