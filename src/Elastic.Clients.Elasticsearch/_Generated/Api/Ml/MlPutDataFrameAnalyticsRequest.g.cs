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

using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	public sealed class MlPutDataFrameAnalyticsRequestParameters : RequestParameters<MlPutDataFrameAnalyticsRequestParameters>
	{
	}

	public partial class MlPutDataFrameAnalyticsRequest : PlainRequestBase<MlPutDataFrameAnalyticsRequestParameters>
	{
		public MlPutDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningPutDataFrameAnalytics;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		[JsonInclude]
		[JsonPropertyName("allow_lazy_start")]
		public bool? AllowLazyStart { get; set; }

		[JsonInclude]
		[JsonPropertyName("analysis")]
		public Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer Analysis { get; set; }

		[JsonInclude]
		[JsonPropertyName("analyzed_fields")]
		public Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? AnalyzedFields { get; set; }

		[JsonInclude]
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		[JsonInclude]
		[JsonPropertyName("dest")]
		public Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsDestination Dest { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_num_threads")]
		public int? MaxNumThreads { get; set; }

		[JsonInclude]
		[JsonPropertyName("model_memory_limit")]
		public string? ModelMemoryLimit { get; set; }

		[JsonInclude]
		[JsonPropertyName("source")]
		public Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource Source { get; set; }

		[JsonInclude]
		[JsonPropertyName("headers")]
		public Dictionary<string, IEnumerable<string>>? Headers { get; set; }

		[JsonInclude]
		[JsonPropertyName("version")]
		public string? Version { get; set; }
	}

	public sealed partial class MlPutDataFrameAnalyticsRequestDescriptor<TDocument> : RequestDescriptorBase<MlPutDataFrameAnalyticsRequestDescriptor<TDocument>, MlPutDataFrameAnalyticsRequestParameters>
	{
		internal MlPutDataFrameAnalyticsRequestDescriptor(Action<MlPutDataFrameAnalyticsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public MlPutDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal MlPutDataFrameAnalyticsRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningPutDataFrameAnalytics;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
		{
			RouteValues.Required("id", id);
			return Self;
		}

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer AnalysisValue { get; set; }

		private DataframeAnalysisContainerDescriptor<TDocument> AnalysisDescriptor { get; set; }

		private Action<DataframeAnalysisContainerDescriptor<TDocument>> AnalysisDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsDestination DestValue { get; set; }

		private DataframeAnalyticsDestinationDescriptor<TDocument> DestDescriptor { get; set; }

		private Action<DataframeAnalyticsDestinationDescriptor<TDocument>> DestDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource SourceValue { get; set; }

		private DataframeAnalyticsSourceDescriptor<TDocument> SourceDescriptor { get; set; }

		private Action<DataframeAnalyticsSourceDescriptor<TDocument>> SourceDescriptorAction { get; set; }

		private bool? AllowLazyStartValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? AnalyzedFieldsValue { get; set; }

		private DataframeAnalysisAnalyzedFieldsDescriptor AnalyzedFieldsDescriptor { get; set; }

		private Action<DataframeAnalysisAnalyzedFieldsDescriptor> AnalyzedFieldsDescriptorAction { get; set; }

		private string? DescriptionValue { get; set; }

		private Dictionary<string, IEnumerable<string>>? HeadersValue { get; set; }

		private int? MaxNumThreadsValue { get; set; }

		private string? ModelMemoryLimitValue { get; set; }

		private string? VersionValue { get; set; }

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Analysis(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer analysis)
		{
			AnalysisDescriptor = null;
			AnalysisDescriptorAction = null;
			AnalysisValue = analysis;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Analysis(DataframeAnalysisContainerDescriptor<TDocument> descriptor)
		{
			AnalysisValue = null;
			AnalysisDescriptorAction = null;
			AnalysisDescriptor = descriptor;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Analysis(Action<DataframeAnalysisContainerDescriptor<TDocument>> configure)
		{
			AnalysisValue = null;
			AnalysisDescriptor = null;
			AnalysisDescriptorAction = configure;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Dest(Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsDestination dest)
		{
			DestDescriptor = null;
			DestDescriptorAction = null;
			DestValue = dest;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Dest(DataframeAnalyticsDestinationDescriptor<TDocument> descriptor)
		{
			DestValue = null;
			DestDescriptorAction = null;
			DestDescriptor = descriptor;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Dest(Action<DataframeAnalyticsDestinationDescriptor<TDocument>> configure)
		{
			DestValue = null;
			DestDescriptor = null;
			DestDescriptorAction = configure;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource source)
		{
			SourceDescriptor = null;
			SourceDescriptorAction = null;
			SourceValue = source;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Source(DataframeAnalyticsSourceDescriptor<TDocument> descriptor)
		{
			SourceValue = null;
			SourceDescriptorAction = null;
			SourceDescriptor = descriptor;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Source(Action<DataframeAnalyticsSourceDescriptor<TDocument>> configure)
		{
			SourceValue = null;
			SourceDescriptor = null;
			SourceDescriptorAction = configure;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> AllowLazyStart(bool? allowLazyStart = true)
		{
			AllowLazyStartValue = allowLazyStart;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> AnalyzedFields(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? analyzedFields)
		{
			AnalyzedFieldsDescriptor = null;
			AnalyzedFieldsDescriptorAction = null;
			AnalyzedFieldsValue = analyzedFields;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> AnalyzedFields(DataframeAnalysisAnalyzedFieldsDescriptor descriptor)
		{
			AnalyzedFieldsValue = null;
			AnalyzedFieldsDescriptorAction = null;
			AnalyzedFieldsDescriptor = descriptor;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> AnalyzedFields(Action<DataframeAnalysisAnalyzedFieldsDescriptor> configure)
		{
			AnalyzedFieldsValue = null;
			AnalyzedFieldsDescriptor = null;
			AnalyzedFieldsDescriptorAction = configure;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Description(string? description)
		{
			DescriptionValue = description;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Headers(Func<FluentDictionary<string, IEnumerable<string>>, FluentDictionary<string, IEnumerable<string>>> selector)
		{
			HeadersValue = selector?.Invoke(new FluentDictionary<string, IEnumerable<string>>());
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> MaxNumThreads(int? maxNumThreads)
		{
			MaxNumThreadsValue = maxNumThreads;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> ModelMemoryLimit(string? modelMemoryLimit)
		{
			ModelMemoryLimitValue = modelMemoryLimit;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor<TDocument> Version(string? version)
		{
			VersionValue = version;
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

			if (DestDescriptor is not null)
			{
				writer.WritePropertyName("dest");
				JsonSerializer.Serialize(writer, DestDescriptor, options);
			}
			else if (DestDescriptorAction is not null)
			{
				writer.WritePropertyName("dest");
				JsonSerializer.Serialize(writer, new DataframeAnalyticsDestinationDescriptor<TDocument>(DestDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("dest");
				JsonSerializer.Serialize(writer, DestValue, options);
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

			if (AllowLazyStartValue.HasValue)
			{
				writer.WritePropertyName("allow_lazy_start");
				writer.WriteBooleanValue(AllowLazyStartValue.Value);
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

			if (!string.IsNullOrEmpty(DescriptionValue))
			{
				writer.WritePropertyName("description");
				writer.WriteStringValue(DescriptionValue);
			}

			if (HeadersValue is not null)
			{
				writer.WritePropertyName("headers");
				JsonSerializer.Serialize(writer, HeadersValue, options);
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

			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class MlPutDataFrameAnalyticsRequestDescriptor : RequestDescriptorBase<MlPutDataFrameAnalyticsRequestDescriptor, MlPutDataFrameAnalyticsRequestParameters>
	{
		internal MlPutDataFrameAnalyticsRequestDescriptor(Action<MlPutDataFrameAnalyticsRequestDescriptor> configure) => configure.Invoke(this);
		public MlPutDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal MlPutDataFrameAnalyticsRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningPutDataFrameAnalytics;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		public MlPutDataFrameAnalyticsRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
		{
			RouteValues.Required("id", id);
			return Self;
		}

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer AnalysisValue { get; set; }

		private DataframeAnalysisContainerDescriptor AnalysisDescriptor { get; set; }

		private Action<DataframeAnalysisContainerDescriptor> AnalysisDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsDestination DestValue { get; set; }

		private DataframeAnalyticsDestinationDescriptor DestDescriptor { get; set; }

		private Action<DataframeAnalyticsDestinationDescriptor> DestDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource SourceValue { get; set; }

		private DataframeAnalyticsSourceDescriptor SourceDescriptor { get; set; }

		private Action<DataframeAnalyticsSourceDescriptor> SourceDescriptorAction { get; set; }

		private bool? AllowLazyStartValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? AnalyzedFieldsValue { get; set; }

		private DataframeAnalysisAnalyzedFieldsDescriptor AnalyzedFieldsDescriptor { get; set; }

		private Action<DataframeAnalysisAnalyzedFieldsDescriptor> AnalyzedFieldsDescriptorAction { get; set; }

		private string? DescriptionValue { get; set; }

		private Dictionary<string, IEnumerable<string>>? HeadersValue { get; set; }

		private int? MaxNumThreadsValue { get; set; }

		private string? ModelMemoryLimitValue { get; set; }

		private string? VersionValue { get; set; }

		public MlPutDataFrameAnalyticsRequestDescriptor Analysis(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisContainer analysis)
		{
			AnalysisDescriptor = null;
			AnalysisDescriptorAction = null;
			AnalysisValue = analysis;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Analysis(DataframeAnalysisContainerDescriptor descriptor)
		{
			AnalysisValue = null;
			AnalysisDescriptorAction = null;
			AnalysisDescriptor = descriptor;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Analysis(Action<DataframeAnalysisContainerDescriptor> configure)
		{
			AnalysisValue = null;
			AnalysisDescriptor = null;
			AnalysisDescriptorAction = configure;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Dest(Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsDestination dest)
		{
			DestDescriptor = null;
			DestDescriptorAction = null;
			DestValue = dest;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Dest(DataframeAnalyticsDestinationDescriptor descriptor)
		{
			DestValue = null;
			DestDescriptorAction = null;
			DestDescriptor = descriptor;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Dest(Action<DataframeAnalyticsDestinationDescriptor> configure)
		{
			DestValue = null;
			DestDescriptor = null;
			DestDescriptorAction = configure;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Source(Elastic.Clients.Elasticsearch.Ml.DataframeAnalyticsSource source)
		{
			SourceDescriptor = null;
			SourceDescriptorAction = null;
			SourceValue = source;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Source(DataframeAnalyticsSourceDescriptor descriptor)
		{
			SourceValue = null;
			SourceDescriptorAction = null;
			SourceDescriptor = descriptor;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Source(Action<DataframeAnalyticsSourceDescriptor> configure)
		{
			SourceValue = null;
			SourceDescriptor = null;
			SourceDescriptorAction = configure;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor AllowLazyStart(bool? allowLazyStart = true)
		{
			AllowLazyStartValue = allowLazyStart;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor AnalyzedFields(Elastic.Clients.Elasticsearch.Ml.DataframeAnalysisAnalyzedFields? analyzedFields)
		{
			AnalyzedFieldsDescriptor = null;
			AnalyzedFieldsDescriptorAction = null;
			AnalyzedFieldsValue = analyzedFields;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor AnalyzedFields(DataframeAnalysisAnalyzedFieldsDescriptor descriptor)
		{
			AnalyzedFieldsValue = null;
			AnalyzedFieldsDescriptorAction = null;
			AnalyzedFieldsDescriptor = descriptor;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor AnalyzedFields(Action<DataframeAnalysisAnalyzedFieldsDescriptor> configure)
		{
			AnalyzedFieldsValue = null;
			AnalyzedFieldsDescriptor = null;
			AnalyzedFieldsDescriptorAction = configure;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Description(string? description)
		{
			DescriptionValue = description;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Headers(Func<FluentDictionary<string, IEnumerable<string>>, FluentDictionary<string, IEnumerable<string>>> selector)
		{
			HeadersValue = selector?.Invoke(new FluentDictionary<string, IEnumerable<string>>());
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor MaxNumThreads(int? maxNumThreads)
		{
			MaxNumThreadsValue = maxNumThreads;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor ModelMemoryLimit(string? modelMemoryLimit)
		{
			ModelMemoryLimitValue = modelMemoryLimit;
			return Self;
		}

		public MlPutDataFrameAnalyticsRequestDescriptor Version(string? version)
		{
			VersionValue = version;
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

			if (DestDescriptor is not null)
			{
				writer.WritePropertyName("dest");
				JsonSerializer.Serialize(writer, DestDescriptor, options);
			}
			else if (DestDescriptorAction is not null)
			{
				writer.WritePropertyName("dest");
				JsonSerializer.Serialize(writer, new DataframeAnalyticsDestinationDescriptor(DestDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("dest");
				JsonSerializer.Serialize(writer, DestValue, options);
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

			if (AllowLazyStartValue.HasValue)
			{
				writer.WritePropertyName("allow_lazy_start");
				writer.WriteBooleanValue(AllowLazyStartValue.Value);
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

			if (!string.IsNullOrEmpty(DescriptionValue))
			{
				writer.WritePropertyName("description");
				writer.WriteStringValue(DescriptionValue);
			}

			if (HeadersValue is not null)
			{
				writer.WritePropertyName("headers");
				JsonSerializer.Serialize(writer, HeadersValue, options);
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

			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}
	}
}