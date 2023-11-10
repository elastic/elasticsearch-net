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

namespace Elastic.Clients.Elasticsearch.Serverless;

public sealed class ClosePointInTimeRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>Closes a point-in-time.</para>
/// </summary>
public sealed partial class ClosePointInTimeRequest : PlainRequest<ClosePointInTimeRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceClosePointInTime;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => true;

	internal override string OperationName => "close_point_in_time";

	/// <summary>
	/// <para>The ID of the point-in-time.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("id")]
	public Elastic.Clients.Elasticsearch.Serverless.Id Id { get; set; }
}

/// <summary>
/// <para>Closes a point-in-time.</para>
/// </summary>
public sealed partial class ClosePointInTimeRequestDescriptor : RequestDescriptor<ClosePointInTimeRequestDescriptor, ClosePointInTimeRequestParameters>
{
	internal ClosePointInTimeRequestDescriptor(Action<ClosePointInTimeRequestDescriptor> configure) => configure.Invoke(this);

	public ClosePointInTimeRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceClosePointInTime;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => true;

	internal override string OperationName => "close_point_in_time";

	private Elastic.Clients.Elasticsearch.Serverless.Id IdValue { get; set; }

	/// <summary>
	/// <para>The ID of the point-in-time.</para>
	/// </summary>
	public ClosePointInTimeRequestDescriptor Id(Elastic.Clients.Elasticsearch.Serverless.Id id)
	{
		IdValue = id;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("id");
		JsonSerializer.Serialize(writer, IdValue, options);
		writer.WriteEndObject();
	}
}