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

public sealed partial class PutCalendarJobRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Adds an anomaly detection job to a calendar.
/// </para>
/// </summary>
public sealed partial class PutCalendarJobRequest : PlainRequest<PutCalendarJobRequestParameters>
{
	public PutCalendarJobRequest(Elastic.Clients.Elasticsearch.Id calendarId, Elastic.Clients.Elasticsearch.Ids jobId) : base(r => r.Required("calendar_id", calendarId).Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningPutCalendarJob;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.put_calendar_job";
}

/// <summary>
/// <para>
/// Adds an anomaly detection job to a calendar.
/// </para>
/// </summary>
public sealed partial class PutCalendarJobRequestDescriptor : RequestDescriptor<PutCalendarJobRequestDescriptor, PutCalendarJobRequestParameters>
{
	internal PutCalendarJobRequestDescriptor(Action<PutCalendarJobRequestDescriptor> configure) => configure.Invoke(this);

	public PutCalendarJobRequestDescriptor(Elastic.Clients.Elasticsearch.Id calendarId, Elastic.Clients.Elasticsearch.Ids jobId) : base(r => r.Required("calendar_id", calendarId).Required("job_id", jobId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningPutCalendarJob;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.put_calendar_job";

	public PutCalendarJobRequestDescriptor CalendarId(Elastic.Clients.Elasticsearch.Id calendarId)
	{
		RouteValues.Required("calendar_id", calendarId);
		return Self;
	}

	public PutCalendarJobRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Ids jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}