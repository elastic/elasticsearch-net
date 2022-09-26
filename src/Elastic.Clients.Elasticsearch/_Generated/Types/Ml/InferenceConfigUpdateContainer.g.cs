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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	public interface IInferenceConfigUpdateVariant
	{
	}

	[JsonConverter(typeof(InferenceConfigUpdateContainerConverter))]
	public sealed partial class InferenceConfigUpdateContainer
	{
		internal InferenceConfigUpdateContainer(string variantName, IInferenceConfigUpdateVariant variant)
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

		internal IInferenceConfigUpdateVariant Variant { get; }

		internal string VariantName { get; }

		public static InferenceConfigUpdateContainer Classification(Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions classificationInferenceOptions) => new InferenceConfigUpdateContainer("classification", classificationInferenceOptions);
		public static InferenceConfigUpdateContainer FillMask(Elastic.Clients.Elasticsearch.Ml.FillMaskInferenceUpdateOptions fillMaskInferenceUpdateOptions) => new InferenceConfigUpdateContainer("fill_mask", fillMaskInferenceUpdateOptions);
		public static InferenceConfigUpdateContainer Ner(Elastic.Clients.Elasticsearch.Ml.NerInferenceUpdateOptions nerInferenceUpdateOptions) => new InferenceConfigUpdateContainer("ner", nerInferenceUpdateOptions);
		public static InferenceConfigUpdateContainer PassThrough(Elastic.Clients.Elasticsearch.Ml.PassThroughInferenceUpdateOptions passThroughInferenceUpdateOptions) => new InferenceConfigUpdateContainer("pass_through", passThroughInferenceUpdateOptions);
		public static InferenceConfigUpdateContainer QuestionAnswering(Elastic.Clients.Elasticsearch.Ml.QuestionAnsweringInferenceUpdateOptions questionAnsweringInferenceUpdateOptions) => new InferenceConfigUpdateContainer("question_answering", questionAnsweringInferenceUpdateOptions);
		public static InferenceConfigUpdateContainer Regression(Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions regressionInferenceOptions) => new InferenceConfigUpdateContainer("regression", regressionInferenceOptions);
		public static InferenceConfigUpdateContainer TextClassification(Elastic.Clients.Elasticsearch.Ml.TextClassificationInferenceUpdateOptions textClassificationInferenceUpdateOptions) => new InferenceConfigUpdateContainer("text_classification", textClassificationInferenceUpdateOptions);
		public static InferenceConfigUpdateContainer TextEmbedding(Elastic.Clients.Elasticsearch.Ml.TextEmbeddingInferenceUpdateOptions textEmbeddingInferenceUpdateOptions) => new InferenceConfigUpdateContainer("text_embedding", textEmbeddingInferenceUpdateOptions);
		public static InferenceConfigUpdateContainer ZeroShotClassification(Elastic.Clients.Elasticsearch.Ml.ZeroShotClassificationInferenceUpdateOptions zeroShotClassificationInferenceUpdateOptions) => new InferenceConfigUpdateContainer("zero_shot_classification", zeroShotClassificationInferenceUpdateOptions);
	}

	internal sealed class InferenceConfigUpdateContainerConverter : JsonConverter<InferenceConfigUpdateContainer>
	{
		public override InferenceConfigUpdateContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
			{
				throw new JsonException("Expected start token.");
			}

			reader.Read();
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException("Expected property name token.");
			}

			var propertyName = reader.GetString();
			reader.Read();
			if (propertyName == "classification")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			if (propertyName == "fill_mask")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.FillMaskInferenceUpdateOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			if (propertyName == "ner")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.NerInferenceUpdateOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			if (propertyName == "pass_through")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.PassThroughInferenceUpdateOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			if (propertyName == "question_answering")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.QuestionAnsweringInferenceUpdateOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			if (propertyName == "regression")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			if (propertyName == "text_classification")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.TextClassificationInferenceUpdateOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			if (propertyName == "text_embedding")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.TextEmbeddingInferenceUpdateOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			if (propertyName == "zero_shot_classification")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.ZeroShotClassificationInferenceUpdateOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigUpdateContainer(propertyName, variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, InferenceConfigUpdateContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
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

			writer.WriteEndObject();
		}
	}

	public sealed partial class InferenceConfigUpdateContainerDescriptor<TDocument> : SerializableDescriptorBase<InferenceConfigUpdateContainerDescriptor<TDocument>>
	{
		internal InferenceConfigUpdateContainerDescriptor(Action<InferenceConfigUpdateContainerDescriptor<TDocument>> configure) => configure.Invoke(this);
		public InferenceConfigUpdateContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal InferenceConfigUpdateContainer Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the InferenceConfigUpdateContainerDescriptor. Only a single InferenceConfigUpdateContainer can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IInferenceConfigUpdateVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the InferenceConfigUpdateContainerDescriptor. Only a single InferenceConfigUpdateContainer can be added to this container type.");
			Container = new InferenceConfigUpdateContainer(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Classification(ClassificationInferenceOptions variant) => Set(variant, "classification");
		public void Classification(Action<ClassificationInferenceOptionsDescriptor> configure) => Set(configure, "classification");
		public void FillMask(FillMaskInferenceUpdateOptions variant) => Set(variant, "fill_mask");
		public void FillMask(Action<FillMaskInferenceUpdateOptionsDescriptor> configure) => Set(configure, "fill_mask");
		public void Ner(NerInferenceUpdateOptions variant) => Set(variant, "ner");
		public void Ner(Action<NerInferenceUpdateOptionsDescriptor> configure) => Set(configure, "ner");
		public void PassThrough(PassThroughInferenceUpdateOptions variant) => Set(variant, "pass_through");
		public void PassThrough(Action<PassThroughInferenceUpdateOptionsDescriptor> configure) => Set(configure, "pass_through");
		public void QuestionAnswering(QuestionAnsweringInferenceUpdateOptions variant) => Set(variant, "question_answering");
		public void QuestionAnswering(Action<QuestionAnsweringInferenceUpdateOptionsDescriptor> configure) => Set(configure, "question_answering");
		public void Regression(RegressionInferenceOptions variant) => Set(variant, "regression");
		public void Regression(Action<RegressionInferenceOptionsDescriptor<TDocument>> configure) => Set(configure, "regression");
		public void TextClassification(TextClassificationInferenceUpdateOptions variant) => Set(variant, "text_classification");
		public void TextClassification(Action<TextClassificationInferenceUpdateOptionsDescriptor> configure) => Set(configure, "text_classification");
		public void TextEmbedding(TextEmbeddingInferenceUpdateOptions variant) => Set(variant, "text_embedding");
		public void TextEmbedding(Action<TextEmbeddingInferenceUpdateOptionsDescriptor> configure) => Set(configure, "text_embedding");
		public void ZeroShotClassification(ZeroShotClassificationInferenceUpdateOptions variant) => Set(variant, "zero_shot_classification");
		public void ZeroShotClassification(Action<ZeroShotClassificationInferenceUpdateOptionsDescriptor> configure) => Set(configure, "zero_shot_classification");
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (!ContainsVariant)
			{
				writer.WriteNullValue();
				return;
			}

			if (Container is not null)
			{
				JsonSerializer.Serialize(writer, Container, options);
				return;
			}

			writer.WriteStartObject();
			writer.WritePropertyName(ContainedVariantName);
			JsonSerializer.Serialize(writer, Descriptor, DescriptorType, options);
			writer.WriteEndObject();
		}
	}

	public sealed partial class InferenceConfigUpdateContainerDescriptor : SerializableDescriptorBase<InferenceConfigUpdateContainerDescriptor>
	{
		internal InferenceConfigUpdateContainerDescriptor(Action<InferenceConfigUpdateContainerDescriptor> configure) => configure.Invoke(this);
		public InferenceConfigUpdateContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal InferenceConfigUpdateContainer Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the InferenceConfigUpdateContainerDescriptor. Only a single InferenceConfigUpdateContainer can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IInferenceConfigUpdateVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the InferenceConfigUpdateContainerDescriptor. Only a single InferenceConfigUpdateContainer can be added to this container type.");
			Container = new InferenceConfigUpdateContainer(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Classification(ClassificationInferenceOptions variant) => Set(variant, "classification");
		public void Classification(Action<ClassificationInferenceOptionsDescriptor> configure) => Set(configure, "classification");
		public void FillMask(FillMaskInferenceUpdateOptions variant) => Set(variant, "fill_mask");
		public void FillMask(Action<FillMaskInferenceUpdateOptionsDescriptor> configure) => Set(configure, "fill_mask");
		public void Ner(NerInferenceUpdateOptions variant) => Set(variant, "ner");
		public void Ner(Action<NerInferenceUpdateOptionsDescriptor> configure) => Set(configure, "ner");
		public void PassThrough(PassThroughInferenceUpdateOptions variant) => Set(variant, "pass_through");
		public void PassThrough(Action<PassThroughInferenceUpdateOptionsDescriptor> configure) => Set(configure, "pass_through");
		public void QuestionAnswering(QuestionAnsweringInferenceUpdateOptions variant) => Set(variant, "question_answering");
		public void QuestionAnswering(Action<QuestionAnsweringInferenceUpdateOptionsDescriptor> configure) => Set(configure, "question_answering");
		public void Regression(RegressionInferenceOptions variant) => Set(variant, "regression");
		public void Regression(Action<RegressionInferenceOptionsDescriptor> configure) => Set(configure, "regression");
		public void Regression<TDocument>(Action<RegressionInferenceOptionsDescriptor<TDocument>> configure) => Set(configure, "regression");
		public void TextClassification(TextClassificationInferenceUpdateOptions variant) => Set(variant, "text_classification");
		public void TextClassification(Action<TextClassificationInferenceUpdateOptionsDescriptor> configure) => Set(configure, "text_classification");
		public void TextEmbedding(TextEmbeddingInferenceUpdateOptions variant) => Set(variant, "text_embedding");
		public void TextEmbedding(Action<TextEmbeddingInferenceUpdateOptionsDescriptor> configure) => Set(configure, "text_embedding");
		public void ZeroShotClassification(ZeroShotClassificationInferenceUpdateOptions variant) => Set(variant, "zero_shot_classification");
		public void ZeroShotClassification(Action<ZeroShotClassificationInferenceUpdateOptionsDescriptor> configure) => Set(configure, "zero_shot_classification");
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (!ContainsVariant)
			{
				writer.WriteNullValue();
				return;
			}

			if (Container is not null)
			{
				JsonSerializer.Serialize(writer, Container, options);
				return;
			}

			writer.WriteStartObject();
			writer.WritePropertyName(ContainedVariantName);
			JsonSerializer.Serialize(writer, Descriptor, DescriptorType, options);
			writer.WriteEndObject();
		}
	}
}