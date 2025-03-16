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

public sealed partial class OidcAuthenticateRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Authenticate OpenID Connect.
/// </para>
/// <para>
/// Exchange an OpenID Connect authentication response message for an Elasticsearch internal access token and refresh token that can be subsequently used for authentication.
/// </para>
/// <para>
/// Elasticsearch exposes all the necessary OpenID Connect related functionality with the OpenID Connect APIs.
/// These APIs are used internally by Kibana in order to provide OpenID Connect based authentication, but can also be used by other, custom web applications or other clients.
/// </para>
/// </summary>
public sealed partial class OidcAuthenticateRequest : PlainRequest<OidcAuthenticateRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityOidcAuthenticate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.oidc_authenticate";

	/// <summary>
	/// <para>
	/// Associate a client session with an ID token and mitigate replay attacks.
	/// This value needs to be the same as the one that was provided to the <c>/_security/oidc/prepare</c> API or the one that was generated by Elasticsearch and included in the response to that call.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("nonce")]
	public string Nonce { get; set; }

	/// <summary>
	/// <para>
	/// The name of the OpenID Connect realm.
	/// This property is useful in cases where multiple realms are defined.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("realm")]
	public string? Realm { get; set; }

	/// <summary>
	/// <para>
	/// The URL to which the OpenID Connect Provider redirected the User Agent in response to an authentication request after a successful authentication.
	/// This URL must be provided as-is (URL encoded), taken from the body of the response or as the value of a location header in the response from the OpenID Connect Provider.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("redirect_uri")]
	public string RedirectUri { get; set; }

	/// <summary>
	/// <para>
	/// Maintain state between the authentication request and the response.
	/// This value needs to be the same as the one that was provided to the <c>/_security/oidc/prepare</c> API or the one that was generated by Elasticsearch and included in the response to that call.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("state")]
	public string State { get; set; }
}

/// <summary>
/// <para>
/// Authenticate OpenID Connect.
/// </para>
/// <para>
/// Exchange an OpenID Connect authentication response message for an Elasticsearch internal access token and refresh token that can be subsequently used for authentication.
/// </para>
/// <para>
/// Elasticsearch exposes all the necessary OpenID Connect related functionality with the OpenID Connect APIs.
/// These APIs are used internally by Kibana in order to provide OpenID Connect based authentication, but can also be used by other, custom web applications or other clients.
/// </para>
/// </summary>
public sealed partial class OidcAuthenticateRequestDescriptor : RequestDescriptor<OidcAuthenticateRequestDescriptor, OidcAuthenticateRequestParameters>
{
	internal OidcAuthenticateRequestDescriptor(Action<OidcAuthenticateRequestDescriptor> configure) => configure.Invoke(this);

	public OidcAuthenticateRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityOidcAuthenticate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.oidc_authenticate";

	private string NonceValue { get; set; }
	private string? RealmValue { get; set; }
	private string RedirectUriValue { get; set; }
	private string StateValue { get; set; }

	/// <summary>
	/// <para>
	/// Associate a client session with an ID token and mitigate replay attacks.
	/// This value needs to be the same as the one that was provided to the <c>/_security/oidc/prepare</c> API or the one that was generated by Elasticsearch and included in the response to that call.
	/// </para>
	/// </summary>
	public OidcAuthenticateRequestDescriptor Nonce(string nonce)
	{
		NonceValue = nonce;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the OpenID Connect realm.
	/// This property is useful in cases where multiple realms are defined.
	/// </para>
	/// </summary>
	public OidcAuthenticateRequestDescriptor Realm(string? realm)
	{
		RealmValue = realm;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The URL to which the OpenID Connect Provider redirected the User Agent in response to an authentication request after a successful authentication.
	/// This URL must be provided as-is (URL encoded), taken from the body of the response or as the value of a location header in the response from the OpenID Connect Provider.
	/// </para>
	/// </summary>
	public OidcAuthenticateRequestDescriptor RedirectUri(string redirectUri)
	{
		RedirectUriValue = redirectUri;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maintain state between the authentication request and the response.
	/// This value needs to be the same as the one that was provided to the <c>/_security/oidc/prepare</c> API or the one that was generated by Elasticsearch and included in the response to that call.
	/// </para>
	/// </summary>
	public OidcAuthenticateRequestDescriptor State(string state)
	{
		StateValue = state;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("nonce");
		writer.WriteStringValue(NonceValue);
		if (!string.IsNullOrEmpty(RealmValue))
		{
			writer.WritePropertyName("realm");
			writer.WriteStringValue(RealmValue);
		}

		writer.WritePropertyName("redirect_uri");
		writer.WriteStringValue(RedirectUriValue);
		writer.WritePropertyName("state");
		writer.WriteStringValue(StateValue);
		writer.WriteEndObject();
	}
}