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
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Ingest;

public sealed partial class DeleteIpLocationDatabaseRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Delete IP geolocation database configurations.
/// </para>
/// </summary>
public sealed partial class DeleteIpLocationDatabaseRequest : PlainRequest<DeleteIpLocationDatabaseRequestParameters>
{
	public DeleteIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serverless.Ids id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestDeleteIpLocationDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.delete_ip_location_database";

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Delete IP geolocation database configurations.
/// </para>
/// </summary>
public sealed partial class DeleteIpLocationDatabaseRequestDescriptor<TDocument> : RequestDescriptor<DeleteIpLocationDatabaseRequestDescriptor<TDocument>, DeleteIpLocationDatabaseRequestParameters>
{
	internal DeleteIpLocationDatabaseRequestDescriptor(Action<DeleteIpLocationDatabaseRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DeleteIpLocationDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Ids id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestDeleteIpLocationDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.delete_ip_location_database";

	public DeleteIpLocationDatabaseRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public DeleteIpLocationDatabaseRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public DeleteIpLocationDatabaseRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Serverless.Ids id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Delete IP geolocation database configurations.
/// </para>
/// </summary>
public sealed partial class DeleteIpLocationDatabaseRequestDescriptor : RequestDescriptor<DeleteIpLocationDatabaseRequestDescriptor, DeleteIpLocationDatabaseRequestParameters>
{
	internal DeleteIpLocationDatabaseRequestDescriptor(Action<DeleteIpLocationDatabaseRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteIpLocationDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Ids id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestDeleteIpLocationDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.delete_ip_location_database";

	public DeleteIpLocationDatabaseRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public DeleteIpLocationDatabaseRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public DeleteIpLocationDatabaseRequestDescriptor Id(Elastic.Clients.Elasticsearch.Serverless.Ids id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}