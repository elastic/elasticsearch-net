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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class GetSecuritySettingsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get security index settings.
/// </para>
/// <para>
/// Get the user-configurable settings for the security internal index (<c>.security</c> and associated indices).
/// Only a subset of the index settings — those that are user-configurable—will be shown.
/// This includes:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// <c>index.auto_expand_replicas</c>
/// </para>
/// </item>
/// <item>
/// <para>
/// <c>index.number_of_replicas</c>
/// </para>
/// </item>
/// </list>
/// </summary>
public sealed partial class GetSecuritySettingsRequest : PlainRequest<GetSecuritySettingsRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetSettings;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_settings";

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get security index settings.
/// </para>
/// <para>
/// Get the user-configurable settings for the security internal index (<c>.security</c> and associated indices).
/// Only a subset of the index settings — those that are user-configurable—will be shown.
/// This includes:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// <c>index.auto_expand_replicas</c>
/// </para>
/// </item>
/// <item>
/// <para>
/// <c>index.number_of_replicas</c>
/// </para>
/// </item>
/// </list>
/// </summary>
public sealed partial class GetSecuritySettingsRequestDescriptor : RequestDescriptor<GetSecuritySettingsRequestDescriptor, GetSecuritySettingsRequestParameters>
{
	internal GetSecuritySettingsRequestDescriptor(Action<GetSecuritySettingsRequestDescriptor> configure) => configure.Invoke(this);

	public GetSecuritySettingsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetSettings;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_settings";

	public GetSecuritySettingsRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}