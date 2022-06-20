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
		string InferenceConfigVariantName { get; }
	}

	[JsonConverter(typeof(InferenceConfigContainerConverter))]
	public partial class InferenceConfigContainer : IContainer
	{
		public InferenceConfigContainer(IInferenceConfigVariant variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
		internal IInferenceConfigVariant Variant { get; }

		internal string VariantName => Variant.InferenceConfigVariantName;
	}

	internal sealed class InferenceConfigContainerConverter : JsonConverter<InferenceConfigContainer>
	{
		public override InferenceConfigContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var readerCopy = reader;
			readerCopy.Read();
			if (readerCopy.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException();
			}

			var propertyName = readerCopy.GetString();
			if (propertyName == "classification")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.ClassificationInferenceOptions?>(ref reader, options);
				return new InferenceConfigContainer(variant);
			}

			if (propertyName == "regression")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.RegressionInferenceOptions?>(ref reader, options);
				return new InferenceConfigContainer(variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, InferenceConfigContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.Variant.InferenceConfigVariantName);
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
			where T : Descriptor, new()
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = new T();
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IInferenceConfigVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new InferenceConfigContainer(variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

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

		public void Classification(Ml.ClassificationInferenceOptions variant) => Set(variant, "classification");
		public void Classification(Action<Ml.ClassificationInferenceOptionsDescriptor> configure) => Set(configure, "classification");
		public void Regression(Ml.RegressionInferenceOptions variant) => Set(variant, "regression");
		public void Regression(Action<Ml.RegressionInferenceOptionsDescriptor<TDocument>> configure) => Set(configure, "regression");
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
			where T : Descriptor, new()
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = new T();
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IInferenceConfigVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new InferenceConfigContainer(variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

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

		public void Classification(Ml.ClassificationInferenceOptions variant) => Set(variant, "classification");
		public void Classification(Action<Ml.ClassificationInferenceOptionsDescriptor> configure) => Set(configure, "classification");
		public void Regression(Ml.RegressionInferenceOptions variant) => Set(variant, "regression");
		public void Regression(Action<Ml.RegressionInferenceOptionsDescriptor> configure) => Set(configure, "regression");
		public void Regression<TDocument>(Action<Ml.RegressionInferenceOptionsDescriptor<TDocument>> configure) => Set(configure, "regression");
	}
}