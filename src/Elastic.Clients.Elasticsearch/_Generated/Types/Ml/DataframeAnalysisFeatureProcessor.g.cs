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
	public interface IDataframeAnalysisFeatureProcessorVariant
	{
	}

	[JsonConverter(typeof(DataframeAnalysisFeatureProcessorConverter))]
	public partial class DataframeAnalysisFeatureProcessor
	{
		public DataframeAnalysisFeatureProcessor(string variantName, IDataframeAnalysisFeatureProcessorVariant variant)
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

		internal IDataframeAnalysisFeatureProcessorVariant Variant { get; }

		internal string VariantName { get; }

		public static DataframeAnalysisFeatureProcessor FrequencyEncoding(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorFrequencyEncoding variant) => new DataframeAnalysisFeatureProcessor("frequency_encoding", variant);
		public static DataframeAnalysisFeatureProcessor MultiEncoding(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorMultiEncoding variant) => new DataframeAnalysisFeatureProcessor("multi_encoding", variant);
		public static DataframeAnalysisFeatureProcessor NGramEncoding(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorNGramEncoding variant) => new DataframeAnalysisFeatureProcessor("n_gram_encoding", variant);
		public static DataframeAnalysisFeatureProcessor OneHotEncoding(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorOneHotEncoding variant) => new DataframeAnalysisFeatureProcessor("one_hot_encoding", variant);
		public static DataframeAnalysisFeatureProcessor TargetMeanEncoding(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorTargetMeanEncoding variant) => new DataframeAnalysisFeatureProcessor("target_mean_encoding", variant);
	}

	internal sealed class DataframeAnalysisFeatureProcessorConverter : JsonConverter<DataframeAnalysisFeatureProcessor>
	{
		public override DataframeAnalysisFeatureProcessor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var readerCopy = reader;
			readerCopy.Read();
			if (readerCopy.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException();
			}

			var propertyName = readerCopy.GetString();
			if (propertyName == "frequency_encoding")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorFrequencyEncoding?>(ref reader, options);
				return new DataframeAnalysisFeatureProcessor(propertyName, variant);
			}

			if (propertyName == "multi_encoding")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorMultiEncoding?>(ref reader, options);
				return new DataframeAnalysisFeatureProcessor(propertyName, variant);
			}

			if (propertyName == "n_gram_encoding")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorNGramEncoding?>(ref reader, options);
				return new DataframeAnalysisFeatureProcessor(propertyName, variant);
			}

			if (propertyName == "one_hot_encoding")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorOneHotEncoding?>(ref reader, options);
				return new DataframeAnalysisFeatureProcessor(propertyName, variant);
			}

			if (propertyName == "target_mean_encoding")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorTargetMeanEncoding?>(ref reader, options);
				return new DataframeAnalysisFeatureProcessor(propertyName, variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, DataframeAnalysisFeatureProcessor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "frequency_encoding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorFrequencyEncoding>(writer, (Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorFrequencyEncoding)value.Variant, options);
					break;
				case "multi_encoding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorMultiEncoding>(writer, (Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorMultiEncoding)value.Variant, options);
					break;
				case "n_gram_encoding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorNGramEncoding>(writer, (Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorNGramEncoding)value.Variant, options);
					break;
				case "one_hot_encoding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorOneHotEncoding>(writer, (Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorOneHotEncoding)value.Variant, options);
					break;
				case "target_mean_encoding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorTargetMeanEncoding>(writer, (Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisFeatureProcessorTargetMeanEncoding)value.Variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class DataframeAnalysisFeatureProcessorDescriptor<TDocument> : SerializableDescriptorBase<DataframeAnalysisFeatureProcessorDescriptor<TDocument>>
	{
		internal DataframeAnalysisFeatureProcessorDescriptor(Action<DataframeAnalysisFeatureProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DataframeAnalysisFeatureProcessorDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal DataframeAnalysisFeatureProcessor Container { get; private set; }

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

		private void Set(IDataframeAnalysisFeatureProcessorVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new DataframeAnalysisFeatureProcessor(variantName, variant);
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

		public void FrequencyEncoding(DataframeAnalysisFeatureProcessorFrequencyEncoding variant) => Set(variant, "frequency_encoding");
		public void FrequencyEncoding(Action<DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument>> configure) => Set(configure, "frequency_encoding");
		public void MultiEncoding(DataframeAnalysisFeatureProcessorMultiEncoding variant) => Set(variant, "multi_encoding");
		public void MultiEncoding(Action<DataframeAnalysisFeatureProcessorMultiEncodingDescriptor> configure) => Set(configure, "multi_encoding");
		public void NGramEncoding(DataframeAnalysisFeatureProcessorNGramEncoding variant) => Set(variant, "n_gram_encoding");
		public void NGramEncoding(Action<DataframeAnalysisFeatureProcessorNGramEncodingDescriptor<TDocument>> configure) => Set(configure, "n_gram_encoding");
		public void OneHotEncoding(DataframeAnalysisFeatureProcessorOneHotEncoding variant) => Set(variant, "one_hot_encoding");
		public void OneHotEncoding(Action<DataframeAnalysisFeatureProcessorOneHotEncodingDescriptor<TDocument>> configure) => Set(configure, "one_hot_encoding");
		public void TargetMeanEncoding(DataframeAnalysisFeatureProcessorTargetMeanEncoding variant) => Set(variant, "target_mean_encoding");
		public void TargetMeanEncoding(Action<DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument>> configure) => Set(configure, "target_mean_encoding");
	}

	public sealed partial class DataframeAnalysisFeatureProcessorDescriptor : SerializableDescriptorBase<DataframeAnalysisFeatureProcessorDescriptor>
	{
		internal DataframeAnalysisFeatureProcessorDescriptor(Action<DataframeAnalysisFeatureProcessorDescriptor> configure) => configure.Invoke(this);
		public DataframeAnalysisFeatureProcessorDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal DataframeAnalysisFeatureProcessor Container { get; private set; }

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

		private void Set(IDataframeAnalysisFeatureProcessorVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new DataframeAnalysisFeatureProcessor(variantName, variant);
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

		public void FrequencyEncoding(DataframeAnalysisFeatureProcessorFrequencyEncoding variant) => Set(variant, "frequency_encoding");
		public void FrequencyEncoding(Action<DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor> configure) => Set(configure, "frequency_encoding");
		public void FrequencyEncoding<TDocument>(Action<DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument>> configure) => Set(configure, "frequency_encoding");
		public void MultiEncoding(DataframeAnalysisFeatureProcessorMultiEncoding variant) => Set(variant, "multi_encoding");
		public void MultiEncoding(Action<DataframeAnalysisFeatureProcessorMultiEncodingDescriptor> configure) => Set(configure, "multi_encoding");
		public void NGramEncoding(DataframeAnalysisFeatureProcessorNGramEncoding variant) => Set(variant, "n_gram_encoding");
		public void NGramEncoding(Action<DataframeAnalysisFeatureProcessorNGramEncodingDescriptor> configure) => Set(configure, "n_gram_encoding");
		public void NGramEncoding<TDocument>(Action<DataframeAnalysisFeatureProcessorNGramEncodingDescriptor<TDocument>> configure) => Set(configure, "n_gram_encoding");
		public void OneHotEncoding(DataframeAnalysisFeatureProcessorOneHotEncoding variant) => Set(variant, "one_hot_encoding");
		public void OneHotEncoding(Action<DataframeAnalysisFeatureProcessorOneHotEncodingDescriptor> configure) => Set(configure, "one_hot_encoding");
		public void OneHotEncoding<TDocument>(Action<DataframeAnalysisFeatureProcessorOneHotEncodingDescriptor<TDocument>> configure) => Set(configure, "one_hot_encoding");
		public void TargetMeanEncoding(DataframeAnalysisFeatureProcessorTargetMeanEncoding variant) => Set(variant, "target_mean_encoding");
		public void TargetMeanEncoding(Action<DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor> configure) => Set(configure, "target_mean_encoding");
		public void TargetMeanEncoding<TDocument>(Action<DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument>> configure) => Set(configure, "target_mean_encoding");
	}
}