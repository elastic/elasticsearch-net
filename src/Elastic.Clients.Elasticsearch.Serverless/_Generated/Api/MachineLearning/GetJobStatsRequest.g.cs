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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class GetJobStatsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Specifies what to do when the request:</para>
	/// <para>1. Contains wildcard expressions and there are no jobs that match.<br/>2. Contains the _all string or no identifiers and there are no matches.<br/>3. Contains wildcard expressions and there are only partial matches.</para>
	/// <para>If `true`, the API returns an empty `jobs` array when<br/>there are no matches and the subset of results when there are partial<br/>matches. If `false`, the API returns a `404` status<br/>code when there are no matches or only partial matches.</para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }
}

/// <summary>
/// <para>Retrieves usage information for anomaly detection jobs.</para>
/// </summary>
public sealed partial class GetJobStatsRequest : PlainRequest<GetJobStatsRequestParameters>
{
	public GetJobStatsRequest()
	{
	}

	public GetJobStatsRequest(Elastic.Clients.Elasticsearch.Serverless.Id? jobId) : base(r => r.Optional("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetJobStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_job_stats";

	/// <summary>
	/// <para>Specifies what to do when the request:</para>
	/// <para>1. Contains wildcard expressions and there are no jobs that match.<br/>2. Contains the _all string or no identifiers and there are no matches.<br/>3. Contains wildcard expressions and there are only partial matches.</para>
	/// <para>If `true`, the API returns an empty `jobs` array when<br/>there are no matches and the subset of results when there are partial<br/>matches. If `false`, the API returns a `404` status<br/>code when there are no matches or only partial matches.</para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }
}

/// <summary>
/// <para>Retrieves usage information for anomaly detection jobs.</para>
/// </summary>
public sealed partial class GetJobStatsRequestDescriptor : RequestDescriptor<GetJobStatsRequestDescriptor, GetJobStatsRequestParameters>
{
	internal GetJobStatsRequestDescriptor(Action<GetJobStatsRequestDescriptor> configure) => configure.Invoke(this);

	public GetJobStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id? jobId) : base(r => r.Optional("job_id", jobId))
	{
	}

	public GetJobStatsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetJobStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_job_stats";

	public GetJobStatsRequestDescriptor AllowNoMatch(bool? allowNoMatch = true) => Qs("allow_no_match", allowNoMatch);

	public GetJobStatsRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Serverless.Id? jobId)
	{
		RouteValues.Optional("job_id", jobId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}