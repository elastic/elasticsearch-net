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

public sealed partial class InferenceResponseResult
{
	/// <summary>
	/// <para>
	/// If the model is trained for named entity recognition (NER) tasks, the response contains the recognized entities.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("entities")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelEntities>? Entities { get; init; }

	/// <summary>
	/// <para>
	/// The feature importance for the inference results. Relevant only for classification or regression models
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("feature_importance")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance>? FeatureImportance { get; init; }

	/// <summary>
	/// <para>
	/// Indicates whether the input text was truncated to meet the model's maximum sequence length limit. This property
	/// is present only when it is true.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("is_truncated")]
	public bool? IsTruncated { get; init; }

	/// <summary>
	/// <para>
	/// If the model is trained for a text classification or zero shot classification task, the response is the
	/// predicted class.
	/// For named entity recognition (NER) tasks, it contains the annotated text output.
	/// For fill mask tasks, it contains the top prediction for replacing the mask token.
	/// For text embedding tasks, it contains the raw numerical text embedding values.
	/// For regression models, its a numerical value
	/// For classification models, it may be an integer, double, boolean or string depending on prediction type
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("predicted_value")]
	[SingleOrManyCollectionConverter(typeof(object))]
	public IReadOnlyCollection<object>? PredictedValue { get; init; }

	/// <summary>
	/// <para>
	/// For fill mask tasks, the response contains the input text sequence with the mask token replaced by the predicted
	/// value.
	/// Additionally
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("predicted_value_sequence")]
	public string? PredictedValueSequence { get; init; }

	/// <summary>
	/// <para>
	/// Specifies a probability for the predicted value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prediction_probability")]
	public double? PredictionProbability { get; init; }

	/// <summary>
	/// <para>
	/// Specifies a confidence score for the predicted value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prediction_score")]
	public double? PredictionScore { get; init; }

	/// <summary>
	/// <para>
	/// For fill mask, text classification, and zero shot classification tasks, the response contains a list of top
	/// class entries.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("top_classes")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TopClassEntry>? TopClasses { get; init; }

	/// <summary>
	/// <para>
	/// If the request failed, the response contains the reason for the failure.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("warning")]
	public string? Warning { get; init; }
}