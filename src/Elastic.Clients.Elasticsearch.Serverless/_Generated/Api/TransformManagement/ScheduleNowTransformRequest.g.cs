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

namespace Elastic.Clients.Elasticsearch.Serverless.TransformManagement;

public sealed partial class ScheduleNowTransformRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Controls the time to wait for the scheduling to take place</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>Schedule a transform to start now.<br/>Instantly runs a transform to process data.</para>
/// <para>If you _schedule_now a transform, it will process the new data instantly,<br/>without waiting for the configured frequency interval. After _schedule_now API is called,<br/>the transform will be processed again at now + frequency unless _schedule_now API<br/>is called again in the meantime.</para>
/// </summary>
public sealed partial class ScheduleNowTransformRequest : PlainRequest<ScheduleNowTransformRequestParameters>
{
	public ScheduleNowTransformRequest(Elastic.Clients.Elasticsearch.Serverless.Id transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementScheduleNowTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "transform.schedule_now_transform";

	/// <summary>
	/// <para>Controls the time to wait for the scheduling to take place</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>Schedule a transform to start now.<br/>Instantly runs a transform to process data.</para>
/// <para>If you _schedule_now a transform, it will process the new data instantly,<br/>without waiting for the configured frequency interval. After _schedule_now API is called,<br/>the transform will be processed again at now + frequency unless _schedule_now API<br/>is called again in the meantime.</para>
/// </summary>
public sealed partial class ScheduleNowTransformRequestDescriptor : RequestDescriptor<ScheduleNowTransformRequestDescriptor, ScheduleNowTransformRequestParameters>
{
	internal ScheduleNowTransformRequestDescriptor(Action<ScheduleNowTransformRequestDescriptor> configure) => configure.Invoke(this);

	public ScheduleNowTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementScheduleNowTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "transform.schedule_now_transform";

	public ScheduleNowTransformRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public ScheduleNowTransformRequestDescriptor TransformId(Elastic.Clients.Elasticsearch.Serverless.Id transformId)
	{
		RouteValues.Required("transform_id", transformId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}