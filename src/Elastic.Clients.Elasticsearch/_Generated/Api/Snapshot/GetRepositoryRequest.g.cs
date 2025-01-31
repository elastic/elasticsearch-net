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

namespace Elastic.Clients.Elasticsearch.Snapshot;

public sealed partial class GetRepositoryRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Return local information, do not retrieve the state from master node (default: false)
	/// </para>
	/// </summary>
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	/// <summary>
	/// <para>
	/// Explicit operation timeout for connection to master node
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get snapshot repository information.
/// </para>
/// </summary>
public sealed partial class GetRepositoryRequest : PlainRequest<GetRepositoryRequestParameters>
{
	[JsonConstructor]
	public GetRepositoryRequest()
	{
	}

	public GetRepositoryRequest(Elastic.Clients.Elasticsearch.Names? name) : base(r => r.Optional("repository", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotGetRepository;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "snapshot.get_repository";

	/// <summary>
	/// <para>
	/// A comma-separated list of repository names
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Names? Name { get => P<Elastic.Clients.Elasticsearch.Names?>("repository"); set => PO("repository", value); }

	/// <summary>
	/// <para>
	/// Return local information, do not retrieve the state from master node (default: false)
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	/// <summary>
	/// <para>
	/// Explicit operation timeout for connection to master node
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get snapshot repository information.
/// </para>
/// </summary>
public sealed partial class GetRepositoryRequestDescriptor : RequestDescriptor<GetRepositoryRequestDescriptor, GetRepositoryRequestParameters>
{
	internal GetRepositoryRequestDescriptor(Action<GetRepositoryRequestDescriptor> configure) => configure.Invoke(this);

	public GetRepositoryRequestDescriptor(Elastic.Clients.Elasticsearch.Names? name) : base(r => r.Optional("repository", name))
	{
	}

	public GetRepositoryRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotGetRepository;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "snapshot.get_repository";

	public GetRepositoryRequestDescriptor Local(bool? local = true) => Qs("local", local);
	public GetRepositoryRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);

	public GetRepositoryRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names? name)
	{
		RouteValues.Optional("repository", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}