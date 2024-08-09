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

namespace Elastic.Clients.Elasticsearch.Ingest;

public sealed partial class InferenceConfigClassification
{
	/// <summary>
	/// <para>
	/// Specifies the number of top class predictions to return.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("num_top_classes")]
	public int? NumTopClasses { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("num_top_feature_importance_values")]
	public int? NumTopFeatureImportanceValues { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the type of the predicted field to write.
	/// Valid values are: <c>string</c>, <c>number</c>, <c>boolean</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prediction_field_type")]
	public string? PredictionFieldType { get; set; }

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("results_field")]
	public Elastic.Clients.Elasticsearch.Field? ResultsField { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the field to which the top classes are written.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("top_classes_results_field")]
	public Elastic.Clients.Elasticsearch.Field? TopClassesResultsField { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(InferenceConfigClassification inferenceConfigClassification) => Elastic.Clients.Elasticsearch.Ingest.InferenceConfig.Classification(inferenceConfigClassification);
}

public sealed partial class InferenceConfigClassificationDescriptor<TDocument> : SerializableDescriptor<InferenceConfigClassificationDescriptor<TDocument>>
{
	internal InferenceConfigClassificationDescriptor(Action<InferenceConfigClassificationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public InferenceConfigClassificationDescriptor() : base()
	{
	}

	private int? NumTopClassesValue { get; set; }
	private int? NumTopFeatureImportanceValuesValue { get; set; }
	private string? PredictionFieldTypeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? ResultsFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TopClassesResultsFieldValue { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the number of top class predictions to return.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> NumTopClasses(int? numTopClasses)
	{
		NumTopClassesValue = numTopClasses;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> NumTopFeatureImportanceValues(int? numTopFeatureImportanceValues)
	{
		NumTopFeatureImportanceValuesValue = numTopFeatureImportanceValues;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the type of the predicted field to write.
	/// Valid values are: <c>string</c>, <c>number</c>, <c>boolean</c>.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> PredictionFieldType(string? predictionFieldType)
	{
		PredictionFieldTypeValue = predictionFieldType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> ResultsField(Elastic.Clients.Elasticsearch.Field? resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> ResultsField<TValue>(Expression<Func<TDocument, TValue>> resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> ResultsField(Expression<Func<TDocument, object>> resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the field to which the top classes are written.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> TopClassesResultsField(Elastic.Clients.Elasticsearch.Field? topClassesResultsField)
	{
		TopClassesResultsFieldValue = topClassesResultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the field to which the top classes are written.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> TopClassesResultsField<TValue>(Expression<Func<TDocument, TValue>> topClassesResultsField)
	{
		TopClassesResultsFieldValue = topClassesResultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the field to which the top classes are written.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor<TDocument> TopClassesResultsField(Expression<Func<TDocument, object>> topClassesResultsField)
	{
		TopClassesResultsFieldValue = topClassesResultsField;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (NumTopClassesValue.HasValue)
		{
			writer.WritePropertyName("num_top_classes");
			writer.WriteNumberValue(NumTopClassesValue.Value);
		}

		if (NumTopFeatureImportanceValuesValue.HasValue)
		{
			writer.WritePropertyName("num_top_feature_importance_values");
			writer.WriteNumberValue(NumTopFeatureImportanceValuesValue.Value);
		}

		if (!string.IsNullOrEmpty(PredictionFieldTypeValue))
		{
			writer.WritePropertyName("prediction_field_type");
			writer.WriteStringValue(PredictionFieldTypeValue);
		}

		if (ResultsFieldValue is not null)
		{
			writer.WritePropertyName("results_field");
			JsonSerializer.Serialize(writer, ResultsFieldValue, options);
		}

		if (TopClassesResultsFieldValue is not null)
		{
			writer.WritePropertyName("top_classes_results_field");
			JsonSerializer.Serialize(writer, TopClassesResultsFieldValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class InferenceConfigClassificationDescriptor : SerializableDescriptor<InferenceConfigClassificationDescriptor>
{
	internal InferenceConfigClassificationDescriptor(Action<InferenceConfigClassificationDescriptor> configure) => configure.Invoke(this);

	public InferenceConfigClassificationDescriptor() : base()
	{
	}

	private int? NumTopClassesValue { get; set; }
	private int? NumTopFeatureImportanceValuesValue { get; set; }
	private string? PredictionFieldTypeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? ResultsFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TopClassesResultsFieldValue { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the number of top class predictions to return.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor NumTopClasses(int? numTopClasses)
	{
		NumTopClassesValue = numTopClasses;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor NumTopFeatureImportanceValues(int? numTopFeatureImportanceValues)
	{
		NumTopFeatureImportanceValuesValue = numTopFeatureImportanceValues;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the type of the predicted field to write.
	/// Valid values are: <c>string</c>, <c>number</c>, <c>boolean</c>.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor PredictionFieldType(string? predictionFieldType)
	{
		PredictionFieldTypeValue = predictionFieldType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor ResultsField(Elastic.Clients.Elasticsearch.Field? resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor ResultsField<TDocument, TValue>(Expression<Func<TDocument, TValue>> resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor ResultsField<TDocument>(Expression<Func<TDocument, object>> resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the field to which the top classes are written.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor TopClassesResultsField(Elastic.Clients.Elasticsearch.Field? topClassesResultsField)
	{
		TopClassesResultsFieldValue = topClassesResultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the field to which the top classes are written.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor TopClassesResultsField<TDocument, TValue>(Expression<Func<TDocument, TValue>> topClassesResultsField)
	{
		TopClassesResultsFieldValue = topClassesResultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the field to which the top classes are written.
	/// </para>
	/// </summary>
	public InferenceConfigClassificationDescriptor TopClassesResultsField<TDocument>(Expression<Func<TDocument, object>> topClassesResultsField)
	{
		TopClassesResultsFieldValue = topClassesResultsField;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (NumTopClassesValue.HasValue)
		{
			writer.WritePropertyName("num_top_classes");
			writer.WriteNumberValue(NumTopClassesValue.Value);
		}

		if (NumTopFeatureImportanceValuesValue.HasValue)
		{
			writer.WritePropertyName("num_top_feature_importance_values");
			writer.WriteNumberValue(NumTopFeatureImportanceValuesValue.Value);
		}

		if (!string.IsNullOrEmpty(PredictionFieldTypeValue))
		{
			writer.WritePropertyName("prediction_field_type");
			writer.WriteStringValue(PredictionFieldTypeValue);
		}

		if (ResultsFieldValue is not null)
		{
			writer.WritePropertyName("results_field");
			JsonSerializer.Serialize(writer, ResultsFieldValue, options);
		}

		if (TopClassesResultsFieldValue is not null)
		{
			writer.WritePropertyName("top_classes_results_field");
			JsonSerializer.Serialize(writer, TopClassesResultsFieldValue, options);
		}

		writer.WriteEndObject();
	}
}