// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

/// <summary>
/// Allows a query represented as a string of JSON to be defined. This can be useful when support for a built-in query is not yet available.
/// </summary>
[JsonConverter(typeof(RawJsonQueryConverter))]
public sealed class RawJsonQuery
{
	public RawJsonQuery(string rawQuery) => Raw = rawQuery;

	/// <summary>
	/// The raw JSON representing the query to be executed.
	/// </summary>
	public string Raw { get; }

	public static implicit operator Query(RawJsonQuery rawJsonQuery) => Query.RawJson(rawJsonQuery);
}

internal sealed class RawJsonQueryConverter : JsonConverter<RawJsonQuery>
{
	public override RawJsonQuery? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("We never expect to deserialize a raw query.");

	public override void Write(Utf8JsonWriter writer, RawJsonQuery value, JsonSerializerOptions options) => writer.WriteRawValue(value.Raw);
}
