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

public sealed partial class TrainedModelConfigMetadata
{
	/// <summary>
	/// <para>
	/// An object that contains the baseline for feature importance values. For regression analysis, it is a single value. For classification analysis, there is a value for each class.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("feature_importance_baseline")]
	public IReadOnlyDictionary<string, string>? FeatureImportanceBaseline { get; init; }

	/// <summary>
	/// <para>
	/// List of the available hyperparameters optimized during the fine_parameter_tuning phase as well as specified by the user.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("hyperparameters")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Hyperparameter>? Hyperparameters { get; init; }
	[JsonInclude, JsonPropertyName("model_aliases")]
	public IReadOnlyCollection<string>? ModelAliases { get; init; }

	/// <summary>
	/// <para>
	/// An array of the total feature importance for each feature used from the training data set. This array of objects is returned if data frame analytics trained the model and the request includes total_feature_importance in the include request parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_feature_importance")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportance>? TotalFeatureImportance { get; init; }
}