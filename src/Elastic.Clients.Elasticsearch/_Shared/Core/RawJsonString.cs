// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

#if NET6_0_OR_GREATER
[JsonConverter(typeof(RawJsonConverter))]
public struct RawJsonString
{
public RawJsonString(string rawJson) => Json = rawJson;

public string Json { get; init; }
}

internal class RawJsonConverter : JsonConverter<RawJsonString>
{
public override RawJsonString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
public override void Write(Utf8JsonWriter writer, RawJsonString value, JsonSerializerOptions options) => writer.WriteRawValue(value.Json);
}
#endif
