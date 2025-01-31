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

public sealed partial class GetApiKeyRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys that are currently active. An API key is considered active if it is neither invalidated, nor expired at query time. You can specify this together with other parameters such as <c>owner</c> or <c>name</c>. If <c>active_only</c> is false, the response will include both active and inactive (expired or invalidated) keys.
	/// </para>
	/// </summary>
	public bool? ActiveOnly { get => Q<bool?>("active_only"); set => Q("active_only", value); }

	/// <summary>
	/// <para>
	/// An API key id.
	/// This parameter cannot be used with any of <c>name</c>, <c>realm_name</c> or <c>username</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get => Q<Elastic.Clients.Elasticsearch.Id?>("id"); set => Q("id", value); }

	/// <summary>
	/// <para>
	/// An API key name.
	/// This parameter cannot be used with any of <c>id</c>, <c>realm_name</c> or <c>username</c>.
	/// It supports prefix search with wildcard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Name { get => Q<Elastic.Clients.Elasticsearch.Name?>("name"); set => Q("name", value); }

	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys owned by the currently authenticated user.
	/// The <c>realm_name</c> or <c>username</c> parameters cannot be specified when this parameter is set to <c>true</c> as they are assumed to be the currently authenticated ones.
	/// </para>
	/// </summary>
	public bool? Owner { get => Q<bool?>("owner"); set => Q("owner", value); }

	/// <summary>
	/// <para>
	/// The name of an authentication realm.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? RealmName { get => Q<Elastic.Clients.Elasticsearch.Name?>("realm_name"); set => Q("realm_name", value); }

	/// <summary>
	/// <para>
	/// The username of a user.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Username? Username { get => Q<Elastic.Clients.Elasticsearch.Username?>("username"); set => Q("username", value); }

	/// <summary>
	/// <para>
	/// Return the snapshot of the owner user's role descriptors
	/// associated with the API key. An API key's actual
	/// permission is the intersection of its assigned role
	/// descriptors and the owner user's role descriptors.
	/// </para>
	/// </summary>
	public bool? WithLimitedBy { get => Q<bool?>("with_limited_by"); set => Q("with_limited_by", value); }

	/// <summary>
	/// <para>
	/// Determines whether to also retrieve the profile uid, for the API key owner principal, if it exists.
	/// </para>
	/// </summary>
	public bool? WithProfileUid { get => Q<bool?>("with_profile_uid"); set => Q("with_profile_uid", value); }
}

/// <summary>
/// <para>
/// Get API key information.
/// </para>
/// <para>
/// Retrieves information for one or more API keys.
/// NOTE: If you have only the <c>manage_own_api_key</c> privilege, this API returns only the API keys that you own.
/// If you have <c>read_security</c>, <c>manage_api_key</c> or greater privileges (including <c>manage_security</c>), this API returns all API keys regardless of ownership.
/// </para>
/// </summary>
public sealed partial class GetApiKeyRequest : PlainRequest<GetApiKeyRequestParameters>
{
	[JsonConstructor]
	internal GetApiKeyRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetApiKey;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_api_key";

	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys that are currently active. An API key is considered active if it is neither invalidated, nor expired at query time. You can specify this together with other parameters such as <c>owner</c> or <c>name</c>. If <c>active_only</c> is false, the response will include both active and inactive (expired or invalidated) keys.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? ActiveOnly { get => Q<bool?>("active_only"); set => Q("active_only", value); }

	/// <summary>
	/// <para>
	/// An API key id.
	/// This parameter cannot be used with any of <c>name</c>, <c>realm_name</c> or <c>username</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Id? Id { get => Q<Elastic.Clients.Elasticsearch.Id?>("id"); set => Q("id", value); }

	/// <summary>
	/// <para>
	/// An API key name.
	/// This parameter cannot be used with any of <c>id</c>, <c>realm_name</c> or <c>username</c>.
	/// It supports prefix search with wildcard.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Name? Name { get => Q<Elastic.Clients.Elasticsearch.Name?>("name"); set => Q("name", value); }

	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys owned by the currently authenticated user.
	/// The <c>realm_name</c> or <c>username</c> parameters cannot be specified when this parameter is set to <c>true</c> as they are assumed to be the currently authenticated ones.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Owner { get => Q<bool?>("owner"); set => Q("owner", value); }

	/// <summary>
	/// <para>
	/// The name of an authentication realm.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Name? RealmName { get => Q<Elastic.Clients.Elasticsearch.Name?>("realm_name"); set => Q("realm_name", value); }

	/// <summary>
	/// <para>
	/// The username of a user.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Username? Username { get => Q<Elastic.Clients.Elasticsearch.Username?>("username"); set => Q("username", value); }

	/// <summary>
	/// <para>
	/// Return the snapshot of the owner user's role descriptors
	/// associated with the API key. An API key's actual
	/// permission is the intersection of its assigned role
	/// descriptors and the owner user's role descriptors.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? WithLimitedBy { get => Q<bool?>("with_limited_by"); set => Q("with_limited_by", value); }

	/// <summary>
	/// <para>
	/// Determines whether to also retrieve the profile uid, for the API key owner principal, if it exists.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? WithProfileUid { get => Q<bool?>("with_profile_uid"); set => Q("with_profile_uid", value); }
}

/// <summary>
/// <para>
/// Get API key information.
/// </para>
/// <para>
/// Retrieves information for one or more API keys.
/// NOTE: If you have only the <c>manage_own_api_key</c> privilege, this API returns only the API keys that you own.
/// If you have <c>read_security</c>, <c>manage_api_key</c> or greater privileges (including <c>manage_security</c>), this API returns all API keys regardless of ownership.
/// </para>
/// </summary>
public sealed partial class GetApiKeyRequestDescriptor : RequestDescriptor<GetApiKeyRequestDescriptor, GetApiKeyRequestParameters>
{
	internal GetApiKeyRequestDescriptor(Action<GetApiKeyRequestDescriptor> configure) => configure.Invoke(this);

	public GetApiKeyRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetApiKey;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_api_key";

	public GetApiKeyRequestDescriptor ActiveOnly(bool? activeOnly = true) => Qs("active_only", activeOnly);
	public GetApiKeyRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? id) => Qs("id", id);
	public GetApiKeyRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name? name) => Qs("name", name);
	public GetApiKeyRequestDescriptor Owner(bool? owner = true) => Qs("owner", owner);
	public GetApiKeyRequestDescriptor RealmName(Elastic.Clients.Elasticsearch.Name? realmName) => Qs("realm_name", realmName);
	public GetApiKeyRequestDescriptor Username(Elastic.Clients.Elasticsearch.Username? username) => Qs("username", username);
	public GetApiKeyRequestDescriptor WithLimitedBy(bool? withLimitedBy = true) => Qs("with_limited_by", withLimitedBy);
	public GetApiKeyRequestDescriptor WithProfileUid(bool? withProfileUid = true) => Qs("with_profile_uid", withProfileUid);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}