// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable enable
namespace Elastic.Clients.Elasticsearch.Aggregations;

/// <summary>
/// Filters which terms to exclude from the response.
/// </summary>
[JsonConverter(typeof(TermsExcludeConverter))]
public sealed class TermsExclude
{
	/// <summary>
	/// Creates an instance of <see cref="TermsExclude" /> that uses a regular expression pattern
	/// to determine the terms to exclude from the response.
	/// </summary>
	/// <param name="pattern">The regular expression pattern.</param>
	public TermsExclude(string regexPattern)
	{
		if (regexPattern is null)
			throw new ArgumentNullException(nameof(regexPattern));

		RegexPattern = regexPattern;
	}

	/// <summary>
	/// Creates an instance of <see cref="TermsExclude" /> that uses a collection of terms
	/// to exclude from the response.
	/// </summary>
	/// <param name="values">The exact terms to exclude.</param>
	public TermsExclude(IEnumerable<string> values)
	{
		if (values is null)
			throw new ArgumentNullException(nameof(values));

		Values = values;
	}

	/// <summary>
	/// The regular expression pattern to determine terms to exclude from the response.
	/// </summary>
	public string? RegexPattern { get; }

	/// <summary>
	/// Collection of terms to exclude from the response.
	/// </summary>
	public IEnumerable<string>? Values { get; }
}

internal sealed class TermsExcludeConverter : JsonConverter<TermsExclude>
{
	public override TermsExclude? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Null)
		{
			reader.Read();
			return null;
		}

		TermsExclude termsExclude;

		switch (reader.TokenType)
		{
			case JsonTokenType.StartArray:
				var terms = JsonSerializer.Deserialize<string[]>(ref reader, options) ?? Array.Empty<string>();
				termsExclude = new TermsExclude(terms);
				break;
			case JsonTokenType.String:
				var regex = reader.GetString();
				termsExclude = new TermsExclude(regex!);
				break;
			default:
				throw new JsonException($"Unexpected token {reader.TokenType} when deserializing {nameof(TermsExclude)}");
		}

		return termsExclude;
	}

	public override void Write(Utf8JsonWriter writer, TermsExclude value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		if (value.Values is not null)
		{
			JsonSerializer.Serialize(writer, value.Values, options);
			return;
		}

		writer.WriteStringValue(value.RegexPattern);
	}
}
