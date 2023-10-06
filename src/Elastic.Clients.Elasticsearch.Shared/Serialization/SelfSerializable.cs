// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Marks a type to provide it's own serialization code.
/// <para><b>IMPORTANT:</b> This should only be used for types that are only ever serialized and never deserialised, such as descriptors.</para>
/// </summary>
internal interface ISelfSerializable
{
	void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);
}

internal interface ISelfDeserializable
{
	void Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings);
}

internal interface ISelfTwoWaySerializable
{
	void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);
	void Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings);
}
