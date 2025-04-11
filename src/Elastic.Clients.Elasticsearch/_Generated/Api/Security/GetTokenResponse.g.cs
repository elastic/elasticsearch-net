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

internal sealed partial class GetTokenResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetTokenResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAccessToken = System.Text.Json.JsonEncodedText.Encode("access_token");
	private static readonly System.Text.Json.JsonEncodedText PropAuthentication = System.Text.Json.JsonEncodedText.Encode("authentication");
	private static readonly System.Text.Json.JsonEncodedText PropExpiresIn = System.Text.Json.JsonEncodedText.Encode("expires_in");
	private static readonly System.Text.Json.JsonEncodedText PropKerberosAuthenticationResponseToken = System.Text.Json.JsonEncodedText.Encode("kerberos_authentication_response_token");
	private static readonly System.Text.Json.JsonEncodedText PropRefreshToken = System.Text.Json.JsonEncodedText.Encode("refresh_token");
	private static readonly System.Text.Json.JsonEncodedText PropScope = System.Text.Json.JsonEncodedText.Encode("scope");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Security.GetTokenResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propAccessToken = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Security.AuthenticatedUser> propAuthentication = default;
		LocalJsonValue<long> propExpiresIn = default;
		LocalJsonValue<string?> propKerberosAuthenticationResponseToken = default;
		LocalJsonValue<string?> propRefreshToken = default;
		LocalJsonValue<string?> propScope = default;
		LocalJsonValue<string> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAccessToken.TryReadProperty(ref reader, options, PropAccessToken, null))
			{
				continue;
			}

			if (propAuthentication.TryReadProperty(ref reader, options, PropAuthentication, null))
			{
				continue;
			}

			if (propExpiresIn.TryReadProperty(ref reader, options, PropExpiresIn, null))
			{
				continue;
			}

			if (propKerberosAuthenticationResponseToken.TryReadProperty(ref reader, options, PropKerberosAuthenticationResponseToken, null))
			{
				continue;
			}

			if (propRefreshToken.TryReadProperty(ref reader, options, PropRefreshToken, null))
			{
				continue;
			}

			if (propScope.TryReadProperty(ref reader, options, PropScope, null))
			{
				continue;
			}

			if (propType.TryReadProperty(ref reader, options, PropType, null))
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
		return new Elastic.Clients.Elasticsearch.Security.GetTokenResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AccessToken = propAccessToken.Value,
			Authentication = propAuthentication.Value,
			ExpiresIn = propExpiresIn.Value,
			KerberosAuthenticationResponseToken = propKerberosAuthenticationResponseToken.Value,
			RefreshToken = propRefreshToken.Value,
			Scope = propScope.Value,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetTokenResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAccessToken, value.AccessToken, null, null);
		writer.WriteProperty(options, PropAuthentication, value.Authentication, null, null);
		writer.WriteProperty(options, PropExpiresIn, value.ExpiresIn, null, null);
		writer.WriteProperty(options, PropKerberosAuthenticationResponseToken, value.KerberosAuthenticationResponseToken, null, null);
		writer.WriteProperty(options, PropRefreshToken, value.RefreshToken, null, null);
		writer.WriteProperty(options, PropScope, value.Scope, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetTokenResponseConverter))]
public sealed partial class GetTokenResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetTokenResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetTokenResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		string AccessToken { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.Security.AuthenticatedUser Authentication { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		long ExpiresIn { get; set; }
	public string? KerberosAuthenticationResponseToken { get; set; }
	public string? RefreshToken { get; set; }
	public string? Scope { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string Type { get; set; }
}