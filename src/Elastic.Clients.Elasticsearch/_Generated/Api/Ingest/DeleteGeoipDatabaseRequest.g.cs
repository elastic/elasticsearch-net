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

namespace Elastic.Clients.Elasticsearch.Ingest;

public sealed partial class DeleteGeoipDatabaseRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Delete GeoIP database configurations.
/// Delete one or more IP geolocation database configurations.
/// </para>
/// </summary>
public sealed partial class DeleteGeoipDatabaseRequest : PlainRequest<DeleteGeoipDatabaseRequestParameters>
{
	public DeleteGeoipDatabaseRequest(Elastic.Clients.Elasticsearch.Ids id) : base(r => r.Required("id", id))
	{
	}

	[JsonConstructor]
	internal DeleteGeoipDatabaseRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestDeleteGeoipDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.delete_geoip_database";

	/// <summary>
	/// <para>
	/// A comma-separated list of geoip database configurations to delete
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Ids Id { get => P<Elastic.Clients.Elasticsearch.Ids>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

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
/// Delete GeoIP database configurations.
/// Delete one or more IP geolocation database configurations.
/// </para>
/// </summary>
public sealed partial class DeleteGeoipDatabaseRequestDescriptor<TDocument> : RequestDescriptor<DeleteGeoipDatabaseRequestDescriptor<TDocument>, DeleteGeoipDatabaseRequestParameters>
{
	internal DeleteGeoipDatabaseRequestDescriptor(Action<DeleteGeoipDatabaseRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DeleteGeoipDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Ids id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestDeleteGeoipDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.delete_geoip_database";

	public DeleteGeoipDatabaseRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public DeleteGeoipDatabaseRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public DeleteGeoipDatabaseRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Ids id)
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
/// Delete GeoIP database configurations.
/// Delete one or more IP geolocation database configurations.
/// </para>
/// </summary>
public sealed partial class DeleteGeoipDatabaseRequestDescriptor : RequestDescriptor<DeleteGeoipDatabaseRequestDescriptor, DeleteGeoipDatabaseRequestParameters>
{
	internal DeleteGeoipDatabaseRequestDescriptor(Action<DeleteGeoipDatabaseRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteGeoipDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Ids id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestDeleteGeoipDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.delete_geoip_database";

	public DeleteGeoipDatabaseRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public DeleteGeoipDatabaseRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public DeleteGeoipDatabaseRequestDescriptor Id(Elastic.Clients.Elasticsearch.Ids id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}