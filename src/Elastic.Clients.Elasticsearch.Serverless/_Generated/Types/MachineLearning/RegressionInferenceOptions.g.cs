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

public sealed partial class RegressionInferenceOptions
{
	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("num_top_feature_importance_values")]
	public int? NumTopFeatureImportanceValues { get; set; }

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("results_field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field? ResultsField { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Aggregations.InferenceConfig(RegressionInferenceOptions regressionInferenceOptions) => Elastic.Clients.Elasticsearch.Serverless.Aggregations.InferenceConfig.Regression(regressionInferenceOptions);
	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigCreate(RegressionInferenceOptions regressionInferenceOptions) => Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigCreate.Regression(regressionInferenceOptions);
	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate(RegressionInferenceOptions regressionInferenceOptions) => Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate.Regression(regressionInferenceOptions);
}

public sealed partial class RegressionInferenceOptionsDescriptor<TDocument> : SerializableDescriptor<RegressionInferenceOptionsDescriptor<TDocument>>
{
	internal RegressionInferenceOptionsDescriptor(Action<RegressionInferenceOptionsDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RegressionInferenceOptionsDescriptor() : base()
	{
	}

	private int? NumTopFeatureImportanceValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field? ResultsFieldValue { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	public RegressionInferenceOptionsDescriptor<TDocument> NumTopFeatureImportanceValues(int? numTopFeatureImportanceValues)
	{
		NumTopFeatureImportanceValuesValue = numTopFeatureImportanceValues;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public RegressionInferenceOptionsDescriptor<TDocument> ResultsField(Elastic.Clients.Elasticsearch.Serverless.Field? resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public RegressionInferenceOptionsDescriptor<TDocument> ResultsField<TValue>(Expression<Func<TDocument, TValue>> resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public RegressionInferenceOptionsDescriptor<TDocument> ResultsField(Expression<Func<TDocument, object>> resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (NumTopFeatureImportanceValuesValue.HasValue)
		{
			writer.WritePropertyName("num_top_feature_importance_values");
			writer.WriteNumberValue(NumTopFeatureImportanceValuesValue.Value);
		}

		if (ResultsFieldValue is not null)
		{
			writer.WritePropertyName("results_field");
			JsonSerializer.Serialize(writer, ResultsFieldValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class RegressionInferenceOptionsDescriptor : SerializableDescriptor<RegressionInferenceOptionsDescriptor>
{
	internal RegressionInferenceOptionsDescriptor(Action<RegressionInferenceOptionsDescriptor> configure) => configure.Invoke(this);

	public RegressionInferenceOptionsDescriptor() : base()
	{
	}

	private int? NumTopFeatureImportanceValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field? ResultsFieldValue { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	public RegressionInferenceOptionsDescriptor NumTopFeatureImportanceValues(int? numTopFeatureImportanceValues)
	{
		NumTopFeatureImportanceValuesValue = numTopFeatureImportanceValues;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public RegressionInferenceOptionsDescriptor ResultsField(Elastic.Clients.Elasticsearch.Serverless.Field? resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public RegressionInferenceOptionsDescriptor ResultsField<TDocument, TValue>(Expression<Func<TDocument, TValue>> resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public RegressionInferenceOptionsDescriptor ResultsField<TDocument>(Expression<Func<TDocument, object>> resultsField)
	{
		ResultsFieldValue = resultsField;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (NumTopFeatureImportanceValuesValue.HasValue)
		{
			writer.WritePropertyName("num_top_feature_importance_values");
			writer.WriteNumberValue(NumTopFeatureImportanceValuesValue.Value);
		}

		if (ResultsFieldValue is not null)
		{
			writer.WritePropertyName("results_field");
			JsonSerializer.Serialize(writer, ResultsFieldValue, options);
		}

		writer.WriteEndObject();
	}
}