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
namespace Elastic.Clients.Elasticsearch.Ingest
{
	public interface IInferenceConfigVariant
	{
	}

	[JsonConverter(typeof(InferenceConfigConverter))]
	public sealed partial class InferenceConfig
	{
		internal InferenceConfig(string variantName, IInferenceConfigVariant variant)
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

		public static InferenceConfig Classification(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification inferenceConfigClassification) => new InferenceConfig("classification", inferenceConfigClassification);
		public static InferenceConfig Regression(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression inferenceConfigRegression) => new InferenceConfig("regression", inferenceConfigRegression);
	}

	internal sealed class InferenceConfigConverter : JsonConverter<InferenceConfig>
	{
		public override InferenceConfig Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification?>(ref reader, options);
				reader.Read();
				return new InferenceConfig(propertyName, variant);
			}

			if (propertyName == "regression")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression?>(ref reader, options);
				reader.Read();
				return new InferenceConfig(propertyName, variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, InferenceConfig value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "classification":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification>(writer, (Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification)value.Variant, options);
					break;
				case "regression":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression>(writer, (Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression)value.Variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class InferenceConfigDescriptor<TDocument> : SerializableDescriptorBase<InferenceConfigDescriptor<TDocument>>
	{
		internal InferenceConfigDescriptor(Action<InferenceConfigDescriptor<TDocument>> configure) => configure.Invoke(this);
		public InferenceConfigDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal InferenceConfig Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the InferenceConfigDescriptor. Only a single InferenceConfig can be added to this container type.");
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
				throw new Exception("A variant has already been assigned to the InferenceConfigDescriptor. Only a single InferenceConfig can be added to this container type.");
			Container = new InferenceConfig(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Classification(InferenceConfigClassification variant) => Set(variant, "classification");
		public void Classification(Action<InferenceConfigClassificationDescriptor<TDocument>> configure) => Set(configure, "classification");
		public void Regression(InferenceConfigRegression variant) => Set(variant, "regression");
		public void Regression(Action<InferenceConfigRegressionDescriptor<TDocument>> configure) => Set(configure, "regression");
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

	public sealed partial class InferenceConfigDescriptor : SerializableDescriptorBase<InferenceConfigDescriptor>
	{
		internal InferenceConfigDescriptor(Action<InferenceConfigDescriptor> configure) => configure.Invoke(this);
		public InferenceConfigDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal InferenceConfig Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the InferenceConfigDescriptor. Only a single InferenceConfig can be added to this container type.");
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
				throw new Exception("A variant has already been assigned to the InferenceConfigDescriptor. Only a single InferenceConfig can be added to this container type.");
			Container = new InferenceConfig(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Classification(InferenceConfigClassification variant) => Set(variant, "classification");
		public void Classification(Action<InferenceConfigClassificationDescriptor> configure) => Set(configure, "classification");
		public void Classification<TDocument>(Action<InferenceConfigClassificationDescriptor<TDocument>> configure) => Set(configure, "classification");
		public void Regression(InferenceConfigRegression variant) => Set(variant, "regression");
		public void Regression(Action<InferenceConfigRegressionDescriptor> configure) => Set(configure, "regression");
		public void Regression<TDocument>(Action<InferenceConfigRegressionDescriptor<TDocument>> configure) => Set(configure, "regression");
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