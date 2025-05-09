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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class DataframeEvaluationOutlierDetectionMetricsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics>
{
	private static readonly System.Text.Json.JsonEncodedText PropAucRoc = System.Text.Json.JsonEncodedText.Encode("auc_roc");
	private static readonly System.Text.Json.JsonEncodedText PropConfusionMatrix = System.Text.Json.JsonEncodedText.Encode("confusion_matrix");
	private static readonly System.Text.Json.JsonEncodedText PropPrecision = System.Text.Json.JsonEncodedText.Encode("precision");
	private static readonly System.Text.Json.JsonEncodedText PropRecall = System.Text.Json.JsonEncodedText.Encode("recall");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRoc?> propAucRoc = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propConfusionMatrix = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propPrecision = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propRecall = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAucRoc.TryReadProperty(ref reader, options, PropAucRoc, null))
			{
				continue;
			}

			if (propConfusionMatrix.TryReadProperty(ref reader, options, PropConfusionMatrix, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propPrecision.TryReadProperty(ref reader, options, PropPrecision, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propRecall.TryReadProperty(ref reader, options, PropRecall, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AucRoc = propAucRoc.Value,
			ConfusionMatrix = propConfusionMatrix.Value,
			Precision = propPrecision.Value,
			Recall = propRecall.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAucRoc, value.AucRoc, null, null);
		writer.WriteProperty(options, PropConfusionMatrix, value.ConfusionMatrix, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropPrecision, value.Precision, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropRecall, value.Recall, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsConverter))]
public sealed partial class DataframeEvaluationOutlierDetectionMetrics
{
#if NET7_0_OR_GREATER
	public DataframeEvaluationOutlierDetectionMetrics()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DataframeEvaluationOutlierDetectionMetrics()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataframeEvaluationOutlierDetectionMetrics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The AUC ROC (area under the curve of the receiver operating characteristic) score and optionally the curve. It is calculated for a specific class (provided as "class_name") treated as positive.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRoc? AucRoc { get; set; }

	/// <summary>
	/// <para>
	/// Accuracy of predictions (per-class and overall).
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? ConfusionMatrix { get; set; }

	/// <summary>
	/// <para>
	/// Precision of predictions (per-class and average).
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Precision { get; set; }

	/// <summary>
	/// <para>
	/// Recall of predictions (per-class and average).
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Recall { get; set; }
}

public readonly partial struct DataframeEvaluationOutlierDetectionMetricsDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeEvaluationOutlierDetectionMetricsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeEvaluationOutlierDetectionMetricsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The AUC ROC (area under the curve of the receiver operating characteristic) score and optionally the curve. It is calculated for a specific class (provided as "class_name") treated as positive.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor AucRoc(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRoc? value)
	{
		Instance.AucRoc = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The AUC ROC (area under the curve of the receiver operating characteristic) score and optionally the curve. It is calculated for a specific class (provided as "class_name") treated as positive.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor AucRoc()
	{
		Instance.AucRoc = Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRocDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The AUC ROC (area under the curve of the receiver operating characteristic) score and optionally the curve. It is calculated for a specific class (provided as "class_name") treated as positive.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor AucRoc(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRocDescriptor>? action)
	{
		Instance.AucRoc = Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationMetricsAucRocDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Accuracy of predictions (per-class and overall).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor ConfusionMatrix(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.ConfusionMatrix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Accuracy of predictions (per-class and overall).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor ConfusionMatrix()
	{
		Instance.ConfusionMatrix = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Accuracy of predictions (per-class and overall).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor ConfusionMatrix(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.ConfusionMatrix = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor AddConfusionMatrix(string key, object value)
	{
		Instance.ConfusionMatrix ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.ConfusionMatrix.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Precision of predictions (per-class and average).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor Precision(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Precision = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Precision of predictions (per-class and average).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor Precision()
	{
		Instance.Precision = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Precision of predictions (per-class and average).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor Precision(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Precision = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor AddPrecision(string key, object value)
	{
		Instance.Precision ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Precision.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Recall of predictions (per-class and average).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor Recall(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Recall = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Recall of predictions (per-class and average).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor Recall()
	{
		Instance.Recall = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Recall of predictions (per-class and average).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor Recall(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Recall = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor AddRecall(string key, object value)
	{
		Instance.Recall ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Recall.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetricsDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionMetrics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}