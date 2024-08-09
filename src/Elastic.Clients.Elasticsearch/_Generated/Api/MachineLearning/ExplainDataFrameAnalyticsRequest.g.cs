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
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class ExplainDataFrameAnalyticsRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Explain data frame analytics config.
/// This API provides explanations for a data frame analytics config that either
/// exists already or one that has not been created yet. The following
/// explanations are provided:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// which fields are included or not in the analysis and why,
/// </para>
/// </item>
/// <item>
/// <para>
/// how much memory is estimated to be required. The estimate can be used when deciding the appropriate value for model_memory_limit setting later on.
/// If you have object fields or fields that are excluded via source filtering, they are not included in the explanation.
/// </para>
/// </item>
/// </list>
/// </summary>
public sealed partial class ExplainDataFrameAnalyticsRequest : PlainRequest<ExplainDataFrameAnalyticsRequestParameters>
{
	public ExplainDataFrameAnalyticsRequest()
	{
	}

	public ExplainDataFrameAnalyticsRequest(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningExplainDataFrameAnalytics;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.explain_data_frame_analytics";

	/// <summary>
	/// <para>
	/// Specifies whether this job can start when there is insufficient machine
	/// learning node capacity for it to be immediately assigned to a node.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("allow_lazy_start")]
	public bool? AllowLazyStart { get; set; }

	/// <summary>
	/// <para>
	/// The analysis configuration, which contains the information necessary to
	/// perform one of the following types of analysis: classification, outlier
	/// detection, or regression.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("analysis")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysis? Analysis { get; set; }

	/// <summary>
	/// <para>
	/// Specify includes and/or excludes patterns to select which fields will be
	/// included in the analysis. The patterns specified in excludes are applied
	/// last, therefore excludes takes precedence. In other words, if the same
	/// field is specified in both includes and excludes, then the field will not
	/// be included in the analysis.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("analyzed_fields")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields? AnalyzedFields { get; set; }

	/// <summary>
	/// <para>
	/// A description of the job.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The destination configuration, consisting of index and optionally
	/// results_field (ml by default).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("dest")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestination? Dest { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of threads to be used by the analysis. Using more
	/// threads may decrease the time necessary to complete the analysis at the
	/// cost of using more CPU. Note that the process may use additional threads
	/// for operational functionality other than the analysis itself.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_num_threads")]
	public int? MaxNumThreads { get; set; }

	/// <summary>
	/// <para>
	/// The approximate maximum amount of memory resources that are permitted for
	/// analytical processing. If your <c>elasticsearch.yml</c> file contains an
	/// <c>xpack.ml.max_model_memory_limit</c> setting, an error occurs when you try to
	/// create data frame analytics jobs that have <c>model_memory_limit</c> values
	/// greater than that setting.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("model_memory_limit")]
	public string? ModelMemoryLimit { get; set; }

	/// <summary>
	/// <para>
	/// The configuration of how to source the analysis data. It requires an
	/// index. Optionally, query and _source may be specified.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("source")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSource? Source { get; set; }
}

/// <summary>
/// <para>
/// Explain data frame analytics config.
/// This API provides explanations for a data frame analytics config that either
/// exists already or one that has not been created yet. The following
/// explanations are provided:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// which fields are included or not in the analysis and why,
/// </para>
/// </item>
/// <item>
/// <para>
/// how much memory is estimated to be required. The estimate can be used when deciding the appropriate value for model_memory_limit setting later on.
/// If you have object fields or fields that are excluded via source filtering, they are not included in the explanation.
/// </para>
/// </item>
/// </list>
/// </summary>
public sealed partial class ExplainDataFrameAnalyticsRequestDescriptor<TDocument> : RequestDescriptor<ExplainDataFrameAnalyticsRequestDescriptor<TDocument>, ExplainDataFrameAnalyticsRequestParameters>
{
	internal ExplainDataFrameAnalyticsRequestDescriptor(Action<ExplainDataFrameAnalyticsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ExplainDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}

	public ExplainDataFrameAnalyticsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningExplainDataFrameAnalytics;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.explain_data_frame_analytics";

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id? id)
	{
		RouteValues.Optional("id", id);
		return Self;
	}

