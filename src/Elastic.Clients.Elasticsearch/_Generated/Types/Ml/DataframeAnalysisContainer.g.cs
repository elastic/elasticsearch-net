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
	public interface IDataframeAnalysisVariant
	{
	}

	[JsonConverter(typeof(DataframeAnalysisContainerConverter))]
	public sealed partial class DataframeAnalysisContainer
	{
		internal DataframeAnalysisContainer(string variantName, IDataframeAnalysisVariant variant)
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

		internal IDataframeAnalysisVariant Variant { get; }

		internal string VariantName { get; }

		public static DataframeAnalysisContainer Classification(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisClassification dataframeAnalysisClassification) => new DataframeAnalysisContainer("classification", dataframeAnalysisClassification);
		public static DataframeAnalysisContainer OutlierDetection(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisOutlierDetection dataframeAnalysisOutlierDetection) => new DataframeAnalysisContainer("outlier_detection", dataframeAnalysisOutlierDetection);
		public static DataframeAnalysisContainer Regression(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisRegression dataframeAnalysisRegression) => new DataframeAnalysisContainer("regression", dataframeAnalysisRegression);
	}

	internal sealed class DataframeAnalysisContainerConverter : JsonConverter<DataframeAnalysisContainer>
	{
		public override DataframeAnalysisContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisClassification?>(ref reader, options);
				reader.Read();
				return new DataframeAnalysisContainer(propertyName, variant);
			}

			if (propertyName == "outlier_detection")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisOutlierDetection?>(ref reader, options);
				reader.Read();
				return new DataframeAnalysisContainer(propertyName, variant);
			}

			if (propertyName == "regression")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisRegression?>(ref reader, options);
				reader.Read();
				return new DataframeAnalysisContainer(propertyName, variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, DataframeAnalysisContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "classification":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisClassification>(writer, (Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisClassification)value.Variant, options);
					break;
				case "outlier_detection":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisOutlierDetection>(writer, (Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisOutlierDetection)value.Variant, options);
					break;
				case "regression":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisRegression>(writer, (Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisRegression)value.Variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class DataframeAnalysisContainerDescriptor<TDocument> : SerializableDescriptorBase<DataframeAnalysisContainerDescriptor<TDocument>>
	{
		internal DataframeAnalysisContainerDescriptor(Action<DataframeAnalysisContainerDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DataframeAnalysisContainerDescriptor() : base()
		{
		}

		private bool ContainsVariant { get; set; }

		private string ContainedVariantName { get; set; }

		private object Variant { get; set; }

		private Descriptor Descriptor { get; set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the DataframeAnalysisContainerDescriptor. Only a single DataframeAnalysisContainer variant can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IDataframeAnalysisVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the DataframeAnalysisContainerDescriptor. Only a single DataframeAnalysisContainer variant can be added to this container type.");
			Variant = variant;
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Classification(DataframeAnalysisClassification variant) => Set(variant, "classification");
		public void Classification(Action<DataframeAnalysisClassificationDescriptor<TDocument>> configure) => Set(configure, "classification");
		public void OutlierDetection(DataframeAnalysisOutlierDetection variant) => Set(variant, "outlier_detection");
		public void OutlierDetection(Action<DataframeAnalysisOutlierDetectionDescriptor> configure) => Set(configure, "outlier_detection");
		public void Regression(DataframeAnalysisRegression variant) => Set(variant, "regression");
		public void Regression(Action<DataframeAnalysisRegressionDescriptor<TDocument>> configure) => Set(configure, "regression");
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
			}
			else
			{
				JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class DataframeAnalysisContainerDescriptor : SerializableDescriptorBase<DataframeAnalysisContainerDescriptor>
	{
		internal DataframeAnalysisContainerDescriptor(Action<DataframeAnalysisContainerDescriptor> configure) => configure.Invoke(this);
		public DataframeAnalysisContainerDescriptor() : base()
		{
		}

		private bool ContainsVariant { get; set; }

		private string ContainedVariantName { get; set; }

		private object Variant { get; set; }

		private Descriptor Descriptor { get; set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the DataframeAnalysisContainerDescriptor. Only a single DataframeAnalysisContainer variant can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IDataframeAnalysisVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the DataframeAnalysisContainerDescriptor. Only a single DataframeAnalysisContainer variant can be added to this container type.");
			Variant = variant;
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Classification(DataframeAnalysisClassification variant) => Set(variant, "classification");
		public void Classification(Action<DataframeAnalysisClassificationDescriptor> configure) => Set(configure, "classification");
		public void Classification<TDocument>(Action<DataframeAnalysisClassificationDescriptor<TDocument>> configure) => Set(configure, "classification");
		public void OutlierDetection(DataframeAnalysisOutlierDetection variant) => Set(variant, "outlier_detection");
		public void OutlierDetection(Action<DataframeAnalysisOutlierDetectionDescriptor> configure) => Set(configure, "outlier_detection");
		public void Regression(DataframeAnalysisRegression variant) => Set(variant, "regression");
		public void Regression(Action<DataframeAnalysisRegressionDescriptor> configure) => Set(configure, "regression");
		public void Regression<TDocument>(Action<DataframeAnalysisRegressionDescriptor<TDocument>> configure) => Set(configure, "regression");
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
			}
			else
			{
				JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
			}

			writer.WriteEndObject();
		}
	}
}