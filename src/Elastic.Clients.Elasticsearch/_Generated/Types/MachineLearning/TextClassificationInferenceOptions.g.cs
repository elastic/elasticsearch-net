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

/// <summary>
/// <para>
/// Text classification configuration options
/// </para>
/// </summary>
public sealed partial class TextClassificationInferenceOptions
{
	/// <summary>
	/// <para>
	/// Classification labels to apply other than the stored labels. Must have the same deminsions as the default configured labels
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("classification_labels")]
	public ICollection<string>? ClassificationLabels { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the number of top class predictions to return. Defaults to 0.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("num_top_classes")]
	public int? NumTopClasses { get; set; }

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("results_field")]
	public string? ResultsField { get; set; }

	/// <summary>
	/// <para>
	/// The tokenization options
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tokenization")]
	public Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfig? Tokenization { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreate(TextClassificationInferenceOptions textClassificationInferenceOptions) => Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreate.TextClassification(textClassificationInferenceOptions);
}

/// <summary>
/// <para>
/// Text classification configuration options
/// </para>
/// </summary>
public sealed partial class TextClassificationInferenceOptionsDescriptor : SerializableDescriptor<TextClassificationInferenceOptionsDescriptor>
{
	internal TextClassificationInferenceOptionsDescriptor(Action<TextClassificationInferenceOptionsDescriptor> configure) => configure.Invoke(this);

	public TextClassificationInferenceOptionsDescriptor() : base()
	{
	}

	private ICollection<string>? ClassificationLabelsValue { get; set; }
	private int? NumTopClassesValue { get; set; }
	private string? ResultsFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfig? TokenizationValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfigDescriptor TokenizationDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfigDescriptor> TokenizationDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Classification labels to apply other than the stored labels. Must have the same deminsions as the default configured labels
	/// </para>
	/// </summary>
	public TextClassificationInferenceOptionsDescriptor ClassificationLabels(ICollection<string>? classificationLabels)
	{
		ClassificationLabelsValue = classificationLabels;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the number of top class predictions to return. Defaults to 0.
	/// </para>
	/// </summary>
	public TextClassificationInferenceOptionsDescriptor NumTopClasses(int? numTopClasses)
	{
		NumTopClassesValue = numTopClasses;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public TextClassificationInferenceOptionsDescriptor ResultsField(string? resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The tokenization options
	/// </para>
	/// </summary>
	public TextClassificationInferenceOptionsDescriptor Tokenization(Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfig? tokenization)
	{
		TokenizationDescriptor = null;
		TokenizationDescriptorAction = null;
		TokenizationValue = tokenization;
		return Self;
	}

	public TextClassificationInferenceOptionsDescriptor Tokenization(Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfigDescriptor descriptor)
	{
		TokenizationValue = null;
		TokenizationDescriptorAction = null;
		TokenizationDescriptor = descriptor;
		return Self;
	}

	public TextClassificationInferenceOptionsDescriptor Tokenization(Action<Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfigDescriptor> configure)
	{
		TokenizationValue = null;
		TokenizationDescriptor = null;
		TokenizationDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ClassificationLabelsValue is not null)
		{
			writer.WritePropertyName("classification_labels");
			JsonSerializer.Serialize(writer, ClassificationLabelsValue, options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfigDescriptor(TokenizationDescriptorAction), options);
		}
		else if (TokenizationValue is not null)
		{
			writer.WritePropertyName("tokenization");
			JsonSerializer.Serialize(writer, TokenizationValue, options);
		}

		writer.WriteEndObject();
	}
}