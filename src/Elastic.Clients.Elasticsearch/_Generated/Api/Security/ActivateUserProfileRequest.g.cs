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

public sealed partial class ActivateUserProfileRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Activate a user profile.
/// </para>
/// <para>
/// Create or update a user profile on behalf of another user.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// The calling application must have either an <c>access_token</c> or a combination of <c>username</c> and <c>password</c> for the user that the profile document is intended for.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// <para>
/// This API creates or updates a profile document for end users with information that is extracted from the user's authentication object including <c>username</c>, <c>full_name,</c> <c>roles</c>, and the authentication realm.
/// For example, in the JWT <c>access_token</c> case, the profile user's <c>username</c> is extracted from the JWT token claim pointed to by the <c>claims.principal</c> setting of the JWT realm that authenticated the token.
/// </para>
/// <para>
/// When updating a profile document, the API enables the document if it was disabled.
/// Any updates do not change existing content for either the <c>labels</c> or <c>data</c> fields.
/// </para>
/// </summary>
public sealed partial class ActivateUserProfileRequest : PlainRequest<ActivateUserProfileRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityActivateUserProfile;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.activate_user_profile";

	/// <summary>
	/// <para>
	/// The user's Elasticsearch access token or JWT.
	/// Both <c>access</c> and <c>id</c> JWT token types are supported and they depend on the underlying JWT realm configuration.
	/// If you specify the <c>access_token</c> grant type, this parameter is required.
	/// It is not valid with other grant types.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("access_token")]
	public string? AccessToken { get; set; }

	/// <summary>
	/// <para>
	/// The type of grant.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("grant_type")]
	public Elastic.Clients.Elasticsearch.Security.GrantType GrantType { get; set; }

	/// <summary>
	/// <para>
	/// The user's password.
	/// If you specify the <c>password</c> grant type, this parameter is required.
	/// It is not valid with other grant types.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("password")]
	public string? Password { get; set; }

	/// <summary>
	/// <para>
	/// The username that identifies the user.
	/// If you specify the <c>password</c> grant type, this parameter is required.
	/// It is not valid with other grant types.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("username")]
	public string? Username { get; set; }
}

/// <summary>
/// <para>
/// Activate a user profile.
/// </para>
/// <para>
/// Create or update a user profile on behalf of another user.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// The calling application must have either an <c>access_token</c> or a combination of <c>username</c> and <c>password</c> for the user that the profile document is intended for.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// <para>
/// This API creates or updates a profile document for end users with information that is extracted from the user's authentication object including <c>username</c>, <c>full_name,</c> <c>roles</c>, and the authentication realm.
/// For example, in the JWT <c>access_token</c> case, the profile user's <c>username</c> is extracted from the JWT token claim pointed to by the <c>claims.principal</c> setting of the JWT realm that authenticated the token.
/// </para>
/// <para>
/// When updating a profile document, the API enables the document if it was disabled.
/// Any updates do not change existing content for either the <c>labels</c> or <c>data</c> fields.
/// </para>
/// </summary>
public sealed partial class ActivateUserProfileRequestDescriptor : RequestDescriptor<ActivateUserProfileRequestDescriptor, ActivateUserProfileRequestParameters>
{
	internal ActivateUserProfileRequestDescriptor(Action<ActivateUserProfileRequestDescriptor> configure) => configure.Invoke(this);

	public ActivateUserProfileRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityActivateUserProfile;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.activate_user_profile";

	private string? AccessTokenValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.GrantType GrantTypeValue { get; set; }
	private string? PasswordValue { get; set; }
	private string? UsernameValue { get; set; }

	/// <summary>
	/// <para>
	/// The user's Elasticsearch access token or JWT.
	/// Both <c>access</c> and <c>id</c> JWT token types are supported and they depend on the underlying JWT realm configuration.
	/// If you specify the <c>access_token</c> grant type, this parameter is required.
	/// It is not valid with other grant types.
	/// </para>
	/// </summary>
	public ActivateUserProfileRequestDescriptor AccessToken(string? accessToken)
	{
		AccessTokenValue = accessToken;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The type of grant.
	/// </para>
	/// </summary>
	public ActivateUserProfileRequestDescriptor GrantType(Elastic.Clients.Elasticsearch.Security.GrantType grantType)
	{
		GrantTypeValue = grantType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The user's password.
	/// If you specify the <c>password</c> grant type, this parameter is required.
	/// It is not valid with other grant types.
	/// </para>
	/// </summary>
	public ActivateUserProfileRequestDescriptor Password(string? password)
	{
		PasswordValue = password;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The username that identifies the user.
	/// If you specify the <c>password</c> grant type, this parameter is required.
	/// It is not valid with other grant types.
	/// </para>
	/// </summary>
	public ActivateUserProfileRequestDescriptor Username(string? username)
	{
		UsernameValue = username;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AccessTokenValue))
		{
			writer.WritePropertyName("access_token");
			writer.WriteStringValue(AccessTokenValue);
		}

		writer.WritePropertyName("grant_type");
		JsonSerializer.Serialize(writer, GrantTypeValue, options);
		if (!string.IsNullOrEmpty(PasswordValue))
		{
			writer.WritePropertyName("password");
			writer.WriteStringValue(PasswordValue);
		}

		if (!string.IsNullOrEmpty(UsernameValue))
		{
			writer.WritePropertyName("username");
			writer.WriteStringValue(UsernameValue);
		}

		writer.WriteEndObject();
	}
}