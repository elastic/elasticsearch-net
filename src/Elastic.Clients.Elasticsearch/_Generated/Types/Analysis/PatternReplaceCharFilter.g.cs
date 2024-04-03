// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Analysis;

public sealed partial class PatternReplaceCharFilter : ICharFilter
{
	[JsonInclude, JsonPropertyName("flags")]
	public string? Flags { get; set; }
	[JsonInclude, JsonPropertyName("pattern")]
	public string Pattern { get; set; }
	[JsonInclude, JsonPropertyName("replacement")]
	public string? Replacement { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "pattern_replace";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class PatternReplaceCharFilterDescriptor : SerializableDescriptor<PatternReplaceCharFilterDescriptor>, IBuildableDescriptor<PatternReplaceCharFilter>
{
	internal PatternReplaceCharFilterDescriptor(Action<PatternReplaceCharFilterDescriptor> configure) => configure.Invoke(this);

	public PatternReplaceCharFilterDescriptor() : base()
	{
	}

	private string? FlagsValue { get; set; }
	private string PatternValue { get; set; }
	private string? ReplacementValue { get; set; }
	private string? VersionValue { get; set; }

	public PatternReplaceCharFilterDescriptor Flags(string? flags)
	{
		FlagsValue = flags;
		return Self;
	}

	public PatternReplaceCharFilterDescriptor Pattern(string pattern)
	{
		PatternValue = pattern;
		return Self;
	}

	public PatternReplaceCharFilterDescriptor Replacement(string? replacement)
	{
		ReplacementValue = replacement;
		return Self;
	}

	public PatternReplaceCharFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(FlagsValue))
		{
			writer.WritePropertyName("flags");
			writer.WriteStringValue(FlagsValue);
		}

		writer.WritePropertyName("pattern");
		writer.WriteStringValue(PatternValue);
		if (!string.IsNullOrEmpty(ReplacementValue))
		{
			writer.WritePropertyName("replacement");
			writer.WriteStringValue(ReplacementValue);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("pattern_replace");
		if (!string.IsNullOrEmpty(VersionValue))
		{
			writer.WritePropertyName("version");
			writer.WriteStringValue(VersionValue);
		}

		writer.WriteEndObject();
	}

	PatternReplaceCharFilter IBuildableDescriptor<PatternReplaceCharFilter>.Build() => new()
	{
		Flags = FlagsValue,
		Pattern = PatternValue,
		Replacement = ReplacementValue,
		Version = VersionValue
	};
}