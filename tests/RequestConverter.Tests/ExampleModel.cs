using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RequestConverter.Tests;

/// <summary>One recorded documentation example from <c>alternatives_report.json</c>.</summary>
internal sealed record ExampleModel
{
	[JsonPropertyName("digest")]
	public string Digest { get; init; } = "";

	[JsonPropertyName("lang")]
	public string Lang { get; init; } = "";

	[JsonPropertyName("parsed_source")]
	public IReadOnlyList<ExampleSourceModel>? ParsedSource { get; init; }
}

/// <summary>A single request within a recorded example (an example may contain several).</summary>
internal sealed record ExampleSourceModel
{
	[JsonPropertyName("api")]
	public string Api { get; init; } = "";

	[JsonPropertyName("params")]
	public IReadOnlyDictionary<string, string>? PathParameters { get; init; }

	[JsonPropertyName("query")]
	public IReadOnlyDictionary<string, string>? QueryParameters { get; init; }

	[JsonPropertyName("body")]
	public JsonElement? Body { get; init; }
}
