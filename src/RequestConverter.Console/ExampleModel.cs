using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RequestConverter.Console;

internal sealed record ExampleModel
{
	[JsonPropertyName("digest")]
	public required string Digest { get; init; }

	[JsonPropertyName("lang")]
	public required string Lang { get; init; }

	[JsonPropertyName("parsed_source")]
	public IReadOnlyList<ExampleSourceModel>? ParsedSource { get; init; }
}

internal sealed record ExampleSourceModel
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
