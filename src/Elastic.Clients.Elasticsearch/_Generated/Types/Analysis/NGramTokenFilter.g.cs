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

public sealed partial class NGramTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("max_gram")]
	public int? MaxGram { get; set; }
	[JsonInclude, JsonPropertyName("min_gram")]
	public int? MinGram { get; set; }
	[JsonInclude, JsonPropertyName("preserve_original")]
	public bool? PreserveOriginal { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "ngram";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class NGramTokenFilterDescriptor : SerializableDescriptor<NGramTokenFilterDescriptor>, IBuildableDescriptor<NGramTokenFilter>
{
	internal NGramTokenFilterDescriptor(Action<NGramTokenFilterDescriptor> configure) => configure.Invoke(this);

	public NGramTokenFilterDescriptor() : base()
	{
	}

	private int? MaxGramValue { get; set; }
	private int? MinGramValue { get; set; }
	private bool? PreserveOriginalValue { get; set; }
	private string? VersionValue { get; set; }

	public NGramTokenFilterDescriptor MaxGram(int? maxGram)
	{
		MaxGramValue = maxGram;
		return Self;
	}

	public NGramTokenFilterDescriptor MinGram(int? minGram)
	{
		MinGramValue = minGram;
		return Self;
	}

	public NGramTokenFilterDescriptor PreserveOriginal(bool? preserveOriginal = true)
	{
		PreserveOriginalValue = preserveOriginal;
		return Self;
	}

	public NGramTokenFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MaxGramValue.HasValue)
		{
			writer.WritePropertyName("max_gram");
			writer.WriteNumberValue(MaxGramValue.Value);
		}

		if (MinGramValue.HasValue)
		{
			writer.WritePropertyName("min_gram");
			writer.WriteNumberValue(MinGramValue.Value);
		}

		if (PreserveOriginalValue.HasValue)
		{
			writer.WritePropertyName("preserve_original");
			writer.WriteBooleanValue(PreserveOriginalValue.Value);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("ngram");
		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		writer.WriteEndObject();
	}

	NGramTokenFilter IBuildableDescriptor<NGramTokenFilter>.Build() => new()
	{
		MaxGram = MaxGramValue,
		MinGram = MinGramValue,
		PreserveOriginal = PreserveOriginalValue,
		Version = VersionValue
	};
}