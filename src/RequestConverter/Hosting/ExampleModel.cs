// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RequestConverter.Hosting;

/// <summary>One recorded documentation example from <c>alternatives_report.json</c>.</summary>
public sealed record ExampleModel
{
	[JsonPropertyName("digest")]
	public required string Digest { get; init; }

	[JsonPropertyName("lang")]
	public required string Lang { get; init; }

	[JsonPropertyName("parsed_source")]
	public IReadOnlyList<ExampleSourceModel>? ParsedSource { get; init; }
}

/// <summary>A single request within a recorded example (an example may contain several).</summary>
public sealed record ExampleSourceModel
{
	[JsonPropertyName("api")]
	public required string Api { get; init; }

	[JsonPropertyName("params")]
	public required IReadOnlyDictionary<string, string>? PathParameters { get; init; }

	[JsonPropertyName("query")]
	public required IReadOnlyDictionary<string, string>? QueryParameters { get; init; }

	[JsonPropertyName("body")]
	public required JsonElement? Body { get; init; }
}