	private bool? AllowLazyStartValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysis? AnalysisValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor<TDocument> AnalysisDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor<TDocument>> AnalysisDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields? AnalyzedFieldsValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor AnalyzedFieldsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor> AnalyzedFieldsDescriptorAction { get; set; }
	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestination? DestValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor<TDocument> DestDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor<TDocument>> DestDescriptorAction { get; set; }
	private int? MaxNumThreadsValue { get; set; }
	private string? ModelMemoryLimitValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSource? SourceValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor<TDocument> SourceDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor<TDocument>> SourceDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Specifies whether this job can start when there is insufficient machine
	/// learning node capacity for it to be immediately assigned to a node.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> AllowLazyStart(bool? allowLazyStart = true)
	{
		AllowLazyStartValue = allowLazyStart;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The analysis configuration, which contains the information necessary to
	/// perform one of the following types of analysis: classification, outlier
	/// detection, or regression.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Analysis(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysis? analysis)
	{
		AnalysisDescriptor = null;
		AnalysisDescriptorAction = null;
		AnalysisValue = analysis;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Analysis(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor<TDocument> descriptor)
	{
		AnalysisValue = null;
		AnalysisDescriptorAction = null;
		AnalysisDescriptor = descriptor;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Analysis(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor<TDocument>> configure)
	{
		AnalysisValue = null;
		AnalysisDescriptor = null;
		AnalysisDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specify includes and/or excludes patterns to select which fields will be
	/// included in the analysis. The patterns specified in excludes are applied
	/// last, therefore excludes takes precedence. In other words, if the same
	/// field is specified in both includes and excludes, then the field will not
	/// be included in the analysis.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> AnalyzedFields(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields? analyzedFields)
	{
		AnalyzedFieldsDescriptor = null;
		AnalyzedFieldsDescriptorAction = null;
		AnalyzedFieldsValue = analyzedFields;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> AnalyzedFields(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor descriptor)
	{
		AnalyzedFieldsValue = null;
		AnalyzedFieldsDescriptorAction = null;
		AnalyzedFieldsDescriptor = descriptor;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> AnalyzedFields(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor> configure)
	{
		AnalyzedFieldsValue = null;
		AnalyzedFieldsDescriptor = null;
		AnalyzedFieldsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A description of the job.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The destination configuration, consisting of index and optionally
	/// results_field (ml by default).
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Dest(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestination? dest)
	{
		DestDescriptor = null;
		DestDescriptorAction = null;
		DestValue = dest;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Dest(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor<TDocument> descriptor)
	{
		DestValue = null;
		DestDescriptorAction = null;
		DestDescriptor = descriptor;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Dest(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor<TDocument>> configure)
	{
		DestValue = null;
		DestDescriptor = null;
		DestDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of threads to be used by the analysis. Using more
	/// threads may decrease the time necessary to complete the analysis at the
	/// cost of using more CPU. Note that the process may use additional threads
	/// for operational functionality other than the analysis itself.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> MaxNumThreads(int? maxNumThreads)
	{
		MaxNumThreadsValue = maxNumThreads;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The approximate maximum amount of memory resources that are permitted for
	/// analytical processing. If your <c>elasticsearch.yml</c> file contains an
	/// <c>xpack.ml.max_model_memory_limit</c> setting, an error occurs when you try to
	/// create data frame analytics jobs that have <c>model_memory_limit</c> values
	/// greater than that setting.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> ModelMemoryLimit(string? modelMemoryLimit)
	{
		ModelMemoryLimitValue = modelMemoryLimit;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The configuration of how to source the analysis data. It requires an
	/// index. Optionally, query and _source may be specified.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSource? source)
	{
		SourceDescriptor = null;
		SourceDescriptorAction = null;
		SourceValue = source;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor<TDocument> descriptor)
	{
		SourceValue = null;
		SourceDescriptorAction = null;
		SourceDescriptor = descriptor;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor<TDocument> Source(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor<TDocument>> configure)
	{
		SourceValue = null;
		SourceDescriptor = null;
		SourceDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AllowLazyStartValue.HasValue)
		{
			writer.WritePropertyName("allow_lazy_start");
			writer.WriteBooleanValue(AllowLazyStartValue.Value);
		}

		if (AnalysisDescriptor is not null)
		{
			writer.WritePropertyName("analysis");
			JsonSerializer.Serialize(writer, AnalysisDescriptor, options);
		}
		else if (AnalysisDescriptorAction is not null)
		{
			writer.WritePropertyName("analysis");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor<TDocument>(AnalysisDescriptorAction), options);
		}
		else if (AnalysisValue is not null)
		{
			writer.WritePropertyName("analysis");
			JsonSerializer.Serialize(writer, AnalysisValue, options);
		}

		if (AnalyzedFieldsDescriptor is not null)
		{
			writer.WritePropertyName("analyzed_fields");
			JsonSerializer.Serialize(writer, AnalyzedFieldsDescriptor, options);
		}
		else if (AnalyzedFieldsDescriptorAction is not null)
		{
			writer.WritePropertyName("analyzed_fields");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor(AnalyzedFieldsDescriptorAction), options);
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

		if (DestDescriptor is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, DestDescriptor, options);
		}
		else if (DestDescriptorAction is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor<TDocument>(DestDescriptorAction), options);
		}
		else if (DestValue is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, DestValue, options);
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

		if (SourceDescriptor is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, SourceDescriptor, options);
		}
		else if (SourceDescriptorAction is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor<TDocument>(SourceDescriptorAction), options);
		}
		else if (SourceValue is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Explain data frame analytics config.
/// This API provides explanations for a data frame analytics config that either
/// exists already or one that has not been created yet. The following
/// explanations are provided:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// which fields are included or not in the analysis and why,
/// </para>
/// </item>
/// <item>
/// <para>
/// how much memory is estimated to be required. The estimate can be used when deciding the appropriate value for model_memory_limit setting later on.
/// If you have object fields or fields that are excluded via source filtering, they are not included in the explanation.
/// </para>
/// </item>
/// </list>
/// </summary>
public sealed partial class ExplainDataFrameAnalyticsRequestDescriptor : RequestDescriptor<ExplainDataFrameAnalyticsRequestDescriptor, ExplainDataFrameAnalyticsRequestParameters>
{
	internal ExplainDataFrameAnalyticsRequestDescriptor(Action<ExplainDataFrameAnalyticsRequestDescriptor> configure) => configure.Invoke(this);

	public ExplainDataFrameAnalyticsRequestDescriptor(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}

	public ExplainDataFrameAnalyticsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningExplainDataFrameAnalytics;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.explain_data_frame_analytics";

	public ExplainDataFrameAnalyticsRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? id)
	{
		RouteValues.Optional("id", id);
		return Self;
	}

	private bool? AllowLazyStartValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysis? AnalysisValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor AnalysisDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor> AnalysisDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields? AnalyzedFieldsValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor AnalyzedFieldsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor> AnalyzedFieldsDescriptorAction { get; set; }
	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestination? DestValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor DestDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor> DestDescriptorAction { get; set; }
	private int? MaxNumThreadsValue { get; set; }
	private string? ModelMemoryLimitValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSource? SourceValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor SourceDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor> SourceDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Specifies whether this job can start when there is insufficient machine
	/// learning node capacity for it to be immediately assigned to a node.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor AllowLazyStart(bool? allowLazyStart = true)
	{
		AllowLazyStartValue = allowLazyStart;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The analysis configuration, which contains the information necessary to
	/// perform one of the following types of analysis: classification, outlier
	/// detection, or regression.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor Analysis(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysis? analysis)
	{
		AnalysisDescriptor = null;
		AnalysisDescriptorAction = null;
		AnalysisValue = analysis;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor Analysis(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor descriptor)
	{
		AnalysisValue = null;
		AnalysisDescriptorAction = null;
		AnalysisDescriptor = descriptor;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor Analysis(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor> configure)
	{
		AnalysisValue = null;
		AnalysisDescriptor = null;
		AnalysisDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specify includes and/or excludes patterns to select which fields will be
	/// included in the analysis. The patterns specified in excludes are applied
	/// last, therefore excludes takes precedence. In other words, if the same
	/// field is specified in both includes and excludes, then the field will not
	/// be included in the analysis.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor AnalyzedFields(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields? analyzedFields)
	{
		AnalyzedFieldsDescriptor = null;
		AnalyzedFieldsDescriptorAction = null;
		AnalyzedFieldsValue = analyzedFields;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor AnalyzedFields(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor descriptor)
	{
		AnalyzedFieldsValue = null;
		AnalyzedFieldsDescriptorAction = null;
		AnalyzedFieldsDescriptor = descriptor;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor AnalyzedFields(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor> configure)
	{
		AnalyzedFieldsValue = null;
		AnalyzedFieldsDescriptor = null;
		AnalyzedFieldsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A description of the job.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The destination configuration, consisting of index and optionally
	/// results_field (ml by default).
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor Dest(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestination? dest)
	{
		DestDescriptor = null;
		DestDescriptorAction = null;
		DestValue = dest;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor Dest(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor descriptor)
	{
		DestValue = null;
		DestDescriptorAction = null;
		DestDescriptor = descriptor;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor Dest(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor> configure)
	{
		DestValue = null;
		DestDescriptor = null;
		DestDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of threads to be used by the analysis. Using more
	/// threads may decrease the time necessary to complete the analysis at the
	/// cost of using more CPU. Note that the process may use additional threads
	/// for operational functionality other than the analysis itself.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor MaxNumThreads(int? maxNumThreads)
	{
		MaxNumThreadsValue = maxNumThreads;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The approximate maximum amount of memory resources that are permitted for
	/// analytical processing. If your <c>elasticsearch.yml</c> file contains an
	/// <c>xpack.ml.max_model_memory_limit</c> setting, an error occurs when you try to
	/// create data frame analytics jobs that have <c>model_memory_limit</c> values
	/// greater than that setting.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor ModelMemoryLimit(string? modelMemoryLimit)
	{
		ModelMemoryLimitValue = modelMemoryLimit;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The configuration of how to source the analysis data. It requires an
	/// index. Optionally, query and _source may be specified.
	/// </para>
	/// </summary>
	public ExplainDataFrameAnalyticsRequestDescriptor Source(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSource? source)
	{
		SourceDescriptor = null;
		SourceDescriptorAction = null;
		SourceValue = source;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor Source(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor descriptor)
	{
		SourceValue = null;
		SourceDescriptorAction = null;
		SourceDescriptor = descriptor;
		return Self;
	}

	public ExplainDataFrameAnalyticsRequestDescriptor Source(Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor> configure)
	{
		SourceValue = null;
		SourceDescriptor = null;
		SourceDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AllowLazyStartValue.HasValue)
		{
			writer.WritePropertyName("allow_lazy_start");
			writer.WriteBooleanValue(AllowLazyStartValue.Value);
		}

		if (AnalysisDescriptor is not null)
		{
			writer.WritePropertyName("analysis");
			JsonSerializer.Serialize(writer, AnalysisDescriptor, options);
		}
		else if (AnalysisDescriptorAction is not null)
		{
			writer.WritePropertyName("analysis");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisDescriptor(AnalysisDescriptorAction), options);
		}
		else if (AnalysisValue is not null)
		{
			writer.WritePropertyName("analysis");
			JsonSerializer.Serialize(writer, AnalysisValue, options);
		}

		if (AnalyzedFieldsDescriptor is not null)
		{
			writer.WritePropertyName("analyzed_fields");
			JsonSerializer.Serialize(writer, AnalyzedFieldsDescriptor, options);
		}
		else if (AnalyzedFieldsDescriptorAction is not null)
		{
			writer.WritePropertyName("analyzed_fields");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor(AnalyzedFieldsDescriptorAction), options);
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

		if (DestDescriptor is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, DestDescriptor, options);
		}
		else if (DestDescriptorAction is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestinationDescriptor(DestDescriptorAction), options);
		}
		else if (DestValue is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, DestValue, options);
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

		if (SourceDescriptor is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, SourceDescriptor, options);
		}
		else if (SourceDescriptorAction is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSourceDescriptor(SourceDescriptorAction), options);
		}
		else if (SourceValue is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		writer.WriteEndObject();
	}
}