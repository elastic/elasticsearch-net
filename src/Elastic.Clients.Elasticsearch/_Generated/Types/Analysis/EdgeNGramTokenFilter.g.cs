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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Analysis;
public sealed partial class EdgeNGramTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("max_gram")]
	public int? MaxGram { get; set; }

	[JsonInclude, JsonPropertyName("min_gram")]
	public int? MinGram { get; set; }

	[JsonInclude, JsonPropertyName("preserve_original")]
	public bool? PreserveOriginal { get; set; }

	[JsonInclude, JsonPropertyName("side")]
	public Elastic.Clients.Elasticsearch.Analysis.EdgeNGramSide? Side { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "edge_ngram";
	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class EdgeNGramTokenFilterDescriptor : SerializableDescriptor<EdgeNGramTokenFilterDescriptor>, IBuildableDescriptor<EdgeNGramTokenFilter>
{
	internal EdgeNGramTokenFilterDescriptor(Action<EdgeNGramTokenFilterDescriptor> configure) => configure.Invoke(this);
	public EdgeNGramTokenFilterDescriptor() : base()
	{
	}

	private int? MaxGramValue { get; set; }

	private int? MinGramValue { get; set; }

	private bool? PreserveOriginalValue { get; set; }

	private Elastic.Clients.Elasticsearch.Analysis.EdgeNGramSide? SideValue { get; set; }

	private string? VersionValue { get; set; }

	public EdgeNGramTokenFilterDescriptor MaxGram(int? maxGram)
	{
		MaxGramValue = maxGram;
		return Self;
	}

	public EdgeNGramTokenFilterDescriptor MinGram(int? minGram)
	{
		MinGramValue = minGram;
		return Self;
	}

	public EdgeNGramTokenFilterDescriptor PreserveOriginal(bool? preserveOriginal = true)
	{
		PreserveOriginalValue = preserveOriginal;
		return Self;
	}

	public EdgeNGramTokenFilterDescriptor Side(Elastic.Clients.Elasticsearch.Analysis.EdgeNGramSide? side)
	{
		SideValue = side;
		return Self;
	}

	public EdgeNGramTokenFilterDescriptor Version(string? version)
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

		if (SideValue is not null)
		{
			writer.WritePropertyName("side");
			JsonSerializer.Serialize(writer, SideValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("edge_ngram");
		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		writer.WriteEndObject();
	}

	EdgeNGramTokenFilter IBuildableDescriptor<EdgeNGramTokenFilter>.Build() => new()
	{
		MaxGram = MaxGramValue,
		MinGram = MinGramValue,
		PreserveOriginal = PreserveOriginalValue,
		Side = SideValue,
		Version = VersionValue
	};
}