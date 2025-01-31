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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

public sealed partial class StartTransformRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Restricts the set of transformed entities to those changed after this time. Relative times like now-30d are supported. Only applicable for continuous transforms.
	/// </para>
	/// </summary>
	public string? From { get => Q<string?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Start a transform.
/// Starts a transform.
/// </para>
/// <para>
/// When you start a transform, it creates the destination index if it does not already exist. The <c>number_of_shards</c> is
/// set to <c>1</c> and the <c>auto_expand_replicas</c> is set to <c>0-1</c>. If it is a pivot transform, it deduces the mapping
/// definitions for the destination index from the source indices and the transform aggregations. If fields in the
/// destination index are derived from scripts (as in the case of <c>scripted_metric</c> or <c>bucket_script</c> aggregations),
/// the transform uses dynamic mappings unless an index template exists. If it is a latest transform, it does not deduce
/// mapping definitions; it uses dynamic mappings. To use explicit mappings, create the destination index before you
/// start the transform. Alternatively, you can create an index template, though it does not affect the deduced mappings
/// in a pivot transform.
/// </para>
/// <para>
/// When the transform starts, a series of validations occur to ensure its success. If you deferred validation when you
/// created the transform, they occur when you start the transform—​with the exception of privilege checks. When
/// Elasticsearch security features are enabled, the transform remembers which roles the user that created it had at the
/// time of creation and uses those same roles. If those roles do not have the required privileges on the source and
/// destination indices, the transform fails when it attempts unauthorized operations.
/// </para>
/// </summary>
public sealed partial class StartTransformRequest : PlainRequest<StartTransformRequestParameters>
{
	public StartTransformRequest(Elastic.Clients.Elasticsearch.Id transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	[JsonConstructor]
	internal StartTransformRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementStartTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "transform.start_transform";

	/// <summary>
	/// <para>
	/// Identifier for the transform.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Id TransformId { get => P<Elastic.Clients.Elasticsearch.Id>("transform_id"); set => PR("transform_id", value); }

	/// <summary>
	/// <para>
	/// Restricts the set of transformed entities to those changed after this time. Relative times like now-30d are supported. Only applicable for continuous transforms.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? From { get => Q<string?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Start a transform.
/// Starts a transform.
/// </para>
/// <para>
/// When you start a transform, it creates the destination index if it does not already exist. The <c>number_of_shards</c> is
/// set to <c>1</c> and the <c>auto_expand_replicas</c> is set to <c>0-1</c>. If it is a pivot transform, it deduces the mapping
/// definitions for the destination index from the source indices and the transform aggregations. If fields in the
/// destination index are derived from scripts (as in the case of <c>scripted_metric</c> or <c>bucket_script</c> aggregations),
/// the transform uses dynamic mappings unless an index template exists. If it is a latest transform, it does not deduce
/// mapping definitions; it uses dynamic mappings. To use explicit mappings, create the destination index before you
/// start the transform. Alternatively, you can create an index template, though it does not affect the deduced mappings
/// in a pivot transform.
/// </para>
/// <para>
/// When the transform starts, a series of validations occur to ensure its success. If you deferred validation when you
/// created the transform, they occur when you start the transform—​with the exception of privilege checks. When
/// Elasticsearch security features are enabled, the transform remembers which roles the user that created it had at the
/// time of creation and uses those same roles. If those roles do not have the required privileges on the source and
/// destination indices, the transform fails when it attempts unauthorized operations.
/// </para>
/// </summary>
public sealed partial class StartTransformRequestDescriptor : RequestDescriptor<StartTransformRequestDescriptor, StartTransformRequestParameters>
{
	internal StartTransformRequestDescriptor(Action<StartTransformRequestDescriptor> configure) => configure.Invoke(this);

	public StartTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Id transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementStartTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "transform.start_transform";

	public StartTransformRequestDescriptor From(string? from) => Qs("from", from);
	public StartTransformRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public StartTransformRequestDescriptor TransformId(Elastic.Clients.Elasticsearch.Id transformId)
	{
		RouteValues.Required("transform_id", transformId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}