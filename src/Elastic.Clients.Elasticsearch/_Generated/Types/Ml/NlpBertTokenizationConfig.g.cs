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

namespace Elastic.Clients.Elasticsearch.Ml;

/// <summary>
/// <para>BERT and MPNet tokenization configuration options</para>
/// </summary>
public sealed partial class NlpBertTokenizationConfig
{
	/// <summary>
	/// <para>Should the tokenizer lower case the text</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("do_lower_case")]
	public bool? DoLowerCase { get; set; }

	/// <summary>
	/// <para>Maximum input sequence length for the model</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_sequence_length")]
	public int? MaxSequenceLength { get; set; }

	/// <summary>
	/// <para>Tokenization spanning options. Special value of -1 indicates no spanning takes place</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("span")]
	public int? Span { get; set; }

	/// <summary>
	/// <para>Should tokenization input be automatically truncated before sending to the model for inference</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("truncate")]
	public Elastic.Clients.Elasticsearch.Ml.TokenizationTruncate? Truncate { get; set; }

	/// <summary>
	/// <para>Is tokenization completed with special tokens</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("with_special_tokens")]
	public bool? WithSpecialTokens { get; set; }
}

public sealed partial class NlpBertTokenizationConfigDescriptor : SerializableDescriptor<NlpBertTokenizationConfigDescriptor>
{
	internal NlpBertTokenizationConfigDescriptor(Action<NlpBertTokenizationConfigDescriptor> configure) => configure.Invoke(this);

	public NlpBertTokenizationConfigDescriptor() : base()
	{
	}

	private bool? DoLowerCaseValue { get; set; }
	private int? MaxSequenceLengthValue { get; set; }
	private int? SpanValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ml.TokenizationTruncate? TruncateValue { get; set; }
	private bool? WithSpecialTokensValue { get; set; }

	/// <summary>
	/// <para>Should the tokenizer lower case the text</para>
	/// </summary>
	public NlpBertTokenizationConfigDescriptor DoLowerCase(bool? doLowerCase = true)
	{
		DoLowerCaseValue = doLowerCase;
		return Self;
	}

	/// <summary>
	/// <para>Maximum input sequence length for the model</para>
	/// </summary>
	public NlpBertTokenizationConfigDescriptor MaxSequenceLength(int? maxSequenceLength)
	{
		MaxSequenceLengthValue = maxSequenceLength;
		return Self;
	}

	/// <summary>
	/// <para>Tokenization spanning options. Special value of -1 indicates no spanning takes place</para>
	/// </summary>
	public NlpBertTokenizationConfigDescriptor Span(int? span)
	{
		SpanValue = span;
		return Self;
	}

	/// <summary>
	/// <para>Should tokenization input be automatically truncated before sending to the model for inference</para>
	/// </summary>
	public NlpBertTokenizationConfigDescriptor Truncate(Elastic.Clients.Elasticsearch.Ml.TokenizationTruncate? truncate)
	{
		TruncateValue = truncate;
		return Self;
	}

	/// <summary>
	/// <para>Is tokenization completed with special tokens</para>
	/// </summary>
	public NlpBertTokenizationConfigDescriptor WithSpecialTokens(bool? withSpecialTokens = true)
	{
		WithSpecialTokensValue = withSpecialTokens;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DoLowerCaseValue.HasValue)
		{
			writer.WritePropertyName("do_lower_case");
			writer.WriteBooleanValue(DoLowerCaseValue.Value);
		}

		if (MaxSequenceLengthValue.HasValue)
		{
			writer.WritePropertyName("max_sequence_length");
			writer.WriteNumberValue(MaxSequenceLengthValue.Value);
		}

		if (SpanValue.HasValue)
		{
			writer.WritePropertyName("span");
			writer.WriteNumberValue(SpanValue.Value);
		}

		if (TruncateValue is not null)
		{
			writer.WritePropertyName("truncate");
			JsonSerializer.Serialize(writer, TruncateValue, options);
		}

		if (WithSpecialTokensValue.HasValue)
		{
			writer.WritePropertyName("with_special_tokens");
			writer.WriteBooleanValue(WithSpecialTokensValue.Value);
		}

		writer.WriteEndObject();
	}
}