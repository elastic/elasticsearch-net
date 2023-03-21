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
/// <para>Question answering inference options</para>
/// </summary>
public sealed partial class QuestionAnsweringInferenceOptions
{
	/// <summary>
	/// <para>The maximum answer length to consider</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_answer_length")]
	public int? MaxAnswerLength { get; set; }

	/// <summary>
	/// <para>Specifies the number of top class predictions to return. Defaults to 0.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("num_top_classes")]
	public int? NumTopClasses { get; set; }

	/// <summary>
	/// <para>The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("results_field")]
	public string? ResultsField { get; set; }

	/// <summary>
	/// <para>The tokenization options to update when inferring</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tokenization")]
	public Elastic.Clients.Elasticsearch.Ml.TokenizationConfig? Tokenization { get; set; }

	public static implicit operator InferenceConfigCreate(QuestionAnsweringInferenceOptions questionAnsweringInferenceOptions) => Ml.InferenceConfigCreate.QuestionAnswering(questionAnsweringInferenceOptions);
}

public sealed partial class QuestionAnsweringInferenceOptionsDescriptor : SerializableDescriptor<QuestionAnsweringInferenceOptionsDescriptor>
{
	internal QuestionAnsweringInferenceOptionsDescriptor(Action<QuestionAnsweringInferenceOptionsDescriptor> configure) => configure.Invoke(this);

	public QuestionAnsweringInferenceOptionsDescriptor() : base()
	{
	}

	private int? MaxAnswerLengthValue { get; set; }
	private int? NumTopClassesValue { get; set; }
	private string? ResultsFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ml.TokenizationConfig? TokenizationValue { get; set; }
	private TokenizationConfigDescriptor TokenizationDescriptor { get; set; }
	private Action<TokenizationConfigDescriptor> TokenizationDescriptorAction { get; set; }

	/// <summary>
	/// <para>The maximum answer length to consider</para>
	/// </summary>
	public QuestionAnsweringInferenceOptionsDescriptor MaxAnswerLength(int? maxAnswerLength)
	{
		MaxAnswerLengthValue = maxAnswerLength;
		return Self;
	}

	/// <summary>
	/// <para>Specifies the number of top class predictions to return. Defaults to 0.</para>
	/// </summary>
	public QuestionAnsweringInferenceOptionsDescriptor NumTopClasses(int? numTopClasses)
	{
		NumTopClassesValue = numTopClasses;
		return Self;
	}

	/// <summary>
	/// <para>The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.</para>
	/// </summary>
	public QuestionAnsweringInferenceOptionsDescriptor ResultsField(string? resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>The tokenization options to update when inferring</para>
	/// </summary>
	public QuestionAnsweringInferenceOptionsDescriptor Tokenization(Elastic.Clients.Elasticsearch.Ml.TokenizationConfig? tokenization)
	{
		TokenizationDescriptor = null;
		TokenizationDescriptorAction = null;
		TokenizationValue = tokenization;
		return Self;
	}

	public QuestionAnsweringInferenceOptionsDescriptor Tokenization(TokenizationConfigDescriptor descriptor)
	{
		TokenizationValue = null;
		TokenizationDescriptorAction = null;
		TokenizationDescriptor = descriptor;
		return Self;
	}

	public QuestionAnsweringInferenceOptionsDescriptor Tokenization(Action<TokenizationConfigDescriptor> configure)
	{
		TokenizationValue = null;
		TokenizationDescriptor = null;
		TokenizationDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MaxAnswerLengthValue.HasValue)
		{
			writer.WritePropertyName("max_answer_length");
			writer.WriteNumberValue(MaxAnswerLengthValue.Value);
		}

		if (NumTopClassesValue.HasValue)
		{
			writer.WritePropertyName("num_top_classes");
			writer.WriteNumberValue(NumTopClassesValue.Value);
		}

		if (!string.IsNullOrEmpty(ResultsFieldValue))
		{
			writer.WritePropertyName("results_field");
			writer.WriteStringValue(ResultsFieldValue);
		}

		if (TokenizationDescriptor is not null)
		{
			writer.WritePropertyName("tokenization");
			JsonSerializer.Serialize(writer, TokenizationDescriptor, options);
		}
		else if (TokenizationDescriptorAction is not null)
		{
			writer.WritePropertyName("tokenization");
			JsonSerializer.Serialize(writer, new TokenizationConfigDescriptor(TokenizationDescriptorAction), options);
		}
		else if (TokenizationValue is not null)
		{
			writer.WritePropertyName("tokenization");
			JsonSerializer.Serialize(writer, TokenizationValue, options);
		}

		writer.WriteEndObject();
	}
}