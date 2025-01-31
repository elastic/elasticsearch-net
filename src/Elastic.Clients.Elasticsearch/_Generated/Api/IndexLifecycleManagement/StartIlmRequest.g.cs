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

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

public sealed partial class StartIlmRequestParameters : RequestParameters
{
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Start the ILM plugin.
/// Start the index lifecycle management plugin if it is currently stopped.
/// ILM is started automatically when the cluster is formed.
/// Restarting ILM is necessary only when it has been stopped using the stop ILM API.
/// </para>
/// </summary>
public sealed partial class StartIlmRequest : PlainRequest<StartIlmRequestParameters>
{
	[JsonConstructor]
	internal StartIlmRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexLifecycleManagementStart;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ilm.start";

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Start the ILM plugin.
/// Start the index lifecycle management plugin if it is currently stopped.
/// ILM is started automatically when the cluster is formed.
/// Restarting ILM is necessary only when it has been stopped using the stop ILM API.
/// </para>
/// </summary>
public sealed partial class StartIlmRequestDescriptor : RequestDescriptor<StartIlmRequestDescriptor, StartIlmRequestParameters>
{
	internal StartIlmRequestDescriptor(Action<StartIlmRequestDescriptor> configure) => configure.Invoke(this);

	public StartIlmRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexLifecycleManagementStart;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ilm.start";

	public StartIlmRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public StartIlmRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}