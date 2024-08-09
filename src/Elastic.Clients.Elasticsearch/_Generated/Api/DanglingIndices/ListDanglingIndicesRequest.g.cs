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

namespace Elastic.Clients.Elasticsearch.DanglingIndices;

public sealed partial class ListDanglingIndicesRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Returns all dangling indices.
/// </para>
/// </summary>
public sealed partial class ListDanglingIndicesRequest : PlainRequest<ListDanglingIndicesRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.DanglingIndicesListDanglingIndices;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "dangling_indices.list_dangling_indices";
}

/// <summary>
/// <para>
/// Returns all dangling indices.
/// </para>
/// </summary>
public sealed partial class ListDanglingIndicesRequestDescriptor : RequestDescriptor<ListDanglingIndicesRequestDescriptor, ListDanglingIndicesRequestParameters>
{
	internal ListDanglingIndicesRequestDescriptor(Action<ListDanglingIndicesRequestDescriptor> configure) => configure.Invoke(this);

	public ListDanglingIndicesRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.DanglingIndicesListDanglingIndices;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "dangling_indices.list_dangling_indices";

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}