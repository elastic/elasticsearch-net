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

public sealed partial class DeleteModelSnapshotRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Delete a model snapshot.
/// You cannot delete the active model snapshot. To delete that snapshot, first
/// revert to a different one. To identify the active model snapshot, refer to
/// the <c>model_snapshot_id</c> in the results from the get jobs API.
/// </para>
/// </summary>
public sealed partial class DeleteModelSnapshotRequest : PlainRequest<DeleteModelSnapshotRequestParameters>
{
	public DeleteModelSnapshotRequest(Elastic.Clients.Elasticsearch.Id jobId, Elastic.Clients.Elasticsearch.Id snapshotId) : base(r => r.Required("job_id", jobId).Required("snapshot_id", snapshotId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningDeleteModelSnapshot;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.delete_model_snapshot";
}

/// <summary>
/// <para>
/// Delete a model snapshot.
/// You cannot delete the active model snapshot. To delete that snapshot, first
/// revert to a different one. To identify the active model snapshot, refer to
/// the <c>model_snapshot_id</c> in the results from the get jobs API.
/// </para>
/// </summary>
public sealed partial class DeleteModelSnapshotRequestDescriptor : RequestDescriptor<DeleteModelSnapshotRequestDescriptor, DeleteModelSnapshotRequestParameters>
{
	internal DeleteModelSnapshotRequestDescriptor(Action<DeleteModelSnapshotRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteModelSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId, Elastic.Clients.Elasticsearch.Id snapshotId) : base(r => r.Required("job_id", jobId).Required("snapshot_id", snapshotId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningDeleteModelSnapshot;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.delete_model_snapshot";

	public DeleteModelSnapshotRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	public DeleteModelSnapshotRequestDescriptor SnapshotId(Elastic.Clients.Elasticsearch.Id snapshotId)
	{
		RouteValues.Required("snapshot_id", snapshotId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}