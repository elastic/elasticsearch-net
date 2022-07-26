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
	public sealed partial class DataframePreviewConfig
	{
		[JsonInclude]
		[JsonPropertyName("analysis")]
		public Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer Analysis { get; set; }

		[JsonInclude]
		[JsonPropertyName("analyzed_fields")]
		public Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? AnalyzedFields { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_num_threads")]
		public int? MaxNumThreads { get; set; }

		[JsonInclude]
		[JsonPropertyName("model_memory_limit")]
		public string? ModelMemoryLimit { get; set; }

		[JsonInclude]
		[JsonPropertyName("source")]
		public Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource Source { get; set; }
	}

	public sealed partial class DataframePreviewConfigDescriptor<TDocument> : SerializableDescriptorBase<DataframePreviewConfigDescriptor<TDocument>>
	{
		internal DataframePreviewConfigDescriptor(Action<DataframePreviewConfigDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DataframePreviewConfigDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer AnalysisValue { get; set; }

		private DataframeAnalysisContainerDescriptor<TDocument> AnalysisDescriptor { get; set; }

		private Action<DataframeAnalysisContainerDescriptor<TDocument>> AnalysisDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource SourceValue { get; set; }

		private DataframeAnalyticsSourceDescriptor<TDocument> SourceDescriptor { get; set; }

		private Action<DataframeAnalyticsSourceDescriptor<TDocument>> SourceDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? AnalyzedFieldsValue { get; set; }

		private DataframeAnalysisAnalyzedFieldsDescriptor AnalyzedFieldsDescriptor { get; set; }

		private Action<DataframeAnalysisAnalyzedFieldsDescriptor> AnalyzedFieldsDescriptorAction { get; set; }

		private int? MaxNumThreadsValue { get; set; }

		private string? ModelMemoryLimitValue { get; set; }

		public DataframePreviewConfigDescriptor<TDocument> Analysis(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer analysis)
		{
			AnalysisDescriptor = null;
			AnalysisDescriptorAction = null;
			AnalysisValue = analysis;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> Analysis(DataframeAnalysisContainerDescriptor<TDocument> descriptor)
		{
			AnalysisValue = null;
			AnalysisDescriptorAction = null;
			AnalysisDescriptor = descriptor;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> Analysis(Action<DataframeAnalysisContainerDescriptor<TDocument>> configure)
		{
			AnalysisValue = null;
			AnalysisDescriptor = null;
			AnalysisDescriptorAction = configure;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource source)
		{
			SourceDescriptor = null;
			SourceDescriptorAction = null;
			SourceValue = source;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> Source(DataframeAnalyticsSourceDescriptor<TDocument> descriptor)
		{
			SourceValue = null;
			SourceDescriptorAction = null;
			SourceDescriptor = descriptor;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> Source(Action<DataframeAnalyticsSourceDescriptor<TDocument>> configure)
		{
			SourceValue = null;
			SourceDescriptor = null;
			SourceDescriptorAction = configure;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> AnalyzedFields(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? analyzedFields)
		{
			AnalyzedFieldsDescriptor = null;
			AnalyzedFieldsDescriptorAction = null;
			AnalyzedFieldsValue = analyzedFields;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> AnalyzedFields(DataframeAnalysisAnalyzedFieldsDescriptor descriptor)
		{
			AnalyzedFieldsValue = null;
			AnalyzedFieldsDescriptorAction = null;
			AnalyzedFieldsDescriptor = descriptor;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> AnalyzedFields(Action<DataframeAnalysisAnalyzedFieldsDescriptor> configure)
		{
			AnalyzedFieldsValue = null;
			AnalyzedFieldsDescriptor = null;
			AnalyzedFieldsDescriptorAction = configure;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> MaxNumThreads(int? maxNumThreads)
		{
			MaxNumThreadsValue = maxNumThreads;
			return Self;
		}

		public DataframePreviewConfigDescriptor<TDocument> ModelMemoryLimit(string? modelMemoryLimit)
		{
			ModelMemoryLimitValue = modelMemoryLimit;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (AnalysisDescriptor is not null)
			{
				writer.WritePropertyName("analysis");
				JsonSerializer.Serialize(writer, AnalysisDescriptor, options);
			}
			else if (AnalysisDescriptorAction is not null)
			{
				writer.WritePropertyName("analysis");
				JsonSerializer.Serialize(writer, new DataframeAnalysisContainerDescriptor<TDocument>(AnalysisDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("analysis");
				JsonSerializer.Serialize(writer, AnalysisValue, options);
			}

			if (SourceDescriptor is not null)
			{
				writer.WritePropertyName("source");
				JsonSerializer.Serialize(writer, SourceDescriptor, options);
			}
			else if (SourceDescriptorAction is not null)
			{
				writer.WritePropertyName("source");
				JsonSerializer.Serialize(writer, new DataframeAnalyticsSourceDescriptor<TDocument>(SourceDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("source");
				JsonSerializer.Serialize(writer, SourceValue, options);
			}

			if (AnalyzedFieldsDescriptor is not null)
			{
				writer.WritePropertyName("analyzed_fields");
				JsonSerializer.Serialize(writer, AnalyzedFieldsDescriptor, options);
			}
			else if (AnalyzedFieldsDescriptorAction is not null)
			{
				writer.WritePropertyName("analyzed_fields");
				JsonSerializer.Serialize(writer, new DataframeAnalysisAnalyzedFieldsDescriptor(AnalyzedFieldsDescriptorAction), options);
			}
			else if (AnalyzedFieldsValue is not null)
			{
				writer.WritePropertyName("analyzed_fields");
				JsonSerializer.Serialize(writer, AnalyzedFieldsValue, options);
			}

			if (MaxNumThreadsValue.HasValue)
			{
				writer.WritePropertyName("max_num_threads");
				writer.WriteNumberValue(MaxNumThreadsValue.Value);
			}

			if (!string.IsNullOrEmpty(ModelMemoryLimitValue))
			{
				writer.WritePropertyName("model_memory_limit");
				writer.WriteStringValue(ModelMemoryLimitValue);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class DataframePreviewConfigDescriptor : SerializableDescriptorBase<DataframePreviewConfigDescriptor>
	{
		internal DataframePreviewConfigDescriptor(Action<DataframePreviewConfigDescriptor> configure) => configure.Invoke(this);
		public DataframePreviewConfigDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer AnalysisValue { get; set; }

		private DataframeAnalysisContainerDescriptor AnalysisDescriptor { get; set; }

		private Action<DataframeAnalysisContainerDescriptor> AnalysisDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource SourceValue { get; set; }

		private DataframeAnalyticsSourceDescriptor SourceDescriptor { get; set; }

		private Action<DataframeAnalyticsSourceDescriptor> SourceDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? AnalyzedFieldsValue { get; set; }

		private DataframeAnalysisAnalyzedFieldsDescriptor AnalyzedFieldsDescriptor { get; set; }

		private Action<DataframeAnalysisAnalyzedFieldsDescriptor> AnalyzedFieldsDescriptorAction { get; set; }

		private int? MaxNumThreadsValue { get; set; }

		private string? ModelMemoryLimitValue { get; set; }

		public DataframePreviewConfigDescriptor Analysis(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer analysis)
		{
			AnalysisDescriptor = null;
			AnalysisDescriptorAction = null;
			AnalysisValue = analysis;
			return Self;
		}

		public DataframePreviewConfigDescriptor Analysis(DataframeAnalysisContainerDescriptor descriptor)
		{
			AnalysisValue = null;
			AnalysisDescriptorAction = null;
			AnalysisDescriptor = descriptor;
			return Self;
		}

		public DataframePreviewConfigDescriptor Analysis(Action<DataframeAnalysisContainerDescriptor> configure)
		{
			AnalysisValue = null;
			AnalysisDescriptor = null;
			AnalysisDescriptorAction = configure;
			return Self;
		}

		public DataframePreviewConfigDescriptor Source(Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource source)
		{
			SourceDescriptor = null;
			SourceDescriptorAction = null;
			SourceValue = source;
			return Self;
		}

		public DataframePreviewConfigDescriptor Source(DataframeAnalyticsSourceDescriptor descriptor)
		{
			SourceValue = null;
			SourceDescriptorAction = null;
			SourceDescriptor = descriptor;
			return Self;
		}

		public DataframePreviewConfigDescriptor Source(Action<DataframeAnalyticsSourceDescriptor> configure)
		{
			SourceValue = null;
			SourceDescriptor = null;
			SourceDescriptorAction = configure;
			return Self;
		}

		public DataframePreviewConfigDescriptor AnalyzedFields(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? analyzedFields)
		{
			AnalyzedFieldsDescriptor = null;
			AnalyzedFieldsDescriptorAction = null;
			AnalyzedFieldsValue = analyzedFields;
			return Self;
		}

		public DataframePreviewConfigDescriptor AnalyzedFields(DataframeAnalysisAnalyzedFieldsDescriptor descriptor)
		{
			AnalyzedFieldsValue = null;
			AnalyzedFieldsDescriptorAction = null;
			AnalyzedFieldsDescriptor = descriptor;
			return Self;
		}

		public DataframePreviewConfigDescriptor AnalyzedFields(Action<DataframeAnalysisAnalyzedFieldsDescriptor> configure)
		{
			AnalyzedFieldsValue = null;
			AnalyzedFieldsDescriptor = null;
			AnalyzedFieldsDescriptorAction = configure;
			return Self;
		}

		public DataframePreviewConfigDescriptor MaxNumThreads(int? maxNumThreads)
		{
			MaxNumThreadsValue = maxNumThreads;
			return Self;
		}

		public DataframePreviewConfigDescriptor ModelMemoryLimit(string? modelMemoryLimit)
		{
			ModelMemoryLimitValue = modelMemoryLimit;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (AnalysisDescriptor is not null)
			{
				writer.WritePropertyName("analysis");
				JsonSerializer.Serialize(writer, AnalysisDescriptor, options);
			}
			else if (AnalysisDescriptorAction is not null)
			{
				writer.WritePropertyName("analysis");
				JsonSerializer.Serialize(writer, new DataframeAnalysisContainerDescriptor(AnalysisDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("analysis");
				JsonSerializer.Serialize(writer, AnalysisValue, options);
			}

			if (SourceDescriptor is not null)
			{
				writer.WritePropertyName("source");
				JsonSerializer.Serialize(writer, SourceDescriptor, options);
			}
			else if (SourceDescriptorAction is not null)
			{
				writer.WritePropertyName("source");
				JsonSerializer.Serialize(writer, new DataframeAnalyticsSourceDescriptor(SourceDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("source");
				JsonSerializer.Serialize(writer, SourceValue, options);
			}

			if (AnalyzedFieldsDescriptor is not null)
			{
				writer.WritePropertyName("analyzed_fields");
				JsonSerializer.Serialize(writer, AnalyzedFieldsDescriptor, options);
			}
			else if (AnalyzedFieldsDescriptorAction is not null)
			{
				writer.WritePropertyName("analyzed_fields");
				JsonSerializer.Serialize(writer, new DataframeAnalysisAnalyzedFieldsDescriptor(AnalyzedFieldsDescriptorAction), options);
			}
			else if (AnalyzedFieldsValue is not null)
			{
				writer.WritePropertyName("analyzed_fields");
				JsonSerializer.Serialize(writer, AnalyzedFieldsValue, options);
			}

			if (MaxNumThreadsValue.HasValue)
			{
				writer.WritePropertyName("max_num_threads");
				writer.WriteNumberValue(MaxNumThreadsValue.Value);
			}

			if (!string.IsNullOrEmpty(ModelMemoryLimitValue))
			{
				writer.WritePropertyName("model_memory_limit");
				writer.WriteStringValue(ModelMemoryLimitValue);
			}

			writer.WriteEndObject();
		}
	}
}