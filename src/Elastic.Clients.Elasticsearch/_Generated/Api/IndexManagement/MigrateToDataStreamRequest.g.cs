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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class MigrateToDataStreamRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>Converts an index alias to a data stream.<br/>You must have a matching index template that is data stream enabled.<br/>The alias must meet the following criteria:<br/>The alias must have a write index;<br/>All indices for the alias must have a `@timestamp` field mapping of a `date` or `date_nanos` field type;<br/>The alias must not have any filters;<br/>The alias must not use custom routing.<br/>If successful, the request removes the alias and creates a data stream with the same name.<br/>The indices for the alias become hidden backing indices for the stream.<br/>The write index for the alias becomes the write index for the stream.</para>
/// </summary>
public sealed partial class MigrateToDataStreamRequest : PlainRequest<MigrateToDataStreamRequestParameters>
{
	public MigrateToDataStreamRequest(Elastic.Clients.Elasticsearch.IndexName name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementMigrateToDataStream;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.migrate_to_data_stream";
}

/// <summary>
/// <para>Converts an index alias to a data stream.<br/>You must have a matching index template that is data stream enabled.<br/>The alias must meet the following criteria:<br/>The alias must have a write index;<br/>All indices for the alias must have a `@timestamp` field mapping of a `date` or `date_nanos` field type;<br/>The alias must not have any filters;<br/>The alias must not use custom routing.<br/>If successful, the request removes the alias and creates a data stream with the same name.<br/>The indices for the alias become hidden backing indices for the stream.<br/>The write index for the alias becomes the write index for the stream.</para>
/// </summary>
public sealed partial class MigrateToDataStreamRequestDescriptor : RequestDescriptor<MigrateToDataStreamRequestDescriptor, MigrateToDataStreamRequestParameters>
{
	internal MigrateToDataStreamRequestDescriptor(Action<MigrateToDataStreamRequestDescriptor> configure) => configure.Invoke(this);

	public MigrateToDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName name) : base(r => r.Required("name", name))
	{
	}

	internal MigrateToDataStreamRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementMigrateToDataStream;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.migrate_to_data_stream";

	public MigrateToDataStreamRequestDescriptor Name(Elastic.Clients.Elasticsearch.IndexName name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}