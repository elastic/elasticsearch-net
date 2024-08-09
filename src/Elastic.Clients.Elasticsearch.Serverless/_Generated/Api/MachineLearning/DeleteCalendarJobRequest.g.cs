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

public sealed partial class DeleteCalendarJobRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Delete anomaly jobs from a calendar.
/// </para>
/// </summary>
public sealed partial class DeleteCalendarJobRequest : PlainRequest<DeleteCalendarJobRequestParameters>
{
	public DeleteCalendarJobRequest(Elastic.Clients.Elasticsearch.Serverless.Id calendarId, Elastic.Clients.Elasticsearch.Serverless.Ids jobId) : base(r => r.Required("calendar_id", calendarId).Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningDeleteCalendarJob;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.delete_calendar_job";
}

/// <summary>
/// <para>
/// Delete anomaly jobs from a calendar.
/// </para>
/// </summary>
public sealed partial class DeleteCalendarJobRequestDescriptor : RequestDescriptor<DeleteCalendarJobRequestDescriptor, DeleteCalendarJobRequestParameters>
{
	internal DeleteCalendarJobRequestDescriptor(Action<DeleteCalendarJobRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteCalendarJobRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id calendarId, Elastic.Clients.Elasticsearch.Serverless.Ids jobId) : base(r => r.Required("calendar_id", calendarId).Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningDeleteCalendarJob;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.delete_calendar_job";

	public DeleteCalendarJobRequestDescriptor CalendarId(Elastic.Clients.Elasticsearch.Serverless.Id calendarId)
	{
		RouteValues.Required("calendar_id", calendarId);
		return Self;
	}

	public DeleteCalendarJobRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Serverless.Ids jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}