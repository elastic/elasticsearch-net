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

namespace Elastic.Clients.Elasticsearch.Ml;

[JsonConverter(typeof(InferenceConfigUpdateConverter))]
public sealed partial class InferenceConfigUpdate
{
	internal InferenceConfigUpdate(string variantName, object variant)
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

	public static InferenceConfigUpdate Classification(Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions classificationInferenceOptions) => new InferenceConfigUpdate("classification", classificationInferenceOptions);
	public static InferenceConfigUpdate FillMask(Elastic.Clients.Elasticsearch.Ml.FillMaskInferenceUpdateOptions fillMaskInferenceUpdateOptions) => new InferenceConfigUpdate("fill_mask", fillMaskInferenceUpdateOptions);
	public static InferenceConfigUpdate Ner(Elastic.Clients.Elasticsearch.Ml.NerInferenceUpdateOptions nerInferenceUpdateOptions) => new InferenceConfigUpdate("ner", nerInferenceUpdateOptions);
	public static InferenceConfigUpdate PassThrough(Elastic.Clients.Elasticsearch.Ml.PassThroughInferenceUpdateOptions passThroughInferenceUpdateOptions) => new InferenceConfigUpdate("pass_through", passThroughInferenceUpdateOptions);
	public static InferenceConfigUpdate QuestionAnswering(Elastic.Clients.Elasticsearch.Ml.QuestionAnsweringInferenceUpdateOptions questionAnsweringInferenceUpdateOptions) => new InferenceConfigUpdate("question_answering", questionAnsweringInferenceUpdateOptions);
	public static InferenceConfigUpdate Regression(Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions regressionInferenceOptions) => new InferenceConfigUpdate("regression", regressionInferenceOptions);
	public static InferenceConfigUpdate TextClassification(Elastic.Clients.Elasticsearch.Ml.TextClassificationInferenceUpdateOptions textClassificationInferenceUpdateOptions) => new InferenceConfigUpdate("text_classification", textClassificationInferenceUpdateOptions);
	public static InferenceConfigUpdate TextEmbedding(Elastic.Clients.Elasticsearch.Ml.TextEmbeddingInferenceUpdateOptions textEmbeddingInferenceUpdateOptions) => new InferenceConfigUpdate("text_embedding", textEmbeddingInferenceUpdateOptions);
	public static InferenceConfigUpdate ZeroShotClassification(Elastic.Clients.Elasticsearch.Ml.ZeroShotClassificationInferenceUpdateOptions zeroShotClassificationInferenceUpdateOptions) => new InferenceConfigUpdate("zero_shot_classification", zeroShotClassificationInferenceUpdateOptions);
}

internal sealed partial class InferenceConfigUpdateConverter : JsonConverter<InferenceConfigUpdate>
{
	public override InferenceConfigUpdate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException("Expected start token.");
		}

		reader.Read();
		if (reader.TokenType != JsonTokenType.PropertyName)
		{
			throw new JsonException("Expected a property name token representing the variant held within this container.");
		}

		var propertyName = reader.GetString();
		reader.Read();
		if (propertyName == "classification")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		if (propertyName == "fill_mask")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.FillMaskInferenceUpdateOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		if (propertyName == "ner")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.NerInferenceUpdateOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		if (propertyName == "pass_through")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.PassThroughInferenceUpdateOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		if (propertyName == "question_answering")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.QuestionAnsweringInferenceUpdateOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		if (propertyName == "regression")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		if (propertyName == "text_classification")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.TextClassificationInferenceUpdateOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		if (propertyName == "text_embedding")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.TextEmbeddingInferenceUpdateOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		if (propertyName == "zero_shot_classification")
		{
			var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.ZeroShotClassificationInferenceUpdateOptions?>(ref reader, options);
			reader.Read();
			return new InferenceConfigUpdate(propertyName, variant);
		}

		throw new JsonException();
	}

	public override void Write(Utf8JsonWriter writer, InferenceConfigUpdate value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		if (value.VariantName is not null & value.Variant is not null)
		{
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "classification":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions)value.Variant, options);
					break;
				case "fill_mask":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.FillMaskInferenceUpdateOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.FillMaskInferenceUpdateOptions)value.Variant, options);
					break;
				case "ner":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.NerInferenceUpdateOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.NerInferenceUpdateOptions)value.Variant, options);
					break;
				case "pass_through":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.PassThroughInferenceUpdateOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.PassThroughInferenceUpdateOptions)value.Variant, options);
					break;
				case "question_answering":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.QuestionAnsweringInferenceUpdateOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.QuestionAnsweringInferenceUpdateOptions)value.Variant, options);
					break;
				case "regression":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions)value.Variant, options);
					break;
				case "text_classification":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.TextClassificationInferenceUpdateOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.TextClassificationInferenceUpdateOptions)value.Variant, options);
					break;
				case "text_embedding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.TextEmbeddingInferenceUpdateOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.TextEmbeddingInferenceUpdateOptions)value.Variant, options);
					break;
				case "zero_shot_classification":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.ZeroShotClassificationInferenceUpdateOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.ZeroShotClassificationInferenceUpdateOptions)value.Variant, options);
					break;
			}
		}

		writer.WriteEndObject();
	}
}

