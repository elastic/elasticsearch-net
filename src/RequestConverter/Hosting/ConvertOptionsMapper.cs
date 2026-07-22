// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RequestConverter.Hosting;

/// <summary>
/// Bridges the host's conversion contract to the converter: maps <see cref="ConvertOptions"/> onto
/// <see cref="FormattingOptions"/> and flattens the host's JSON parameter maps to the string maps the converter expects.
/// </summary>
public static class ConvertOptionsMapper
{
	public static FormattingOptions BuildFormattingOptions(ConvertOptions? options, string variableName)
	{
		// Emit a typed variable declaration (e.g. `SearchRequest request = new() { ... };`) so the request type and its
		// namespace surface in the generated snippet.
		var formatting = new FormattingOptions
		{
			EmitVariableDeclaration = true,
			VariableName = variableName
		};

		// Accept the style name case-insensitively; an unknown or absent value keeps the converter's default style.
		if (!string.IsNullOrEmpty(options?.TypeNameStyle)
			&& Enum.TryParse<TypeNameStyle>(options.TypeNameStyle, ignoreCase: true, out var style))
		{
			formatting = formatting with { TypeNameStyle = style };
		}

		// Accept the syntax mode case-insensitively (the host's snake_case `object_initializer`/`descriptor` parse after
		// the underscore is stripped); an unknown or absent value keeps the converter's object-initializer default.
		if (!string.IsNullOrEmpty(options?.SyntaxMode)
			&& Enum.TryParse<SyntaxMode>(options.SyntaxMode.Replace("_", ""), ignoreCase: true, out var syntaxMode))
		{
			formatting = formatting with { SyntaxMode = syntaxMode };
		}

		if (options?.UseStronglyTypedDocument is { } useStronglyTypedDocument)
		{
			formatting = formatting with { UseStronglyTypedDocument = useStronglyTypedDocument };
		}

		if (!string.IsNullOrEmpty(options?.DocumentTypeName))
		{
			formatting = formatting with { DocumentTypeName = options.DocumentTypeName };
		}

		return formatting;
	}

	/// <summary>
	/// Flattens the host's string-to-JSON parameter map to the string-to-string map the converter expects. A JSON
	/// string keeps its text value; any other JSON scalar (number, bool) keeps its raw token, which is the form the
	/// converter reuses when reconstructing path and query parameters.
	/// </summary>
	public static IReadOnlyDictionary<string, string>? ToStringMap(IReadOnlyDictionary<string, JsonElement>? source)
	{
		if (source is null || source.Count == 0)
		{
			return null;
		}

		var map = new Dictionary<string, string>(source.Count);
		foreach (var (key, value) in source)
		{
			map[key] = value.ValueKind == JsonValueKind.String ? value.GetString()! : value.GetRawText();
		}

		return map;
	}
}
