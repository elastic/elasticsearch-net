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
	public partial class DataframeAnalysisFeatureProcessorFrequencyEncoding : IDataframeAnalysisFeatureProcessorVariant
	{
		[JsonIgnore]
		string IDataframeAnalysisFeatureProcessorVariant.DataframeAnalysisFeatureProcessorVariantName => "frequency_encoding";
		[JsonInclude]
		[JsonPropertyName("feature_name")]
		public Elastic.Clients.Elasticsearch.Name FeatureName { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("frequency_map")]
		public Dictionary<string, double> FrequencyMap { get; set; }
	}

	public sealed partial class DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument> : DescriptorBase<DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument>>
	{
		internal DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor(Action<DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Name FeatureNameValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private Dictionary<string, double> FrequencyMapValue { get; set; }

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument> FeatureName(Elastic.Clients.Elasticsearch.Name featureName)
		{
			FeatureNameValue = featureName;
			return Self;
		}

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor<TDocument> FrequencyMap(Func<FluentDictionary<string, double>, FluentDictionary<string, double>> selector)
		{
			FrequencyMapValue = selector?.Invoke(new FluentDictionary<string, double>());
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("feature_name");
			JsonSerializer.Serialize(writer, FeatureNameValue, options);
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			writer.WritePropertyName("frequency_map");
			JsonSerializer.Serialize(writer, FrequencyMapValue, options);
			writer.WriteEndObject();
		}
	}

	public sealed partial class DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor : DescriptorBase<DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor>
	{
		internal DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor(Action<DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor> configure) => configure.Invoke(this);
		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Name FeatureNameValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private Dictionary<string, double> FrequencyMapValue { get; set; }

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor FeatureName(Elastic.Clients.Elasticsearch.Name featureName)
		{
			FeatureNameValue = featureName;
			return Self;
		}

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DataframeAnalysisFeatureProcessorFrequencyEncodingDescriptor FrequencyMap(Func<FluentDictionary<string, double>, FluentDictionary<string, double>> selector)
		{
			FrequencyMapValue = selector?.Invoke(new FluentDictionary<string, double>());
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("feature_name");
			JsonSerializer.Serialize(writer, FeatureNameValue, options);
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			writer.WritePropertyName("frequency_map");
			JsonSerializer.Serialize(writer, FrequencyMapValue, options);
			writer.WriteEndObject();
		}
	}
}