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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

[JsonConverter(typeof(DataframeEvaluationConverter))]
public sealed partial class DataframeEvaluation
{
	internal DataframeEvaluation(string variantName, object variant)
	{
		if (variantName is null)
			throw new ArgumentNullException(nameof(variantName));
		if (variant is null)
			throw new ArgumentNullException(nameof(variant));
		if (string.IsNullOrWhiteSpace(variantName))
			throw new ArgumentException("Variant name must not be empty or whitespace.");
		VariantName = variantName;
		Variant = variant;
	}

	internal object Variant { get; }
	internal string VariantName { get; }

	public static DataframeEvaluation Classification(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassification dataframeEvaluationClassification) => new DataframeEvaluation("classification", dataframeEvaluationClassification);
	public static DataframeEvaluation OutlierDetection(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetection dataframeEvaluationOutlierDetection) => new DataframeEvaluation("outlier_detection", dataframeEvaluationOutlierDetection);
	public static DataframeEvaluation Regression(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegression dataframeEvaluationRegression) => new DataframeEvaluation("regression", dataframeEvaluationRegression);

	public bool TryGet<T>([NotNullWhen(true)] out T? result) where T : class
	{
		result = default;
		if (Variant is T variant)
		{
			result = variant;
			return true;
		}

		return false;
	}
}

internal sealed partial class DataframeEvaluationConverter : JsonConverter<DataframeEvaluation>
{
	public override DataframeEvaluation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException("Expected start token.");
		}

		object? variantValue = default;
		string? variantNameValue = default;
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException("Expected a property name token.");
			}

			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException("Expected a property name token representing the name of an Elasticsearch field.");
			}

			var propertyName = reader.GetString();
			reader.Read();
			if (propertyName == "classification")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassification?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "outlier_detection")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetection?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "regression")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegression?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			throw new JsonException($"Unknown property name '{propertyName}' received while deserializing the 'DataframeEvaluation' from the response.");
		}

		var result = new DataframeEvaluation(variantNameValue, variantValue);
		return result;
	}

	public override void Write(Utf8JsonWriter writer, DataframeEvaluation value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		if (value.VariantName is not null && value.Variant is not null)
		{
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "classification":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassification>(writer, (Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassification)value.Variant, options);
					break;
				case "outlier_detection":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetection>(writer, (Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetection)value.Variant, options);
					break;
				case "regression":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegression>(writer, (Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegression)value.Variant, options);
					break;
			}
		}

		writer.WriteEndObject();
	}
}

public sealed partial class DataframeEvaluationDescriptor<TDocument> : SerializableDescriptor<DataframeEvaluationDescriptor<TDocument>>
{
	internal DataframeEvaluationDescriptor(Action<DataframeEvaluationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DataframeEvaluationDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private DataframeEvaluationDescriptor<TDocument> Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private DataframeEvaluationDescriptor<TDocument> Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public DataframeEvaluationDescriptor<TDocument> Classification(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassification dataframeEvaluationClassification) => Set(dataframeEvaluationClassification, "classification");
	public DataframeEvaluationDescriptor<TDocument> Classification(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationDescriptor<TDocument>> configure) => Set(configure, "classification");
	public DataframeEvaluationDescriptor<TDocument> OutlierDetection(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetection dataframeEvaluationOutlierDetection) => Set(dataframeEvaluationOutlierDetection, "outlier_detection");
	public DataframeEvaluationDescriptor<TDocument> OutlierDetection(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionDescriptor<TDocument>> configure) => Set(configure, "outlier_detection");
	public DataframeEvaluationDescriptor<TDocument> Regression(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegression dataframeEvaluationRegression) => Set(dataframeEvaluationRegression, "regression");
	public DataframeEvaluationDescriptor<TDocument> Regression(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionDescriptor<TDocument>> configure) => Set(configure, "regression");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ContainedVariantName))
		{
			writer.WritePropertyName(ContainedVariantName);
			if (Variant is not null)
			{
				JsonSerializer.Serialize(writer, Variant, Variant.GetType(), options);
				writer.WriteEndObject();
				return;
			}

			JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class DataframeEvaluationDescriptor : SerializableDescriptor<DataframeEvaluationDescriptor>
{
	internal DataframeEvaluationDescriptor(Action<DataframeEvaluationDescriptor> configure) => configure.Invoke(this);

	public DataframeEvaluationDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private DataframeEvaluationDescriptor Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private DataframeEvaluationDescriptor Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public DataframeEvaluationDescriptor Classification(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassification dataframeEvaluationClassification) => Set(dataframeEvaluationClassification, "classification");
	public DataframeEvaluationDescriptor Classification<TDocument>(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassificationDescriptor> configure) => Set(configure, "classification");
	public DataframeEvaluationDescriptor OutlierDetection(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetection dataframeEvaluationOutlierDetection) => Set(dataframeEvaluationOutlierDetection, "outlier_detection");
	public DataframeEvaluationDescriptor OutlierDetection<TDocument>(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetectionDescriptor> configure) => Set(configure, "outlier_detection");
	public DataframeEvaluationDescriptor Regression(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegression dataframeEvaluationRegression) => Set(dataframeEvaluationRegression, "regression");
	public DataframeEvaluationDescriptor Regression<TDocument>(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionDescriptor> configure) => Set(configure, "regression");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ContainedVariantName))
		{
			writer.WritePropertyName(ContainedVariantName);
			if (Variant is not null)
			{
				JsonSerializer.Serialize(writer, Variant, Variant.GetType(), options);
				writer.WriteEndObject();
				return;
			}

			JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
		}

		writer.WriteEndObject();
	}
}