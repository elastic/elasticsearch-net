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

public sealed partial class GetBucketsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Skips the specified number of buckets.</para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>Specifies the maximum number of buckets to obtain.</para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>Retrieves anomaly detection job results for one or more buckets.<br/>The API presents a chronological view of the records, grouped by bucket.</para>
/// </summary>
public sealed partial class GetBucketsRequest : PlainRequest<GetBucketsRequestParameters>
{
	public GetBucketsRequest(Elastic.Clients.Elasticsearch.Id jobId, DateTimeOffset? timestamp) : base(r => r.Required("job_id", jobId).Optional("timestamp", timestamp))
	{
	}

	public GetBucketsRequest(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetBuckets;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.get_buckets";

	/// <summary>
	/// <para>Skips the specified number of buckets.</para>
	/// </summary>
	[JsonIgnore]
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>Specifies the maximum number of buckets to obtain.</para>
	/// </summary>
	[JsonIgnore]
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>Refer to the description for the `anomaly_score` query parameter.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("anomaly_score")]
	public double? AnomalyScore { get; set; }

	/// <summary>
	/// <para>Refer to the description for the `desc` query parameter.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("desc")]
	public bool? Desc { get; set; }

	/// <summary>
	/// <para>Refer to the description for the `end` query parameter.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("end")]
	public DateTimeOffset? End { get; set; }

	/// <summary>
	/// <para>Refer to the description for the `exclude_interim` query parameter.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("exclude_interim")]
	public bool? ExcludeInterim { get; set; }

	/// <summary>
	/// <para>Refer to the description for the `expand` query parameter.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("expand")]
	public bool? Expand { get; set; }
	[JsonInclude, JsonPropertyName("page")]
	public Elastic.Clients.Elasticsearch.MachineLearning.Page? Page { get; set; }

	/// <summary>
	/// <para>Refer to the desription for the `sort` query parameter.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("sort")]
	public Elastic.Clients.Elasticsearch.Field? Sort { get; set; }

	/// <summary>
	/// <para>Refer to the description for the `start` query parameter.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("start")]
	public DateTimeOffset? Start { get; set; }
}

/// <summary>
/// <para>Retrieves anomaly detection job results for one or more buckets.<br/>The API presents a chronological view of the records, grouped by bucket.</para>
/// </summary>
public sealed partial class GetBucketsRequestDescriptor<TDocument> : RequestDescriptor<GetBucketsRequestDescriptor<TDocument>, GetBucketsRequestParameters>
{
	internal GetBucketsRequestDescriptor(Action<GetBucketsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GetBucketsRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId, DateTimeOffset? timestamp) : base(r => r.Required("job_id", jobId).Optional("timestamp", timestamp))
	{
	}

	public GetBucketsRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetBuckets;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.get_buckets";

	public GetBucketsRequestDescriptor<TDocument> From(int? from) => Qs("from", from);
	public GetBucketsRequestDescriptor<TDocument> Size(int? size) => Qs("size", size);

