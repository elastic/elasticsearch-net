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
/// </summary>
public sealed partial class ActivateUserProfileRequest : PlainRequest<ActivateUserProfileRequestParameters>
{
	[JsonConstructor]
	internal ActivateUserProfileRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityActivateUserProfile;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.activate_user_profile";

	[JsonInclude, JsonPropertyName("access_token")]
	public string? AccessToken { get; set; }
	[JsonInclude, JsonPropertyName("grant_type")]
	public Elastic.Clients.Elasticsearch.Security.GrantType GrantType { get; set; }
	[JsonInclude, JsonPropertyName("password")]
	public string? Password { get; set; }
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

	public ActivateUserProfileRequestDescriptor AccessToken(string? accessToken)
	{
		AccessTokenValue = accessToken;
		return Self;
	}

	public ActivateUserProfileRequestDescriptor GrantType(Elastic.Clients.Elasticsearch.Security.GrantType grantType)
	{
		GrantTypeValue = grantType;
		return Self;
	}

	public ActivateUserProfileRequestDescriptor Password(string? password)
	{
		PasswordValue = password;
		return Self;
	}

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