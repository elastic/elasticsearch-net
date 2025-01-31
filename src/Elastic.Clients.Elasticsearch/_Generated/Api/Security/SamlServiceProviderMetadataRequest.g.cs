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

public sealed partial class SamlServiceProviderMetadataRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Create SAML service provider metadata.
/// </para>
/// <para>
/// Generate SAML metadata for a SAML 2.0 Service Provider.
/// </para>
/// </summary>
public sealed partial class SamlServiceProviderMetadataRequest : PlainRequest<SamlServiceProviderMetadataRequestParameters>
{
	public SamlServiceProviderMetadataRequest(Elastic.Clients.Elasticsearch.Name realmName) : base(r => r.Required("realm_name", realmName))
	{
	}

	[JsonConstructor]
	internal SamlServiceProviderMetadataRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecuritySamlServiceProviderMetadata;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.saml_service_provider_metadata";

	/// <summary>
	/// <para>
	/// The name of the SAML realm in Elasticsearch.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Name RealmName { get => P<Elastic.Clients.Elasticsearch.Name>("realm_name"); set => PR("realm_name", value); }
}

/// <summary>
/// <para>
/// Create SAML service provider metadata.
/// </para>
/// <para>
/// Generate SAML metadata for a SAML 2.0 Service Provider.
/// </para>
/// </summary>
public sealed partial class SamlServiceProviderMetadataRequestDescriptor : RequestDescriptor<SamlServiceProviderMetadataRequestDescriptor, SamlServiceProviderMetadataRequestParameters>
{
	internal SamlServiceProviderMetadataRequestDescriptor(Action<SamlServiceProviderMetadataRequestDescriptor> configure) => configure.Invoke(this);

	public SamlServiceProviderMetadataRequestDescriptor(Elastic.Clients.Elasticsearch.Name realmName) : base(r => r.Required("realm_name", realmName))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecuritySamlServiceProviderMetadata;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.saml_service_provider_metadata";

	public SamlServiceProviderMetadataRequestDescriptor RealmName(Elastic.Clients.Elasticsearch.Name realmName)
	{
		RouteValues.Required("realm_name", realmName);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}