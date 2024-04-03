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

public sealed partial class DetectorRead
{
	/// <summary>
	/// <para>The field used to split the data.<br/>In particular, this property is used for analyzing the splits with respect to their own history.<br/>It is used for finding unusual values in the context of the split.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("by_field_name")]
	public string? ByFieldName { get; init; }

	/// <summary>
	/// <para>An array of custom rule objects, which enable you to customize the way detectors operate.<br/>For example, a rule may dictate to the detector conditions under which results should be skipped.<br/>Kibana refers to custom rules as job rules.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("custom_rules")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>? CustomRules { get; init; }

	/// <summary>
	/// <para>A description of the detector.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("detector_description")]
	public string? DetectorDescription { get; init; }

	/// <summary>
	/// <para>A unique identifier for the detector.<br/>This identifier is based on the order of the detectors in the `analysis_config`, starting at zero.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("detector_index")]
	public int? DetectorIndex { get; init; }

	/// <summary>
	/// <para>Contains one of the following values: `all`, `none`, `by`, or `over`.<br/>If set, frequent entities are excluded from influencing the anomaly results.<br/>Entities can be considered frequent over time or frequent in a population.<br/>If you are working with both over and by fields, then you can set `exclude_frequent` to all for both fields, or to `by` or `over` for those specific fields.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("exclude_frequent")]
	public Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent? ExcludeFrequent { get; init; }

	/// <summary>
	/// <para>The field that the detector uses in the function.<br/>If you use an event rate function such as `count` or `rare`, do not specify this field.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field_name")]
	public string? FieldName { get; init; }

	/// <summary>
	/// <para>The analysis function that is used.<br/>For example, `count`, `rare`, `mean`, `min`, `max`, and `sum`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("function")]
	public string Function { get; init; }

	/// <summary>
	/// <para>The field used to split the data.<br/>In particular, this property is used for analyzing the splits with respect to the history of all splits.<br/>It is used for finding unusual values in the population of all splits.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("over_field_name")]
	public string? OverFieldName { get; init; }

	/// <summary>
	/// <para>The field used to segment the analysis.<br/>When you use this property, you have completely independent baselines for each value of this field.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("partition_field_name")]
	public string? PartitionFieldName { get; init; }

	/// <summary>
	/// <para>Defines whether a new series is used as the null series when there is no value for the by or partition fields.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("use_null")]
	public bool? UseNull { get; init; }
}