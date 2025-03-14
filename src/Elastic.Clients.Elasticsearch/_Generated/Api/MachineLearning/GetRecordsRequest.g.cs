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
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class GetRecordsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Skips the specified number of records.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of records to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>
/// Get anomaly records for an anomaly detection job.
/// Records contain the detailed analytical results. They describe the anomalous
/// activity that has been identified in the input data based on the detector
/// configuration.
/// There can be many anomaly records depending on the characteristics and size
/// of the input data. In practice, there are often too many to be able to
/// manually process them. The machine learning features therefore perform a
/// sophisticated aggregation of the anomaly records into buckets.
/// The number of record results depends on the number of anomalies found in each
/// bucket, which relates to the number of time series being modeled and the
/// number of detectors.
/// </para>
/// </summary>
public sealed partial class GetRecordsRequest : PlainRequest<GetRecordsRequestParameters>
{
	public GetRecordsRequest(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetRecords;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.get_records";

	/// <summary>
	/// <para>
	/// Skips the specified number of records.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of records to obtain.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>desc</c> query parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("desc")]
	public bool? Desc { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>end</c> query parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("end")]
	public DateTimeOffset? End { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>exclude_interim</c> query parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("exclude_interim")]
	public bool? ExcludeInterim { get; set; }
	[JsonInclude, JsonPropertyName("page")]
	public Elastic.Clients.Elasticsearch.MachineLearning.Page? Page { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>record_score</c> query parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("record_score")]
	public double? RecordScore { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>sort</c> query parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("sort")]
	public Elastic.Clients.Elasticsearch.Field? Sort { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>start</c> query parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("start")]
	public DateTimeOffset? Start { get; set; }
}

/// <summary>
/// <para>
/// Get anomaly records for an anomaly detection job.
/// Records contain the detailed analytical results. They describe the anomalous
/// activity that has been identified in the input data based on the detector
/// configuration.
/// There can be many anomaly records depending on the characteristics and size
/// of the input data. In practice, there are often too many to be able to
/// manually process them. The machine learning features therefore perform a
/// sophisticated aggregation of the anomaly records into buckets.
/// The number of record results depends on the number of anomalies found in each
/// bucket, which relates to the number of time series being modeled and the
/// number of detectors.
/// </para>
/// </summary>
public sealed partial class GetRecordsRequestDescriptor<TDocument> : RequestDescriptor<GetRecordsRequestDescriptor<TDocument>, GetRecordsRequestParameters>
{
	internal GetRecordsRequestDescriptor(Action<GetRecordsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GetRecordsRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetRecords;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.get_records";

	public GetRecordsRequestDescriptor<TDocument> From(int? from) => Qs("from", from);
	public GetRecordsRequestDescriptor<TDocument> Size(int? size) => Qs("size", size);

	public GetRecordsRequestDescriptor<TDocument> JobId(Elastic.Clients.Elasticsearch.Id jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	private bool? DescValue { get; set; }
	private DateTimeOffset? EndValue { get; set; }
	private bool? ExcludeInterimValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.Page? PageValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor PageDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor> PageDescriptorAction { get; set; }
	private double? RecordScoreValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? SortValue { get; set; }
	private DateTimeOffset? StartValue { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>desc</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor<TDocument> Desc(bool? desc = true)
	{
		DescValue = desc;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>end</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor<TDocument> End(DateTimeOffset? end)
	{
		EndValue = end;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>exclude_interim</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor<TDocument> ExcludeInterim(bool? excludeInterim = true)
	{
		ExcludeInterimValue = excludeInterim;
		return Self;
	}

	public GetRecordsRequestDescriptor<TDocument> Page(Elastic.Clients.Elasticsearch.MachineLearning.Page? page)
	{
		PageDescriptor = null;
		PageDescriptorAction = null;
		PageValue = page;
		return Self;
	}

	public GetRecordsRequestDescriptor<TDocument> Page(Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor descriptor)
	{
		PageValue = null;
		PageDescriptorAction = null;
		PageDescriptor = descriptor;
		return Self;
	}

	public GetRecordsRequestDescriptor<TDocument> Page(Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor> configure)
	{
		PageValue = null;
		PageDescriptor = null;
		PageDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>record_score</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor<TDocument> RecordScore(double? recordScore)
	{
		RecordScoreValue = recordScore;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>sort</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.Field? sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>sort</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor<TDocument> Sort<TValue>(Expression<Func<TDocument, TValue>> sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>sort</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor<TDocument> Sort(Expression<Func<TDocument, object>> sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>start</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor<TDocument> Start(DateTimeOffset? start)
	{
		StartValue = start;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DescValue.HasValue)
		{
			writer.WritePropertyName("desc");
			writer.WriteBooleanValue(DescValue.Value);
		}

		if (EndValue is not null)
		{
			writer.WritePropertyName("end");
			JsonSerializer.Serialize(writer, EndValue, options);
		}

		if (ExcludeInterimValue.HasValue)
		{
			writer.WritePropertyName("exclude_interim");
			writer.WriteBooleanValue(ExcludeInterimValue.Value);
		}

		if (PageDescriptor is not null)
		{
			writer.WritePropertyName("page");
			JsonSerializer.Serialize(writer, PageDescriptor, options);
		}
		else if (PageDescriptorAction is not null)
		{
			writer.WritePropertyName("page");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor(PageDescriptorAction), options);
		}
		else if (PageValue is not null)
		{
			writer.WritePropertyName("page");
			JsonSerializer.Serialize(writer, PageValue, options);
		}

		if (RecordScoreValue.HasValue)
		{
			writer.WritePropertyName("record_score");
			writer.WriteNumberValue(RecordScoreValue.Value);
		}

		if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, SortValue, options);
		}

		if (StartValue is not null)
		{
			writer.WritePropertyName("start");
			JsonSerializer.Serialize(writer, StartValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get anomaly records for an anomaly detection job.
/// Records contain the detailed analytical results. They describe the anomalous
/// activity that has been identified in the input data based on the detector
/// configuration.
/// There can be many anomaly records depending on the characteristics and size
/// of the input data. In practice, there are often too many to be able to
/// manually process them. The machine learning features therefore perform a
/// sophisticated aggregation of the anomaly records into buckets.
/// The number of record results depends on the number of anomalies found in each
/// bucket, which relates to the number of time series being modeled and the
/// number of detectors.
/// </para>
/// </summary>
public sealed partial class GetRecordsRequestDescriptor : RequestDescriptor<GetRecordsRequestDescriptor, GetRecordsRequestParameters>
{
	internal GetRecordsRequestDescriptor(Action<GetRecordsRequestDescriptor> configure) => configure.Invoke(this);

	public GetRecordsRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetRecords;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.get_records";

	public GetRecordsRequestDescriptor From(int? from) => Qs("from", from);
	public GetRecordsRequestDescriptor Size(int? size) => Qs("size", size);

	public GetRecordsRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	private bool? DescValue { get; set; }
	private DateTimeOffset? EndValue { get; set; }
	private bool? ExcludeInterimValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.Page? PageValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor PageDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor> PageDescriptorAction { get; set; }
	private double? RecordScoreValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? SortValue { get; set; }
	private DateTimeOffset? StartValue { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>desc</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor Desc(bool? desc = true)
	{
		DescValue = desc;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>end</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor End(DateTimeOffset? end)
	{
		EndValue = end;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>exclude_interim</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor ExcludeInterim(bool? excludeInterim = true)
	{
		ExcludeInterimValue = excludeInterim;
		return Self;
	}

	public GetRecordsRequestDescriptor Page(Elastic.Clients.Elasticsearch.MachineLearning.Page? page)
	{
		PageDescriptor = null;
		PageDescriptorAction = null;
		PageValue = page;
		return Self;
	}

	public GetRecordsRequestDescriptor Page(Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor descriptor)
	{
		PageValue = null;
		PageDescriptorAction = null;
		PageDescriptor = descriptor;
		return Self;
	}

	public GetRecordsRequestDescriptor Page(Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor> configure)
	{
		PageValue = null;
		PageDescriptor = null;
		PageDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>record_score</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor RecordScore(double? recordScore)
	{
		RecordScoreValue = recordScore;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>sort</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor Sort(Elastic.Clients.Elasticsearch.Field? sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>sort</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor Sort<TDocument, TValue>(Expression<Func<TDocument, TValue>> sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>sort</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor Sort<TDocument>(Expression<Func<TDocument, object>> sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>start</c> query parameter.
	/// </para>
	/// </summary>
	public GetRecordsRequestDescriptor Start(DateTimeOffset? start)
	{
		StartValue = start;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DescValue.HasValue)
		{
			writer.WritePropertyName("desc");
			writer.WriteBooleanValue(DescValue.Value);
		}

		if (EndValue is not null)
		{
			writer.WritePropertyName("end");
			JsonSerializer.Serialize(writer, EndValue, options);
		}

		if (ExcludeInterimValue.HasValue)
		{
			writer.WritePropertyName("exclude_interim");
			writer.WriteBooleanValue(ExcludeInterimValue.Value);
		}

		if (PageDescriptor is not null)
		{
			writer.WritePropertyName("page");
			JsonSerializer.Serialize(writer, PageDescriptor, options);
		}
		else if (PageDescriptorAction is not null)
		{
			writer.WritePropertyName("page");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor(PageDescriptorAction), options);
		}
		else if (PageValue is not null)
		{
			writer.WritePropertyName("page");
			JsonSerializer.Serialize(writer, PageValue, options);
		}

		if (RecordScoreValue.HasValue)
		{
			writer.WritePropertyName("record_score");
			writer.WriteNumberValue(RecordScoreValue.Value);
		}

		if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, SortValue, options);
		}

		if (StartValue is not null)
		{
			writer.WritePropertyName("start");
			JsonSerializer.Serialize(writer, StartValue, options);
		}

		writer.WriteEndObject();
	}
}