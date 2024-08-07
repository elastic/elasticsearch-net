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

public sealed partial class DataframeEvaluationClassificationMetrics
{
	/// <summary>
	/// <para>
	/// Accuracy of predictions (per-class and overall).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("accuracy")]
	public IDictionary<string, object>? Accuracy { get; set; }

	/// <summary>
	/// <para>
	/// The AUC ROC (area under the curve of the receiver operating characteristic) score and optionally the curve. It is calculated for a specific class (provided as "class_name") treated as positive.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("auc_roc")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRoc? AucRoc { get; set; }

	/// <summary>
	/// <para>
	/// Multiclass confusion matrix.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("multiclass_confusion_matrix")]
	public IDictionary<string, object>? MulticlassConfusionMatrix { get; set; }

	/// <summary>
	/// <para>
	/// Precision of predictions (per-class and average).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("precision")]
	public IDictionary<string, object>? Precision { get; set; }

	/// <summary>
	/// <para>
	/// Recall of predictions (per-class and average).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("recall")]
	public IDictionary<string, object>? Recall { get; set; }
}

public sealed partial class DataframeEvaluationClassificationMetricsDescriptor : SerializableDescriptor<DataframeEvaluationClassificationMetricsDescriptor>
{
	internal DataframeEvaluationClassificationMetricsDescriptor(Action<DataframeEvaluationClassificationMetricsDescriptor> configure) => configure.Invoke(this);

	public DataframeEvaluationClassificationMetricsDescriptor() : base()
	{
	}

	private IDictionary<string, object>? AccuracyValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRoc? AucRocValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRocDescriptor AucRocDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRocDescriptor> AucRocDescriptorAction { get; set; }
	private IDictionary<string, object>? MulticlassConfusionMatrixValue { get; set; }
	private IDictionary<string, object>? PrecisionValue { get; set; }
	private IDictionary<string, object>? RecallValue { get; set; }

	/// <summary>
	/// <para>
	/// Accuracy of predictions (per-class and overall).
	/// </para>
	/// </summary>
	public DataframeEvaluationClassificationMetricsDescriptor Accuracy(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		AccuracyValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The AUC ROC (area under the curve of the receiver operating characteristic) score and optionally the curve. It is calculated for a specific class (provided as "class_name") treated as positive.
	/// </para>
	/// </summary>
	public DataframeEvaluationClassificationMetricsDescriptor AucRoc(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRoc? aucRoc)
	{
		AucRocDescriptor = null;
		AucRocDescriptorAction = null;
		AucRocValue = aucRoc;
		return Self;
	}

	public DataframeEvaluationClassificationMetricsDescriptor AucRoc(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRocDescriptor descriptor)
	{
		AucRocValue = null;
		AucRocDescriptorAction = null;
		AucRocDescriptor = descriptor;
		return Self;
	}

	public DataframeEvaluationClassificationMetricsDescriptor AucRoc(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRocDescriptor> configure)
	{
		AucRocValue = null;
		AucRocDescriptor = null;
		AucRocDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Multiclass confusion matrix.
	/// </para>
	/// </summary>
	public DataframeEvaluationClassificationMetricsDescriptor MulticlassConfusionMatrix(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MulticlassConfusionMatrixValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Precision of predictions (per-class and average).
	/// </para>
	/// </summary>
	public DataframeEvaluationClassificationMetricsDescriptor Precision(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		PrecisionValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Recall of predictions (per-class and average).
	/// </para>
	/// </summary>
	public DataframeEvaluationClassificationMetricsDescriptor Recall(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		RecallValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AccuracyValue is not null)
		{
			writer.WritePropertyName("accuracy");
			JsonSerializer.Serialize(writer, AccuracyValue, options);
		}

		if (AucRocDescriptor is not null)
		{
			writer.WritePropertyName("auc_roc");
			JsonSerializer.Serialize(writer, AucRocDescriptor, options);
		}
		else if (AucRocDescriptorAction is not null)
		{
			writer.WritePropertyName("auc_roc");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRocDescriptor(AucRocDescriptorAction), options);
		}
		else if (AucRocValue is not null)
		{
			writer.WritePropertyName("auc_roc");
			JsonSerializer.Serialize(writer, AucRocValue, options);
		}

		if (MulticlassConfusionMatrixValue is not null)
		{
			writer.WritePropertyName("multiclass_confusion_matrix");
			JsonSerializer.Serialize(writer, MulticlassConfusionMatrixValue, options);
		}

		if (PrecisionValue is not null)
		{
			writer.WritePropertyName("precision");
			JsonSerializer.Serialize(writer, PrecisionValue, options);
		}

		if (RecallValue is not null)
		{
			writer.WritePropertyName("recall");
			JsonSerializer.Serialize(writer, RecallValue, options);
		}

		writer.WriteEndObject();
	}
}