	public GetBucketsRequestDescriptor<TDocument> JobId(Elastic.Clients.Elasticsearch.Id jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	public GetBucketsRequestDescriptor<TDocument> Timestamp(DateTimeOffset? timestamp)
	{
		RouteValues.Optional("timestamp", timestamp);
		return Self;
	}

	private double? AnomalyScoreValue { get; set; }
	private bool? DescValue { get; set; }
	private DateTimeOffset? EndValue { get; set; }
	private bool? ExcludeInterimValue { get; set; }
	private bool? ExpandValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.Page? PageValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor PageDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor> PageDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Field? SortValue { get; set; }
	private DateTimeOffset? StartValue { get; set; }

	/// <summary>
	/// <para>Refer to the description for the `anomaly_score` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> AnomalyScore(double? anomalyScore)
	{
		AnomalyScoreValue = anomalyScore;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `desc` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> Desc(bool? desc = true)
	{
		DescValue = desc;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `end` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> End(DateTimeOffset? end)
	{
		EndValue = end;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `exclude_interim` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> ExcludeInterim(bool? excludeInterim = true)
	{
		ExcludeInterimValue = excludeInterim;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `expand` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> Expand(bool? expand = true)
	{
		ExpandValue = expand;
		return Self;
	}

	public GetBucketsRequestDescriptor<TDocument> Page(Elastic.Clients.Elasticsearch.MachineLearning.Page? page)
	{
		PageDescriptor = null;
		PageDescriptorAction = null;
		PageValue = page;
		return Self;
	}

	public GetBucketsRequestDescriptor<TDocument> Page(Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor descriptor)
	{
		PageValue = null;
		PageDescriptorAction = null;
		PageDescriptor = descriptor;
		return Self;
	}

	public GetBucketsRequestDescriptor<TDocument> Page(Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor> configure)
	{
		PageValue = null;
		PageDescriptor = null;
		PageDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the desription for the `sort` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.Field? sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the desription for the `sort` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> Sort<TValue>(Expression<Func<TDocument, TValue>> sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the desription for the `sort` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> Sort(Expression<Func<TDocument, object>> sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `start` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor<TDocument> Start(DateTimeOffset? start)
	{
		StartValue = start;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AnomalyScoreValue.HasValue)
		{
			writer.WritePropertyName("anomaly_score");
			writer.WriteNumberValue(AnomalyScoreValue.Value);
		}

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

		if (ExpandValue.HasValue)
		{
			writer.WritePropertyName("expand");
			writer.WriteBooleanValue(ExpandValue.Value);
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
/// <para>Retrieves anomaly detection job results for one or more buckets.<br/>The API presents a chronological view of the records, grouped by bucket.</para>
/// </summary>
public sealed partial class GetBucketsRequestDescriptor : RequestDescriptor<GetBucketsRequestDescriptor, GetBucketsRequestParameters>
{
	internal GetBucketsRequestDescriptor(Action<GetBucketsRequestDescriptor> configure) => configure.Invoke(this);

	public GetBucketsRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId, DateTimeOffset? timestamp) : base(r => r.Required("job_id", jobId).Optional("timestamp", timestamp))
	{
	}

	public GetBucketsRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetBuckets;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.get_buckets";

	public GetBucketsRequestDescriptor From(int? from) => Qs("from", from);
	public GetBucketsRequestDescriptor Size(int? size) => Qs("size", size);

	public GetBucketsRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	public GetBucketsRequestDescriptor Timestamp(DateTimeOffset? timestamp)
	{
		RouteValues.Optional("timestamp", timestamp);
		return Self;
	}

	private double? AnomalyScoreValue { get; set; }
	private bool? DescValue { get; set; }
	private DateTimeOffset? EndValue { get; set; }
	private bool? ExcludeInterimValue { get; set; }
	private bool? ExpandValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.Page? PageValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor PageDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor> PageDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Field? SortValue { get; set; }
	private DateTimeOffset? StartValue { get; set; }

	/// <summary>
	/// <para>Refer to the description for the `anomaly_score` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor AnomalyScore(double? anomalyScore)
	{
		AnomalyScoreValue = anomalyScore;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `desc` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor Desc(bool? desc = true)
	{
		DescValue = desc;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `end` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor End(DateTimeOffset? end)
	{
		EndValue = end;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `exclude_interim` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor ExcludeInterim(bool? excludeInterim = true)
	{
		ExcludeInterimValue = excludeInterim;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `expand` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor Expand(bool? expand = true)
	{
		ExpandValue = expand;
		return Self;
	}

	public GetBucketsRequestDescriptor Page(Elastic.Clients.Elasticsearch.MachineLearning.Page? page)
	{
		PageDescriptor = null;
		PageDescriptorAction = null;
		PageValue = page;
		return Self;
	}

	public GetBucketsRequestDescriptor Page(Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor descriptor)
	{
		PageValue = null;
		PageDescriptorAction = null;
		PageDescriptor = descriptor;
		return Self;
	}

	public GetBucketsRequestDescriptor Page(Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor> configure)
	{
		PageValue = null;
		PageDescriptor = null;
		PageDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the desription for the `sort` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor Sort(Elastic.Clients.Elasticsearch.Field? sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the desription for the `sort` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor Sort<TDocument, TValue>(Expression<Func<TDocument, TValue>> sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the desription for the `sort` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor Sort<TDocument>(Expression<Func<TDocument, object>> sort)
	{
		SortValue = sort;
		return Self;
	}

	/// <summary>
	/// <para>Refer to the description for the `start` query parameter.</para>
	/// </summary>
	public GetBucketsRequestDescriptor Start(DateTimeOffset? start)
	{
		StartValue = start;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AnomalyScoreValue.HasValue)
		{
			writer.WritePropertyName("anomaly_score");
			writer.WriteNumberValue(AnomalyScoreValue.Value);
		}

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

		if (ExpandValue.HasValue)
		{
			writer.WritePropertyName("expand");
			writer.WriteBooleanValue(ExpandValue.Value);
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