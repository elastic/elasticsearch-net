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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class NlpTokenizationUpdateOptions
{
	/// <summary>
	/// <para>
	/// Span options to apply
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("span")]
	public int? Span { get; set; }

	/// <summary>
	/// <para>
	/// Truncate options to apply
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("truncate")]
	public Elastic.Clients.Elasticsearch.MachineLearning.TokenizationTruncate? Truncate { get; set; }
}

public sealed partial class NlpTokenizationUpdateOptionsDescriptor : SerializableDescriptor<NlpTokenizationUpdateOptionsDescriptor>
{
	internal NlpTokenizationUpdateOptionsDescriptor(Action<NlpTokenizationUpdateOptionsDescriptor> configure) => configure.Invoke(this);

	public NlpTokenizationUpdateOptionsDescriptor() : base()
	{
	}

	private int? SpanValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.TokenizationTruncate? TruncateValue { get; set; }

	/// <summary>
	/// <para>
	/// Span options to apply
	/// </para>
	/// </summary>
	public NlpTokenizationUpdateOptionsDescriptor Span(int? span)
	{
		SpanValue = span;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Truncate options to apply
	/// </para>
	/// </summary>
	public NlpTokenizationUpdateOptionsDescriptor Truncate(Elastic.Clients.Elasticsearch.MachineLearning.TokenizationTruncate? truncate)
	{
		TruncateValue = truncate;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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

		writer.WriteEndObject();
	}
}