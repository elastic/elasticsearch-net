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

internal sealed partial class InferenceResponseResultConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.InferenceResponseResult>
{
	private static readonly System.Text.Json.JsonEncodedText PropEntities = System.Text.Json.JsonEncodedText.Encode("entities");
	private static readonly System.Text.Json.JsonEncodedText PropFeatureImportance = System.Text.Json.JsonEncodedText.Encode("feature_importance");
	private static readonly System.Text.Json.JsonEncodedText PropIsTruncated = System.Text.Json.JsonEncodedText.Encode("is_truncated");
	private static readonly System.Text.Json.JsonEncodedText PropPredictedValue = System.Text.Json.JsonEncodedText.Encode("predicted_value");
	private static readonly System.Text.Json.JsonEncodedText PropPredictedValueSequence = System.Text.Json.JsonEncodedText.Encode("predicted_value_sequence");
	private static readonly System.Text.Json.JsonEncodedText PropPredictionProbability = System.Text.Json.JsonEncodedText.Encode("prediction_probability");
	private static readonly System.Text.Json.JsonEncodedText PropPredictionScore = System.Text.Json.JsonEncodedText.Encode("prediction_score");
	private static readonly System.Text.Json.JsonEncodedText PropTopClasses = System.Text.Json.JsonEncodedText.Encode("top_classes");
	private static readonly System.Text.Json.JsonEncodedText PropWarning = System.Text.Json.JsonEncodedText.Encode("warning");

	public override Elastic.Clients.Elasticsearch.MachineLearning.InferenceResponseResult Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelEntities>?> propEntities = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance>?> propFeatureImportance = default;
		LocalJsonValue<bool?> propIsTruncated = default;
		LocalJsonValue<System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>?> propPredictedValue = default;
		LocalJsonValue<string?> propPredictedValueSequence = default;
		LocalJsonValue<double?> propPredictionProbability = default;
		LocalJsonValue<double?> propPredictionScore = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TopClassEntry>?> propTopClasses = default;
		LocalJsonValue<string?> propWarning = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEntities.TryReadProperty(ref reader, options, PropEntities, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelEntities>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelEntities>(o, null)))
			{
				continue;
			}

			if (propFeatureImportance.TryReadProperty(ref reader, options, PropFeatureImportance, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance>(o, null)))
			{
				continue;
			}

			if (propIsTruncated.TryReadProperty(ref reader, options, PropIsTruncated, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propPredictedValue.TryReadProperty(ref reader, options, PropPredictedValue, static System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(o, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, null)!)))
			{
				continue;
			}

			if (propPredictedValueSequence.TryReadProperty(ref reader, options, PropPredictedValueSequence, null))
			{
				continue;
			}

			if (propPredictionProbability.TryReadProperty(ref reader, options, PropPredictionProbability, static double? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<double>(o)))
			{
				continue;
			}

			if (propPredictionScore.TryReadProperty(ref reader, options, PropPredictionScore, static double? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<double>(o)))
			{
				continue;
			}

			if (propTopClasses.TryReadProperty(ref reader, options, PropTopClasses, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TopClassEntry>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TopClassEntry>(o, null)))
			{
				continue;
			}

			if (propWarning.TryReadProperty(ref reader, options, PropWarning, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.InferenceResponseResult(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Entities = propEntities.Value,
			FeatureImportance = propFeatureImportance.Value,
			IsTruncated = propIsTruncated.Value,
			PredictedValue = propPredictedValue.Value,
			PredictedValueSequence = propPredictedValueSequence.Value,
			PredictionProbability = propPredictionProbability.Value,
			PredictionScore = propPredictionScore.Value,
			TopClasses = propTopClasses.Value,
			Warning = propWarning.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.InferenceResponseResult value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEntities, value.Entities, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelEntities>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelEntities>(o, v, null));
		writer.WriteProperty(options, PropFeatureImportance, value.FeatureImportance, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance>(o, v, null));
		writer.WriteProperty(options, PropIsTruncated, value.IsTruncated, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropPredictedValue, value.PredictedValue, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>? v) => w.WriteSingleOrManyCollectionValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(o, v, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, v, null)));
		writer.WriteProperty(options, PropPredictedValueSequence, value.PredictedValueSequence, null, null);
		writer.WriteProperty(options, PropPredictionProbability, value.PredictionProbability, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, double? v) => w.WriteNullableValue<double>(o, v));
		writer.WriteProperty(options, PropPredictionScore, value.PredictionScore, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, double? v) => w.WriteNullableValue<double>(o, v));
		writer.WriteProperty(options, PropTopClasses, value.TopClasses, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TopClassEntry>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TopClassEntry>(o, v, null));
		writer.WriteProperty(options, PropWarning, value.Warning, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.InferenceResponseResultConverter))]
public sealed partial class InferenceResponseResult
{
#if NET7_0_OR_GREATER
	public InferenceResponseResult()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public InferenceResponseResult()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal InferenceResponseResult(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If the model is trained for named entity recognition (NER) tasks, the response contains the recognized entities.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelEntities>? Entities { get; set; }

	/// <summary>
	/// <para>
	/// The feature importance for the inference results. Relevant only for classification or regression models
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance>? FeatureImportance { get; set; }

	/// <summary>
	/// <para>
	/// Indicates whether the input text was truncated to meet the model's maximum sequence length limit. This property
	/// is present only when it is true.
	/// </para>
	/// </summary>
	public bool? IsTruncated { get; set; }

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
	public System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>? PredictedValue { get; set; }

	/// <summary>
	/// <para>
	/// For fill mask tasks, the response contains the input text sequence with the mask token replaced by the predicted
	/// value.
	/// Additionally
	/// </para>
	/// </summary>
	public string? PredictedValueSequence { get; set; }

	/// <summary>
	/// <para>
	/// Specifies a probability for the predicted value.
	/// </para>
	/// </summary>
	public double? PredictionProbability { get; set; }

	/// <summary>
	/// <para>
	/// Specifies a confidence score for the predicted value.
	/// </para>
	/// </summary>
	public double? PredictionScore { get; set; }

	/// <summary>
	/// <para>
	/// For fill mask, text classification, and zero shot classification tasks, the response contains a list of top
	/// class entries.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TopClassEntry>? TopClasses { get; set; }

	/// <summary>
	/// <para>
	/// If the request failed, the response contains the reason for the failure.
	/// </para>
	/// </summary>
	public string? Warning { get; set; }
}