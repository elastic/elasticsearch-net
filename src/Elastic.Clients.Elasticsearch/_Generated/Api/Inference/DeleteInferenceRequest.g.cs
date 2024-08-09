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

namespace Elastic.Clients.Elasticsearch.Inference;

public sealed partial class DeleteInferenceRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// When true, the endpoint is not deleted, and a list of ingest processors which reference this endpoint is returned
	/// </para>
	/// </summary>
	public bool? DryRun { get => Q<bool?>("dry_run"); set => Q("dry_run", value); }

	/// <summary>
	/// <para>
	/// When true, the inference endpoint is forcefully deleted even if it is still being used by ingest processors or semantic text fields
	/// </para>
	/// </summary>
	public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }
}

/// <summary>
/// <para>
/// Delete an inference endpoint
/// </para>
/// </summary>
public sealed partial class DeleteInferenceRequest : PlainRequest<DeleteInferenceRequestParameters>
{
	public DeleteInferenceRequest(Elastic.Clients.Elasticsearch.Id inferenceId) : base(r => r.Required("inference_id", inferenceId))
	{
	}

	public DeleteInferenceRequest(Elastic.Clients.Elasticsearch.Inference.TaskType? taskType, Elastic.Clients.Elasticsearch.Id inferenceId) : base(r => r.Optional("task_type", taskType).Required("inference_id", inferenceId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.InferenceDelete;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "inference.delete";

	/// <summary>
	/// <para>
	/// When true, the endpoint is not deleted, and a list of ingest processors which reference this endpoint is returned
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? DryRun { get => Q<bool?>("dry_run"); set => Q("dry_run", value); }

	/// <summary>
	/// <para>
	/// When true, the inference endpoint is forcefully deleted even if it is still being used by ingest processors or semantic text fields
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }
}

/// <summary>
/// <para>
/// Delete an inference endpoint
/// </para>
/// </summary>
public sealed partial class DeleteInferenceRequestDescriptor : RequestDescriptor<DeleteInferenceRequestDescriptor, DeleteInferenceRequestParameters>
{
	internal DeleteInferenceRequestDescriptor(Action<DeleteInferenceRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteInferenceRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.TaskType? taskType, Elastic.Clients.Elasticsearch.Id inferenceId) : base(r => r.Optional("task_type", taskType).Required("inference_id", inferenceId))
	{
	}

	public DeleteInferenceRequestDescriptor(Elastic.Clients.Elasticsearch.Id inferenceId) : base(r => r.Required("inference_id", inferenceId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.InferenceDelete;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "inference.delete";

	public DeleteInferenceRequestDescriptor DryRun(bool? dryRun = true) => Qs("dry_run", dryRun);
	public DeleteInferenceRequestDescriptor Force(bool? force = true) => Qs("force", force);

	public DeleteInferenceRequestDescriptor InferenceId(Elastic.Clients.Elasticsearch.Id inferenceId)
	{
		RouteValues.Required("inference_id", inferenceId);
		return Self;
	}

	public DeleteInferenceRequestDescriptor TaskType(Elastic.Clients.Elasticsearch.Inference.TaskType? taskType)
	{
		RouteValues.Optional("task_type", taskType);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}