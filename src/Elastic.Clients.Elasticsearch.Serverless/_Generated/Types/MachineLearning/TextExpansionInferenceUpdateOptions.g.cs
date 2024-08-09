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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class TextExpansionInferenceUpdateOptions
{
	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("results_field")]
	public string? ResultsField { get; set; }
	[JsonInclude, JsonPropertyName("tokenization")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.NlpTokenizationUpdateOptions? Tokenization { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate(TextExpansionInferenceUpdateOptions textExpansionInferenceUpdateOptions) => Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate.TextExpansion(textExpansionInferenceUpdateOptions);
}

public sealed partial class TextExpansionInferenceUpdateOptionsDescriptor : SerializableDescriptor<TextExpansionInferenceUpdateOptionsDescriptor>
{
	internal TextExpansionInferenceUpdateOptionsDescriptor(Action<TextExpansionInferenceUpdateOptionsDescriptor> configure) => configure.Invoke(this);

	public TextExpansionInferenceUpdateOptionsDescriptor() : base()
	{
	}

	private string? ResultsFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MachineLearning.NlpTokenizationUpdateOptions? TokenizationValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MachineLearning.NlpTokenizationUpdateOptionsDescriptor TokenizationDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.MachineLearning.NlpTokenizationUpdateOptionsDescriptor> TokenizationDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public TextExpansionInferenceUpdateOptionsDescriptor ResultsField(string? resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	public TextExpansionInferenceUpdateOptionsDescriptor Tokenization(Elastic.Clients.Elasticsearch.Serverless.MachineLearning.NlpTokenizationUpdateOptions? tokenization)
	{
		TokenizationDescriptor = null;
		TokenizationDescriptorAction = null;
		TokenizationValue = tokenization;
		return Self;
	}

	public TextExpansionInferenceUpdateOptionsDescriptor Tokenization(Elastic.Clients.Elasticsearch.Serverless.MachineLearning.NlpTokenizationUpdateOptionsDescriptor descriptor)
	{
		TokenizationValue = null;
		TokenizationDescriptorAction = null;
		TokenizationDescriptor = descriptor;
		return Self;
	}

	public TextExpansionInferenceUpdateOptionsDescriptor Tokenization(Action<Elastic.Clients.Elasticsearch.Serverless.MachineLearning.NlpTokenizationUpdateOptionsDescriptor> configure)
	{
		TokenizationValue = null;
		TokenizationDescriptor = null;
		TokenizationDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.MachineLearning.NlpTokenizationUpdateOptionsDescriptor(TokenizationDescriptorAction), options);
		}
		else if (TokenizationValue is not null)
		{
			writer.WritePropertyName("tokenization");
			JsonSerializer.Serialize(writer, TokenizationValue, options);
		}

		writer.WriteEndObject();
	}
}