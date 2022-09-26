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
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public interface IInferenceConfigVariant
	{
	}

	[JsonConverter(typeof(InferenceConfigContainerConverter))]
	public sealed partial class InferenceConfigContainer
	{
		internal InferenceConfigContainer(string variantName, IInferenceConfigVariant variant)
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

		internal IInferenceConfigVariant Variant { get; }

		internal string VariantName { get; }

		public static InferenceConfigContainer Classification(Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions classificationInferenceOptions) => new InferenceConfigContainer("classification", classificationInferenceOptions);
		public static InferenceConfigContainer Regression(Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions regressionInferenceOptions) => new InferenceConfigContainer("regression", regressionInferenceOptions);
	}

	internal sealed class InferenceConfigContainerConverter : JsonConverter<InferenceConfigContainer>
	{
		public override InferenceConfigContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
				return new InferenceConfigContainer(propertyName, variant);
			}

			if (propertyName == "regression")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions?>(ref reader, options);
				reader.Read();
				return new InferenceConfigContainer(propertyName, variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, InferenceConfigContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "classification":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions)value.Variant, options);
					break;
				case "regression":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions>(writer, (Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions)value.Variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class InferenceConfigContainerDescriptor<TDocument> : SerializableDescriptorBase<InferenceConfigContainerDescriptor<TDocument>>
	{
		internal InferenceConfigContainerDescriptor(Action<InferenceConfigContainerDescriptor<TDocument>> configure) => configure.Invoke(this);
		public InferenceConfigContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal InferenceConfigContainer Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the InferenceConfigContainerDescriptor. Only a single InferenceConfigContainer can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IInferenceConfigVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the InferenceConfigContainerDescriptor. Only a single InferenceConfigContainer can be added to this container type.");
			Container = new InferenceConfigContainer(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Classification(Ml.ClassificationInferenceOptions variant) => Set(variant, "classification");
		public void Classification(Action<Ml.ClassificationInferenceOptionsDescriptor> configure) => Set(configure, "classification");
		public void Regression(Ml.RegressionInferenceOptions variant) => Set(variant, "regression");
		public void Regression(Action<Ml.RegressionInferenceOptionsDescriptor<TDocument>> configure) => Set(configure, "regression");
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

	public sealed partial class InferenceConfigContainerDescriptor : SerializableDescriptorBase<InferenceConfigContainerDescriptor>
	{
		internal InferenceConfigContainerDescriptor(Action<InferenceConfigContainerDescriptor> configure) => configure.Invoke(this);
		public InferenceConfigContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal InferenceConfigContainer Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the InferenceConfigContainerDescriptor. Only a single InferenceConfigContainer can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IInferenceConfigVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the InferenceConfigContainerDescriptor. Only a single InferenceConfigContainer can be added to this container type.");
			Container = new InferenceConfigContainer(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Classification(Ml.ClassificationInferenceOptions variant) => Set(variant, "classification");
		public void Classification(Action<Ml.ClassificationInferenceOptionsDescriptor> configure) => Set(configure, "classification");
		public void Regression(Ml.RegressionInferenceOptions variant) => Set(variant, "regression");
		public void Regression(Action<Ml.RegressionInferenceOptionsDescriptor> configure) => Set(configure, "regression");
		public void Regression<TDocument>(Action<Ml.RegressionInferenceOptionsDescriptor<TDocument>> configure) => Set(configure, "regression");
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