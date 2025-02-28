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
		VariantType = variantName;
		Variant = variant;
	}

	internal DataframeEvaluation()
	{
	}

	public object Variant { get; internal set; }
	public string VariantType { get; internal set; }

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

internal sealed partial class DataframeEvaluationConverter : System.Text.Json.Serialization.JsonConverter<DataframeEvaluation>
{
	private static readonly System.Text.Json.JsonEncodedText VariantClassification = System.Text.Json.JsonEncodedText.Encode("classification");
	private static readonly System.Text.Json.JsonEncodedText VariantOutlierDetection = System.Text.Json.JsonEncodedText.Encode("outlier_detection");
	private static readonly System.Text.Json.JsonEncodedText VariantRegression = System.Text.Json.JsonEncodedText.Encode("regression");

	public override DataframeEvaluation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		var variantType = string.Empty;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(VariantClassification))
			{
				variantType = VariantClassification.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassification?>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantOutlierDetection))
			{
				variantType = VariantOutlierDetection.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetection?>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantRegression))
			{
				variantType = VariantRegression.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegression?>(options, null);
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new DataframeEvaluation { VariantType = variantType, Variant = variant };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, DataframeEvaluation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case "":
				break;
			case "classification":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationClassification?)value.Variant, null, null);
				break;
			case "outlier_detection":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationOutlierDetection?)value.Variant, null, null);
				break;
			case "regression":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegression?)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(DataframeEvaluation)}'.");
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