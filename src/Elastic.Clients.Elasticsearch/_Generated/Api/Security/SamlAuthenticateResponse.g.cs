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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class SamlAuthenticateResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.SamlAuthenticateResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAccessToken = System.Text.Json.JsonEncodedText.Encode("access_token");
	private static readonly System.Text.Json.JsonEncodedText PropExpiresIn = System.Text.Json.JsonEncodedText.Encode("expires_in");
	private static readonly System.Text.Json.JsonEncodedText PropRealm = System.Text.Json.JsonEncodedText.Encode("realm");
	private static readonly System.Text.Json.JsonEncodedText PropRefreshToken = System.Text.Json.JsonEncodedText.Encode("refresh_token");
	private static readonly System.Text.Json.JsonEncodedText PropUsername = System.Text.Json.JsonEncodedText.Encode("username");

	public override Elastic.Clients.Elasticsearch.Security.SamlAuthenticateResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propAccessToken = default;
		LocalJsonValue<int> propExpiresIn = default;
		LocalJsonValue<string> propRealm = default;
		LocalJsonValue<string> propRefreshToken = default;
		LocalJsonValue<string> propUsername = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAccessToken.TryReadProperty(ref reader, options, PropAccessToken, null))
			{
				continue;
			}

			if (propExpiresIn.TryReadProperty(ref reader, options, PropExpiresIn, null))
			{
				continue;
			}

			if (propRealm.TryReadProperty(ref reader, options, PropRealm, null))
			{
				continue;
			}

			if (propRefreshToken.TryReadProperty(ref reader, options, PropRefreshToken, null))
			{
				continue;
			}

			if (propUsername.TryReadProperty(ref reader, options, PropUsername, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Security.SamlAuthenticateResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AccessToken = propAccessToken.Value,
			ExpiresIn = propExpiresIn.Value,
			Realm = propRealm.Value,
			RefreshToken = propRefreshToken.Value,
			Username = propUsername.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.SamlAuthenticateResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAccessToken, value.AccessToken, null, null);
		writer.WriteProperty(options, PropExpiresIn, value.ExpiresIn, null, null);
		writer.WriteProperty(options, PropRealm, value.Realm, null, null);
		writer.WriteProperty(options, PropRefreshToken, value.RefreshToken, null, null);
		writer.WriteProperty(options, PropUsername, value.Username, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.SamlAuthenticateResponseConverter))]
public sealed partial class SamlAuthenticateResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SamlAuthenticateResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SamlAuthenticateResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The access token that was generated by Elasticsearch.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string AccessToken { get; set; }

	/// <summary>
	/// <para>
	/// The amount of time (in seconds) left until the token expires.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ExpiresIn { get; set; }

	/// <summary>
	/// <para>
	/// The name of the realm where the user was authenticated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Realm { get; set; }

	/// <summary>
	/// <para>
	/// The refresh token that was generated by Elasticsearch.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string RefreshToken { get; set; }

	/// <summary>
	/// <para>
	/// The authenticated user's name.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Username { get; set; }
}