public sealed partial class InferenceConfigUpdateDescriptor<TDocument> : SerializableDescriptor<InferenceConfigUpdateDescriptor<TDocument>>
{
	internal InferenceConfigUpdateDescriptor(Action<InferenceConfigUpdateDescriptor<TDocument>> configure) => configure.Invoke(this);

	public InferenceConfigUpdateDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private InferenceConfigUpdateDescriptor<TDocument> Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private InferenceConfigUpdateDescriptor<TDocument> Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public InferenceConfigUpdateDescriptor<TDocument> Classification(ClassificationInferenceOptions classificationInferenceOptions) => Set(classificationInferenceOptions, "classification");
	public InferenceConfigUpdateDescriptor<TDocument> Classification(Action<ClassificationInferenceOptionsDescriptor> configure) => Set(configure, "classification");
	public InferenceConfigUpdateDescriptor<TDocument> FillMask(FillMaskInferenceUpdateOptions fillMaskInferenceUpdateOptions) => Set(fillMaskInferenceUpdateOptions, "fill_mask");
	public InferenceConfigUpdateDescriptor<TDocument> FillMask(Action<FillMaskInferenceUpdateOptionsDescriptor> configure) => Set(configure, "fill_mask");
	public InferenceConfigUpdateDescriptor<TDocument> Ner(NerInferenceUpdateOptions nerInferenceUpdateOptions) => Set(nerInferenceUpdateOptions, "ner");
	public InferenceConfigUpdateDescriptor<TDocument> Ner(Action<NerInferenceUpdateOptionsDescriptor> configure) => Set(configure, "ner");
	public InferenceConfigUpdateDescriptor<TDocument> PassThrough(PassThroughInferenceUpdateOptions passThroughInferenceUpdateOptions) => Set(passThroughInferenceUpdateOptions, "pass_through");
	public InferenceConfigUpdateDescriptor<TDocument> PassThrough(Action<PassThroughInferenceUpdateOptionsDescriptor> configure) => Set(configure, "pass_through");
	public InferenceConfigUpdateDescriptor<TDocument> QuestionAnswering(QuestionAnsweringInferenceUpdateOptions questionAnsweringInferenceUpdateOptions) => Set(questionAnsweringInferenceUpdateOptions, "question_answering");
	public InferenceConfigUpdateDescriptor<TDocument> QuestionAnswering(Action<QuestionAnsweringInferenceUpdateOptionsDescriptor> configure) => Set(configure, "question_answering");
	public InferenceConfigUpdateDescriptor<TDocument> Regression(RegressionInferenceOptions regressionInferenceOptions) => Set(regressionInferenceOptions, "regression");
	public InferenceConfigUpdateDescriptor<TDocument> Regression(Action<RegressionInferenceOptionsDescriptor<TDocument>> configure) => Set(configure, "regression");
	public InferenceConfigUpdateDescriptor<TDocument> TextClassification(TextClassificationInferenceUpdateOptions textClassificationInferenceUpdateOptions) => Set(textClassificationInferenceUpdateOptions, "text_classification");
	public InferenceConfigUpdateDescriptor<TDocument> TextClassification(Action<TextClassificationInferenceUpdateOptionsDescriptor> configure) => Set(configure, "text_classification");
	public InferenceConfigUpdateDescriptor<TDocument> TextEmbedding(TextEmbeddingInferenceUpdateOptions textEmbeddingInferenceUpdateOptions) => Set(textEmbeddingInferenceUpdateOptions, "text_embedding");
	public InferenceConfigUpdateDescriptor<TDocument> TextEmbedding(Action<TextEmbeddingInferenceUpdateOptionsDescriptor> configure) => Set(configure, "text_embedding");
	public InferenceConfigUpdateDescriptor<TDocument> ZeroShotClassification(ZeroShotClassificationInferenceUpdateOptions zeroShotClassificationInferenceUpdateOptions) => Set(zeroShotClassificationInferenceUpdateOptions, "zero_shot_classification");
	public InferenceConfigUpdateDescriptor<TDocument> ZeroShotClassification(Action<ZeroShotClassificationInferenceUpdateOptionsDescriptor> configure) => Set(configure, "zero_shot_classification");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (!ContainsVariant)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStartObject();
		writer.WritePropertyName(ContainedVariantName);
		if (Variant is not null)
		{
			JsonSerializer.Serialize(writer, Variant, Variant.GetType(), options);
			writer.WriteEndObject();
			return;
		}

		JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
		writer.WriteEndObject();
	}
}

public sealed partial class InferenceConfigUpdateDescriptor : SerializableDescriptor<InferenceConfigUpdateDescriptor>
{
	internal InferenceConfigUpdateDescriptor(Action<InferenceConfigUpdateDescriptor> configure) => configure.Invoke(this);

	public InferenceConfigUpdateDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private InferenceConfigUpdateDescriptor Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private InferenceConfigUpdateDescriptor Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public InferenceConfigUpdateDescriptor Classification(ClassificationInferenceOptions classificationInferenceOptions) => Set(classificationInferenceOptions, "classification");
	public InferenceConfigUpdateDescriptor Classification(Action<ClassificationInferenceOptionsDescriptor> configure) => Set(configure, "classification");
	public InferenceConfigUpdateDescriptor FillMask(FillMaskInferenceUpdateOptions fillMaskInferenceUpdateOptions) => Set(fillMaskInferenceUpdateOptions, "fill_mask");
	public InferenceConfigUpdateDescriptor FillMask(Action<FillMaskInferenceUpdateOptionsDescriptor> configure) => Set(configure, "fill_mask");
	public InferenceConfigUpdateDescriptor Ner(NerInferenceUpdateOptions nerInferenceUpdateOptions) => Set(nerInferenceUpdateOptions, "ner");
	public InferenceConfigUpdateDescriptor Ner(Action<NerInferenceUpdateOptionsDescriptor> configure) => Set(configure, "ner");
	public InferenceConfigUpdateDescriptor PassThrough(PassThroughInferenceUpdateOptions passThroughInferenceUpdateOptions) => Set(passThroughInferenceUpdateOptions, "pass_through");
	public InferenceConfigUpdateDescriptor PassThrough(Action<PassThroughInferenceUpdateOptionsDescriptor> configure) => Set(configure, "pass_through");
	public InferenceConfigUpdateDescriptor QuestionAnswering(QuestionAnsweringInferenceUpdateOptions questionAnsweringInferenceUpdateOptions) => Set(questionAnsweringInferenceUpdateOptions, "question_answering");
	public InferenceConfigUpdateDescriptor QuestionAnswering(Action<QuestionAnsweringInferenceUpdateOptionsDescriptor> configure) => Set(configure, "question_answering");
	public InferenceConfigUpdateDescriptor Regression(RegressionInferenceOptions regressionInferenceOptions) => Set(regressionInferenceOptions, "regression");
	public InferenceConfigUpdateDescriptor Regression(Action<RegressionInferenceOptionsDescriptor> configure) => Set(configure, "regression");
	public InferenceConfigUpdateDescriptor Regression<TDocument>(Action<RegressionInferenceOptionsDescriptor<TDocument>> configure) => Set(configure, "regression");
	public InferenceConfigUpdateDescriptor TextClassification(TextClassificationInferenceUpdateOptions textClassificationInferenceUpdateOptions) => Set(textClassificationInferenceUpdateOptions, "text_classification");
	public InferenceConfigUpdateDescriptor TextClassification(Action<TextClassificationInferenceUpdateOptionsDescriptor> configure) => Set(configure, "text_classification");
	public InferenceConfigUpdateDescriptor TextEmbedding(TextEmbeddingInferenceUpdateOptions textEmbeddingInferenceUpdateOptions) => Set(textEmbeddingInferenceUpdateOptions, "text_embedding");
	public InferenceConfigUpdateDescriptor TextEmbedding(Action<TextEmbeddingInferenceUpdateOptionsDescriptor> configure) => Set(configure, "text_embedding");
	public InferenceConfigUpdateDescriptor ZeroShotClassification(ZeroShotClassificationInferenceUpdateOptions zeroShotClassificationInferenceUpdateOptions) => Set(zeroShotClassificationInferenceUpdateOptions, "zero_shot_classification");
	public InferenceConfigUpdateDescriptor ZeroShotClassification(Action<ZeroShotClassificationInferenceUpdateOptionsDescriptor> configure) => Set(configure, "zero_shot_classification");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (!ContainsVariant)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStartObject();
		writer.WritePropertyName(ContainedVariantName);
		if (Variant is not null)
		{
			JsonSerializer.Serialize(writer, Variant, Variant.GetType(), options);
			writer.WriteEndObject();
			return;
		}

		JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
		writer.WriteEndObject();
	}
}