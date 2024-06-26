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

public sealed partial class GetJobsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Specifies what to do when the request:</para>
	/// <para>1. Contains wildcard expressions and there are no jobs that match.<br/>2. Contains the _all string or no identifiers and there are no matches.<br/>3. Contains wildcard expressions and there are only partial matches.</para>
	/// <para>The default value is `true`, which returns an empty `jobs` array when<br/>there are no matches and the subset of results when there are partial<br/>matches. If this parameter is `false`, the request returns a `404` status<br/>code when there are no matches or only partial matches.</para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>Indicates if certain fields should be removed from the configuration on<br/>retrieval. This allows the configuration to be in an acceptable format to<br/>be retrieved and then added to another cluster.</para>
	/// </summary>
	public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }
}

/// <summary>
/// <para>Retrieves configuration information for anomaly detection jobs.<br/>You can get information for multiple anomaly detection jobs in a single API<br/>request by using a group name, a comma-separated list of jobs, or a wildcard<br/>expression. You can get information for all anomaly detection jobs by using<br/>`_all`, by specifying `*` as the `<job_id>`, or by omitting the `<job_id>`.</para>
/// </summary>
public sealed partial class GetJobsRequest : PlainRequest<GetJobsRequestParameters>
{
	public GetJobsRequest()
	{
	}

	public GetJobsRequest(Elastic.Clients.Elasticsearch.Ids? jobId) : base(r => r.Optional("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetJobs;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_jobs";

	/// <summary>
	/// <para>Specifies what to do when the request:</para>
	/// <para>1. Contains wildcard expressions and there are no jobs that match.<br/>2. Contains the _all string or no identifiers and there are no matches.<br/>3. Contains wildcard expressions and there are only partial matches.</para>
	/// <para>The default value is `true`, which returns an empty `jobs` array when<br/>there are no matches and the subset of results when there are partial<br/>matches. If this parameter is `false`, the request returns a `404` status<br/>code when there are no matches or only partial matches.</para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>Indicates if certain fields should be removed from the configuration on<br/>retrieval. This allows the configuration to be in an acceptable format to<br/>be retrieved and then added to another cluster.</para>
	/// </summary>
	[JsonIgnore]
	public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }
}

/// <summary>
/// <para>Retrieves configuration information for anomaly detection jobs.<br/>You can get information for multiple anomaly detection jobs in a single API<br/>request by using a group name, a comma-separated list of jobs, or a wildcard<br/>expression. You can get information for all anomaly detection jobs by using<br/>`_all`, by specifying `*` as the `<job_id>`, or by omitting the `<job_id>`.</para>
/// </summary>
public sealed partial class GetJobsRequestDescriptor : RequestDescriptor<GetJobsRequestDescriptor, GetJobsRequestParameters>
{
	internal GetJobsRequestDescriptor(Action<GetJobsRequestDescriptor> configure) => configure.Invoke(this);

	public GetJobsRequestDescriptor(Elastic.Clients.Elasticsearch.Ids? jobId) : base(r => r.Optional("job_id", jobId))
	{
	}

	public GetJobsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetJobs;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_jobs";

	public GetJobsRequestDescriptor AllowNoMatch(bool? allowNoMatch = true) => Qs("allow_no_match", allowNoMatch);
	public GetJobsRequestDescriptor ExcludeGenerated(bool? excludeGenerated = true) => Qs("exclude_generated", excludeGenerated);

	public GetJobsRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Ids? jobId)
	{
		RouteValues.Optional("job_id", jobId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}