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

public sealed partial class StopTransformRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Specifies what to do when the request: contains wildcard expressions and there are no transforms that match;<br/>contains the `_all` string or no identifiers and there are no matches; contains wildcard expressions and there<br/>are only partial matches.</para>
	/// <para>If it is true, the API returns a successful acknowledgement message when there are no matches. When there are<br/>only partial matches, the API stops the appropriate transforms.</para>
	/// <para>If it is false, the request returns a 404 status code when there are no matches or only partial matches.</para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>If it is true, the API forcefully stops the transforms.</para>
	/// </summary>
	public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

	/// <summary>
	/// <para>Period to wait for a response when `wait_for_completion` is `true`. If no response is received before the<br/>timeout expires, the request returns a timeout exception. However, the request continues processing and<br/>eventually moves the transform to a STOPPED state.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>If it is true, the transform does not completely stop until the current checkpoint is completed. If it is false,<br/>the transform stops as soon as possible.</para>
	/// </summary>
	public bool? WaitForCheckpoint { get => Q<bool?>("wait_for_checkpoint"); set => Q("wait_for_checkpoint", value); }

	/// <summary>
	/// <para>If it is true, the API blocks until the indexer state completely stops. If it is false, the API returns<br/>immediately and the indexer is stopped asynchronously in the background.</para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

/// <summary>
/// <para>Stop transforms.<br/>Stops one or more transforms.</para>
/// </summary>
public sealed partial class StopTransformRequest : PlainRequest<StopTransformRequestParameters>
{
	public StopTransformRequest(Elastic.Clients.Elasticsearch.Serverless.Name transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementStopTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "transform.stop_transform";

	/// <summary>
	/// <para>Specifies what to do when the request: contains wildcard expressions and there are no transforms that match;<br/>contains the `_all` string or no identifiers and there are no matches; contains wildcard expressions and there<br/>are only partial matches.</para>
	/// <para>If it is true, the API returns a successful acknowledgement message when there are no matches. When there are<br/>only partial matches, the API stops the appropriate transforms.</para>
	/// <para>If it is false, the request returns a 404 status code when there are no matches or only partial matches.</para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>If it is true, the API forcefully stops the transforms.</para>
	/// </summary>
	[JsonIgnore]
	public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

	/// <summary>
	/// <para>Period to wait for a response when `wait_for_completion` is `true`. If no response is received before the<br/>timeout expires, the request returns a timeout exception. However, the request continues processing and<br/>eventually moves the transform to a STOPPED state.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>If it is true, the transform does not completely stop until the current checkpoint is completed. If it is false,<br/>the transform stops as soon as possible.</para>
	/// </summary>
	[JsonIgnore]
	public bool? WaitForCheckpoint { get => Q<bool?>("wait_for_checkpoint"); set => Q("wait_for_checkpoint", value); }

	/// <summary>
	/// <para>If it is true, the API blocks until the indexer state completely stops. If it is false, the API returns<br/>immediately and the indexer is stopped asynchronously in the background.</para>
	/// </summary>
	[JsonIgnore]
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

/// <summary>
/// <para>Stop transforms.<br/>Stops one or more transforms.</para>
/// </summary>
public sealed partial class StopTransformRequestDescriptor : RequestDescriptor<StopTransformRequestDescriptor, StopTransformRequestParameters>
{
	internal StopTransformRequestDescriptor(Action<StopTransformRequestDescriptor> configure) => configure.Invoke(this);

	public StopTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Name transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementStopTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "transform.stop_transform";

	public StopTransformRequestDescriptor AllowNoMatch(bool? allowNoMatch = true) => Qs("allow_no_match", allowNoMatch);
	public StopTransformRequestDescriptor Force(bool? force = true) => Qs("force", force);
	public StopTransformRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);
	public StopTransformRequestDescriptor WaitForCheckpoint(bool? waitForCheckpoint = true) => Qs("wait_for_checkpoint", waitForCheckpoint);
	public StopTransformRequestDescriptor WaitForCompletion(bool? waitForCompletion = true) => Qs("wait_for_completion", waitForCompletion);

	public StopTransformRequestDescriptor TransformId(Elastic.Clients.Elasticsearch.Serverless.Name transformId)
	{
		RouteValues.Required("transform_id", transformId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}